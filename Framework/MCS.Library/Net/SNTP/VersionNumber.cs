﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Net.SNTP
{
    /// <summary>
    /// Indicator of the NTP/SNTP version number.
    /// </summary>
    public enum VersionNumber
    {
        /// <summary>
        /// Version 3 (IPv4 only).
        /// </summary>
        Version3 = 3,

        /// <summary>
        /// Version 4 (IPv4, IPv6 and OSI).
        /// </summary>
        Version4 = 4,
    }
}
