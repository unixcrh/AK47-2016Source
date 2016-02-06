using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MCS.Library.Configuration;

namespace MCS.Library.SOA.DataObjects.Dynamics.Configuration
{
    //public class SAPLoginParams : ConfigurationSection
    //{
    //    /// <summary>
    //    /// 获取配置SAP登陆配置信息
    //    /// </summary>
    //    /// <returns></returns>
    //    public static SAPLoginParams GetConfig()
    //    {
    //        SAPLoginParams settings = (SAPLoginParams)ConfigurationBroker.GetSection("SAPLoginParams");

    //        if (settings == null)
    //            settings = new SAPLoginParams();

    //        return settings;
    //    }

    //    [ConfigurationProperty("System", IsRequired = true)]
    //    public string System
    //    {
    //        get
    //        {
    //            return (string)base["System"];
    //        }
    //        set
    //        {
    //            base["System"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// SAP登陆的服务器地址
    //    /// </summary>
    //    [ConfigurationProperty("ApplicationServer", IsRequired = true)]
    //    public string ApplicationServer
    //    {
    //        get
    //        {
    //            return (string)base["ApplicationServer"];
    //        }
    //        set
    //        {
    //            base["ApplicationServer"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// SAP的系统编号
    //    /// </summary>
    //    [ConfigurationProperty("SystemNumber", IsRequired = true)]
    //    public string SystemNumber
    //    {
    //        get
    //        {
    //            return (string)base["SystemNumber"];
    //        }
    //        set
    //        {
    //            base["SystemNumber"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// SAP登陆的客户端
    //    /// </summary>
    //    [ConfigurationProperty("Client", IsRequired = true)]
    //    public string Client
    //    {
    //        get
    //        {
    //            return (string)base["Client"];
    //        }
    //        set
    //        {
    //            base["Client"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 登陆SAP的用户名
    //    /// </summary>
    //    [ConfigurationProperty("User", IsRequired = true)]
    //    public string User
    //    {
    //        get
    //        {
    //            return (string)base["User"];
    //        }
    //        set
    //        {
    //            base["User"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 登陆SAP的密码
    //    /// </summary>
    //    [ConfigurationProperty("Password", IsRequired = true)]
    //    public string Password
    //    {
    //        get
    //        {
    //            return (string)base["Password"];
    //        }
    //        set
    //        {
    //            base["Password"] = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 登陆SAP的语言选项
    //    /// </summary>
    //    [ConfigurationProperty("Language", IsRequired = false, DefaultValue = "ZH")]
    //    public string Language
    //    {
    //        get
    //        {
    //            return (string)base["Language"];
    //        }
    //        set
    //        {
    //            base["Language"] = value;
    //        }
    //    }
    //}
}
