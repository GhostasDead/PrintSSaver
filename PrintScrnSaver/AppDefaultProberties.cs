using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintSSaver
{
    internal static class AppDefaultProberties
    {
        #region Settings' path
        /// <summary>
        /// Gets a directory in %AppData%\ folder.
        /// </summary>
        public static string GetAppDir(string appName)
        {
            return $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\{appName}";
        }

        /// <summary>
        /// Gets path of a file in a certain directory.
        /// </summary>
        public static string GetSettingsFile(string appDir, string settingsFile)
        {
            return $@"{appDir}\{settingsFile}";
        }
        #endregion

        #region Settings' data
        /// <summary>
        /// App's specific default values.
        /// </summary>
        public static string[] DefaultDataValues()
        {
            string defaultLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\PrintSSaver";
            string defaultName = "Title+Timestamp";
            string defaultTitle = "ProcessName";
            string defaultFormat = "png";
            string defaultSFX = "SFXdisabled%DefaultSound";
            return new string[] { defaultName, defaultName, defaultTitle, defaultFormat, defaultSFX };
        }
        #endregion
    }
}
