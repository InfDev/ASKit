using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKit.Common
{
    /// <summary>
    /// OS groups
    /// </summary>
    public enum OsGroupId
    {
        /// <summary>
        /// Unknown OS. Any other operating system. This includes Browser (WASM)
        /// </summary>
        Other = 0,
        /// <summary>
        /// Any Windows OS
        /// </summary>
        Windows = 1,
        /// <summary>
        /// Any Linux OS
        /// </summary>
        Linux = 2,
        /// <summary>
        /// Any Macintosh OS
        /// </summary>
        MacOS = 3,
        /// <summary>
        /// Any Android OS
        /// </summary>
        Android = 4
    }
    
    /// <summary>
    /// OS helper
    /// </summary>
    public static class OsHelper
    {
        /// <summary>
        /// Default Shell for Windows
        /// </summary>
        public const string DefaultWindowsShell = "cmd";
        /// <summary>
        /// Default Shell for Linux
        /// </summary>
        public const string DefaultLinuxShell = "bash";
        /// <summary>
        /// Default Shell for MacOS
        /// </summary>
        public const string DefaultMacOSShell = "zsh";
        /// <summary>
        /// Default Shell for Android
        /// </summary>
        public const string DefaultAndroidShell = "adb";

        /// <summary>
        /// Current group id
        /// </summary>
        /// <returns></returns>
        public static OsGroupId CurrentOsGroupId()
        {
            var group = OsGroupId.Other;
            if (OperatingSystem.IsWindows())
                group = OsGroupId.Windows;
            else if (OperatingSystem.IsLinux())
                group = OsGroupId.Linux;
            else if (OperatingSystem.IsMacOS())
                group = OsGroupId.MacOS;
            else if (OperatingSystem.IsAndroid())
                group = OsGroupId.Android;
            return group;
        }

        /// <summary>
        /// Default shell for current OS
        /// </summary>
        /// <returns></returns>
        public static (string shell, string key) DefaultShell() => CurrentOsGroupId() switch
        {
            OsGroupId.Windows => (DefaultWindowsShell, "/c"),
            OsGroupId.Linux => (DefaultLinuxShell, "-c"),
            OsGroupId.MacOS => (DefaultMacOSShell, "-c"),
            OsGroupId.Android => (DefaultAndroidShell, "-c"),
            _ => ("", "")
        };
    }
}
