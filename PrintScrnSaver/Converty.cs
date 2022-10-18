using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PrintSSaver
{
    static class Converty
    {
        #region Converts for different memory and storage scenarios
        internal static byte ByteAppends(string appends)
        {
            if (appends == "Timestamp")
                return 0;
            else if (appends == "Title")
                return 1;
            return 2;
        }
        internal static string StringAppends(byte appends)
        {
            if (appends == 0)
                return "Timestamp";
            else if (appends == 1)
                return "Title";
            return "Title+Timestamp";
        }
        
        internal static bool BoolTitle(string isProcessName)
        {
            if (isProcessName == "WindowTitle")
                return false;
            return true;
        }
        internal static string StringTitle(bool proccesNameNotWindowTitle)
        {
            if (!proccesNameNotWindowTitle)
                return "WindowTitle";
            return "ProcessName";

        }
        internal static bool BoolImgFormat(string isPng)
        {
            if (isPng == "jpg")
                return false;
            return true;

        }
        internal static string StringImgFormat(bool pngNotJpg)
        {
            if (!pngNotJpg)
                return "jpg";
            return "png";
        }
        internal static ImageFormat GetImageFormat(bool pngNotJpg)
        {
            if (!pngNotJpg)
                return ImageFormat.Jpeg;
            return ImageFormat.Png;
        }
        #endregion

        #region Checks and returns correct or default values
        internal static string CheckPath(string path, string defaultPath, ref FolderBrowserDialog browseDialog)
        {
            try
            {
                Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception)
            {
                DialogResult clickedOK = MessageBox.Show("Path Is Not Valid.", "Screenshots Not Being Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (clickedOK == DialogResult.OK)
                    if (browseDialog.ShowDialog() == DialogResult.OK)
                        return CheckPath(browseDialog.SelectedPath, defaultPath, ref browseDialog);
                return defaultPath;
            }
        }
        internal static string CheckName(string fileNameFormat)
        {
            if (fileNameFormat == "Title" || fileNameFormat == "Timestamp")
                return fileNameFormat;
            return "Title+Timestamp";
        }
        internal static string CheckTitle(string processTitle)
        {
            if (processTitle == "WindowTitle")
                return processTitle;
            return "ProcessName";
        }
        internal static string CheckFormat(string pngNotJpg)
        {
            if (pngNotJpg == "png")
                return pngNotJpg;
            return "jpg";
        }
        internal static string CheckSFX(string pathFolder, string SFX)
        {
            string[] stateSFX = SFX.Split('%');
            if (stateSFX.Length == 1)
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%DefaultSound";
                else
                    return "SFXdisabled%DefaultSound";
            else if (stateSFX.Length > 1 && File.Exists($"{pathFolder}\\{stateSFX[1]}"))
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%{stateSFX[1]}";
                else
                    return $"SFXdisabled%{stateSFX[1]}";
            else if (stateSFX.Length > 1 && stateSFX[1] == "DefaultSound")
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%{stateSFX[1]}";
            return "SFXdisabled%DefaultSound";
        }
        #endregion

        #region IO operations

        /// <summary>
        /// Writes a line in a file, at a specified line index.
        /// </summary>
        internal static void WriteLineIn(int lineIndex, string newLine, string[] inMemoryData, string writePath)
        {
            int i = 0;
            foreach (string line in inMemoryData)
            {
                if (lineIndex == i)
                {
                    inMemoryData[lineIndex] = newLine;
                    break;
                }
                i++;
            }
            File.WriteAllLines(writePath, inMemoryData);
        }
        internal static string GetFileIncrementName(string folder, string name, string ext)
        {
            string path = $"{folder}\\{name}.{ext}";
            if (!File.Exists(path))
                return path;
            else
            {
                for (int i = 1; i < int.MaxValue; i++)
                {
                    path = $"{folder}\\{name} ({i}).{ext}";
                    if (!File.Exists(path))
                    {
                        return path;
                    }
                }
            }
            return path + new Random().Next();
        }
        #endregion

        #region Obsolote/Deprecated
        //// takes double the memory (also probably iterates more)
        //internal string GetFileIncrementName(string path, string searchPattern)
        //{
        //    string folder = Path.GetDirectoryName(path);
        //    string name = Path.GetFileNameWithoutExtension(path);
        //    string ext = Path.GetExtension(path);
        //    string[] files = Directory.GetFiles(folder);
        //    int i = 0;
        //    foreach (string f in files)
        //    {
        //        if (Path.GetFileName(f).Substring(0, searchPattern.Length) == searchPattern)
        //            i++;
        //    }
        //    return $"{folder}\\{name} ({i}){ext}";
        //}

        //// Not used. similar methods already exist in .Net controls (While disabling AutoSize and enabling AutoEllipsis)
        //
        //internal string LeftStr(string fullStr, int maxLength)
        //{
        //    if (fullStr.Length > maxLength)
        //    {
        //        return fullStr.Substring(0, maxLength) + "...";
        //    }
        //    else
        //    {
        //        return fullStr;
        //    }
        //}
        //internal void ToolTipSetter(string str, int strMaxLength, ref ToolTip toolTip, Control ctrl)
        //{
        //    if (str.Length > strMaxLength)
        //        toolTip.SetToolTip(ctrl, str);
        //    else
        //        toolTip.SetToolTip(ctrl, "");
        //}
        #endregion
    }
}
