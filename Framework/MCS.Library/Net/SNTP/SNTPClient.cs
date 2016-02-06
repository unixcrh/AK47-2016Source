using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.Net.SNTP
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultEvent("QueryServerCompleted"),
    DefaultProperty("RemoteSNTPServer")]
    public partial class SNTPClient : Component
    {
        private static double LocalOffsetSeconds = 0;
        private static ReaderWriterLockSlim _RWLocalOffsetLock = new ReaderWriterLockSlim();

        static SNTPClient()
        {
            Thread t = new Thread(new ThreadStart(SyncLocalTimeOffset));

            t.IsBackground = true;
            t.Priority = ThreadPriority.Lowest;
            t.Start();
        }

        private static void SyncLocalTimeOffset()
        {
            double localOffset = 0;

            while (true)
            {
                try
                {
                    localOffset = GetLocalClockOffset();

                    _RWLocalOffsetLock.EnterWriteLock();

                    try
                    {
                        LocalOffsetSeconds = localOffset;
                    }
                    finally
                    {
                        _RWLocalOffsetLock.ExitWriteLock();
                    }
                }
                catch (System.Exception)
                {
                }

                try
                {
                    Thread.Sleep(SNTPSettings.GetConfigOrDefault().PoolInterval);
                }
                catch (System.Exception)
                {
                    Thread.Sleep(60000);
                }
            }
        }

        /// <summary>
        /// 本地时间的偏移量
        /// </summary>
        public static TimeSpan LocalOffset
        {
            get
            {
                double offsetLocal = 0;

                _RWLocalOffsetLock.EnterReadLock();

                try
                {
                    offsetLocal = LocalOffsetSeconds;
                }
                finally
                {
                    _RWLocalOffsetLock.ExitReadLock();
                }

                return TimeSpan.FromSeconds(offsetLocal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public SNTPClient(IContainer container)
        {
            container.Add(this);

            this.InitializeComponent();
        }

        #region Fields

        private TimeSpan _Timeout;
        private AsyncOperation asyncOperation = null;

        /// <summary>
        /// The server that is used by default.
        /// </summary>
        public static readonly RemoteSNTPServer DefaultServer = RemoteSNTPServer.Default;

        /// <summary>
        /// The default number of milliseconds used for send and receive.
        /// </summary>
        public const int DefaultTimeout = 5000;

        /// <summary>
        /// The default NTP/SNTP version number.
        /// </summary>
        public const VersionNumber DefaultVersionNumber = VersionNumber.Version3;

        private readonly SendOrPostCallback OperationCompleted;
        private readonly WorkerThreadStartDelegate ThreadStart;
        private readonly object SyncObject = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a new instance of SNTPClient.
        /// </summary>
        public SNTPClient()
        {
            this.InitializeComponent();

            this.Initialize();
            this.ThreadStart = new WorkerThreadStartDelegate(WorkerThreadStart);
            this.OperationCompleted = new SendOrPostCallback(AsyncOperationCompleted);
            this.Timeout = SNTPSettings.GetConfigOrDefault().Timeout;
            this.VersionNumber = DefaultVersionNumber;
            this.UpdateLocalDateTime = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets whether the SNTPClient is busy.
        /// </summary>
        [Browsable(false)]
        public bool IsBusy
        {
            get;
            private set;
        }

        /// <summary>
        /// 调整后的本地时间
        /// </summary>
        public static DateTime AdjustedLocalTime
        {
            get
            {
                return DateTime.Now + LocalOffset;
            }
        }

        /// <summary>
        /// 调整后的Utc时间
        /// </summary>
        public static DateTime AdjustedUtcTime
        {
            get
            {
                return AdjustedLocalTime.ToUniversalTime();
            }
        }

        /// <summary>
        /// 调整后的时间。根据SNTPSettings的DefaultDateTimeKind决定是否使用Local还是Utc时间。
        /// 默认是Local
        /// </summary>
        public static DateTime AdjustedTime
        {
            get
            {
                DateTime result = AdjustedLocalTime;

                if (SNTPSettings.GetConfigOrDefault().DefaultDateTimeKind == DateTimeKind.Utc)
                    result = AdjustedUtcTime;

                return result;
            }
        }

        ///// <summary>
        ///// Gets the real local date and time using the default server and a total timeout of 1 second.
        ///// If there is an error or exception, DateTime.MinValue is returned.
        ///// (NB: This property getter is blocking)
        ///// </summary>
        //public static DateTime Now
        //{
        //    get { return GetNow(); }
        //}

        /// <summary>
        /// Gets or sets the server to use.
        /// </summary>
        [Description("The server to use."),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Category("Connection")]
        public RemoteSNTPServer RemoteSNTPServer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the timeout in milliseconds used for sending and receiving.
        /// </summary>
        [Description("The timeout in milliseconds used for sending and receiving.")]
        [DefaultValue(DefaultTimeout),
        Category("Connection")]
        public TimeSpan Timeout
        {
            get
            {
                return this._Timeout;
            }
            set
            {
                this._Timeout = value;
            }
        }

        /// <summary>
        /// Gets or sets whether to update the local date and time to the date and time calculated by querying the server.
        /// </summary>
        [Description("Whether to update the local date and time to the date and time calculated by querying the server."),
        DefaultValue(true),
        Category("Actions")]
        public bool UpdateLocalDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the NTP/SNTP version to use.
        /// </summary>
        [Description("The NTP/SNTP version to use.")]
        [DefaultValue(DefaultVersionNumber),
        Category("Connection")]
        public VersionNumber VersionNumber
        {
            get;
            set;
        }

        #endregion Properties

        #region Delegates and Events

        // Delegates

        private delegate void WorkerThreadStartDelegate();

        // Events 

        /// <summary>
        /// Raised when a query to the server completes successfully.
        /// </summary>
        [Description("Raised when a query to the server completes successfully."),
        Category("Success")]
        public event EventHandler<QueryServerCompletedEventArgs> QueryServerCompleted;

        #endregion Delegates and Events

        #region Methods

        // Public Methods

        /// <summary>
        /// Calculates the current local time zone offset from UTC.
        /// </summary>
        /// <returns>A TimeSpan that is the current local time zone offset from UTC.</returns>
        public static TimeSpan GetCurrentLocalTimeZoneOffset()
        {
            return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        }

        /// <summary>
        /// Gets the real local date and time using the default server and a total timeout of 1 second.
        /// If there is an error or exception, DateTime.MinValue is returned.
        /// </summary>
        /// <returns>The real local date and time.</returns>
        public static DateTime GetNow()
        {
            return GetNow(SNTPSettings.GetSNTPServer(), SNTPSettings.GetConfigOrDefault().Timeout);
        }

        /// <summary>
        /// Gets the real local date and time using the specified server and a total timeout of 1 second.
        /// If there is an error or exception, DateTime.MinValue is returned.
        /// </summary>
        /// <param name="remoteSNTPServer">The server to use.</param>
        /// <returns>The real local date and time.</returns>
        public static DateTime GetNow(RemoteSNTPServer remoteSNTPServer)
        {
            return GetNow(remoteSNTPServer, SNTPSettings.GetConfigOrDefault().Timeout);
        }

        /// <summary>
        /// Gets the real local date and time using the default server and the specified timeout.
        /// If there is an error or exception, DateTime.MinValue is returned.
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds used for sending and receiving.</param>
        /// <returns>The real local date and time.</returns>
        public static DateTime GetNow(TimeSpan timeout)
        {
            return GetNow(SNTPSettings.GetSNTPServer(), timeout);
        }

        /// <summary>
        /// Gets the real local date and time using the default server and the specified timeout.
        /// If there is an error or exception, DateTime.MinValue is returned.
        /// </summary>
        /// <param name="remoteSNTPServer">The server to use.</param>
        /// <param name="timeout">The timeout in milliseconds used for sending and receiving.</param>
        /// <returns>The real local date and time.</returns>
        public static DateTime GetNow(RemoteSNTPServer remoteSNTPServer, TimeSpan timeout)
        {
            SNTPClient sntpClient = new SNTPClient();
            sntpClient.UpdateLocalDateTime = false;
            sntpClient.RemoteSNTPServer = remoteSNTPServer;
            sntpClient.Timeout = timeout;

            QueryServerCompletedEventArgs args = sntpClient.QueryServer();

            if (args.Succeeded)
                return DateTime.Now.AddSeconds(args.Data.LocalClockOffset);
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static double GetLocalClockOffset()
        {
            return GetLocalClockOffset(SNTPSettings.GetConfigOrDefault().Timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static double GetLocalClockOffset(TimeSpan timeout)
        {
            return GetLocalClockOffset(SNTPSettings.GetSNTPServer(), timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteSNTPServer">The server to use.</param>
        /// <param name="timeout">The timeout in milliseconds used for sending and receiving.</param>
        /// <returns>The real local offset by seconds.</returns>
        public static double GetLocalClockOffset(RemoteSNTPServer remoteSNTPServer, TimeSpan timeout)
        {
            SNTPClient sntpClient = new SNTPClient();
            sntpClient.UpdateLocalDateTime = false;
            sntpClient.RemoteSNTPServer = remoteSNTPServer;
            sntpClient.Timeout = timeout;
            QueryServerCompletedEventArgs args = sntpClient.QueryServer();

            if (args.Succeeded)
                return args.Data.LocalClockOffset;
            else
                throw new ApplicationException(args.ErrorData.ErrorText);
        }

        /// <summary>
        /// Queries the specified server on a separate thread.
        /// </summary>
        /// <returns>true if the SNTPClient wasn't busy, otherwise false.</returns>
        public bool QueryServerAsync()
        {
            bool result = false;
            if (!IsBusy)
            {
                IsBusy = true;
                asyncOperation = AsyncOperationManager.CreateOperation(null);
                ThreadStart.BeginInvoke(null, null);
                result = true;
            }
            return result;
        }

        // Protected Methods 

        /// <summary>
        /// Raises the QueryServerCompleted event.
        /// </summary>
        /// <param name="e">A QueryServerCompletedEventArgs instance.</param>
        protected virtual void OnQueryServerCompleted(QueryServerCompletedEventArgs e)
        {
            EventHandler<QueryServerCompletedEventArgs> eh = QueryServerCompleted;
            if (eh != null)
                eh(this, e);
        }

        // Private Methods 

        private void AsyncOperationCompleted(object arg)
        {
            IsBusy = false;
            OnQueryServerCompleted((QueryServerCompletedEventArgs)arg);
        }

        private void Initialize()
        {
            if (RemoteSNTPServer == null)
                RemoteSNTPServer = SNTPSettings.GetSNTPServer();
        }

        /// <summary>
        /// This is the 'nuts and bolts' method that queries the server.
        /// </summary>
        /// <returns>A QueryServerResults instance that holds the results of the query.</returns>
        private QueryServerCompletedEventArgs QueryServer()
        {
            QueryServerCompletedEventArgs result = new QueryServerCompletedEventArgs();
            Initialize();
            using (UdpClient client = new UdpClient())
            {
                try
                {
                    // Configure and connect the socket.
                    IPEndPoint ipEndPoint = RemoteSNTPServer.GetIPEndPoint();
                    client.Client.SendTimeout = (int)this.Timeout.TotalMilliseconds;
                    client.Client.ReceiveTimeout = (int)this.Timeout.TotalMilliseconds;
                    client.Connect(ipEndPoint);

                    // Send and receive the data, and save the completion DateTime.
                    SNTPData request = SNTPData.GetClientRequestPacket(this.VersionNumber);
                    client.Send(request, request.Length);
                    result.Data = client.Receive(ref ipEndPoint);
                    result.Data.DestinationDateTime = DateTime.Now.ToUniversalTime();

                    // Check the data
                    if (result.Data.Mode == Mode.Server)
                    {
                        result.Succeeded = true;

                        // Call other method(s) if needed
                        if (this.UpdateLocalDateTime)
                        {
                            this.UpdateTime(result.Data.LocalClockOffset);
                            result.LocalDateTimeUpdated = true;
                        }
                    }
                    else
                    {
                        result.ErrorData = new ErrorData("The response from the server was invalid.");
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    result.ErrorData = new ErrorData(ex);
                    return result;
                }
            }
        }

        private void UpdateTime(double localClockOffset)
        {
            SYSTEMTIME systemTime;
            DateTime newDateTime = DateTime.Now.AddSeconds(localClockOffset);
            systemTime.wYear = (UInt16)newDateTime.Year;
            systemTime.wMonth = (UInt16)newDateTime.Month;
            systemTime.wDayOfWeek = (UInt16)newDateTime.DayOfWeek;
            systemTime.wDay = (UInt16)newDateTime.Day;
            systemTime.wHour = (UInt16)newDateTime.Hour;
            systemTime.wMinute = (UInt16)newDateTime.Minute;
            systemTime.wSecond = (UInt16)newDateTime.Second;
            systemTime.wMilliseconds = (UInt16)newDateTime.Millisecond;
            if (!NativeMethods.SetLocalTime(ref systemTime))
                throw new Win32Exception();
        }

        private void WorkerThreadStart()
        {
            lock (this.SyncObject)
            {
                QueryServerCompletedEventArgs e = QueryServer();

                asyncOperation.PostOperationCompleted(OperationCompleted, e);
            }
        }

        #endregion Methods
    }
}
