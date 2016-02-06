using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Core
{
    /// <summary>
    /// 为系统常用的队列功能提供接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueue<T>
    {
        /// <summary>
        /// 在队列中添加消息
        /// </summary>
        /// <param name="category"></param>
        /// <param name="messages"></param>
        void AddMessages(string category, params T[] messages);

        /// <summary>
        /// 从队列中获取消息，并且从队列中移除
        /// </summary>
        /// <param name="category"></param>
        /// <param name="count">读取的最大个数</param>
        /// <returns></returns>
        IEnumerable<T> GetMessages(string category, int count = 1);

        /// <summary>
        /// 从队列中获取消息，但是不从队列中移除
        /// </summary>
        /// <param name="category"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> PeekMessages(string category, int count = 1);
    }
}
