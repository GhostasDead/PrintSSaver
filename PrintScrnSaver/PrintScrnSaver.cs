using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gma.System.MouseKeyHook;
using System.Media;

namespace PrintSSaver
{
    public partial class PrintSSaver : Form
    {
        string appData = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\PrintSSaver\\Data.txt";
        string appFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\PrintSSaver";
        string specifiedSSlocation;
        string specifiedSSname;
        string specifiedSFX;
        SoundPlayer shutterSound;
        bool prefixProcessName;
        bool pngFormat;
        private IKeyboardMouseEvents hookPrntScrn;
        bool isHooked = false;
        private string[] defaultDataValues()
        {
            string defaultSSlocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\PrintSSaver";
            string defaultSSname = "Title+Timestamp";
            string defaultSStitle = "ProcessName";
            string defaultSSformat = "png";
            string defaultSFX = "SFXdisabled%DefaultSound";
            return new string[] { defaultSSlocation, defaultSSname, defaultSStitle, defaultSSformat, defaultSFX };
        }
        public PrintSSaver()
        {
            InitializeComponent();
            AppDataInit(appFolder, appData, defaultDataValues());
            LoadData();
            try
            {
                StartHook();
            }
            catch (Exception)
            {
                MessageBox.Show("Gma.System.MouseKeyHook.dll is not found.\n\nSaving screenshots using 'PrintScreen' key will not work.", "Missing DLL...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AppDataInit(string appDir, string dataFile, string[] defaultData)
        {
            if (File.Exists(dataFile))
                return;
            else if (Directory.Exists(appDir))
                File.WriteAllLines(dataFile, defaultData);
            else
            {
                Directory.CreateDirectory(appDir);
                File.WriteAllLines(dataFile, defaultData);
            }
        }
        private void LoadData()
        {
            string[] data = SaveData(appData, CheckFixData(defaultDataValues(), ReadDataTXT(appData)));

            specifiedSSlocation = data[0];
            specifiedSSname = data[1];
            prefixProcessName = ParseProcessName(data[2]);
            pngFormat = ParseImgFormat(data[3]);
            specifiedSFX = data[4];

            PopulateData(specifiedSSlocation, specifiedSSname, prefixProcessName, pngFormat, specifiedSFX);
        }
        public string[] ReadDataTXT(string fileLoc)
        {
            string[] data;
            data = File.ReadAllLines(fileLoc);
            return data;
        }
        private string[] CheckFixData(string[] defaultData, string[] data)
        {
            if (data.Length < 5)
            {
                if (data.Length > 0)
                    return CheckFixData(defaultData, new string[] { data[0], defaultData[1], defaultData[2], defaultData[3], defaultData[4] });
                else
                    return CheckFixData(defaultData, defaultData);
            }
            else if (data.Length > 5)
            {
                return CheckFixData(defaultData, data.Take(5).ToArray());
            }
            string[] inData = new string[5];
            inData[0] = CheckLocation(data[0], defaultData[0]);
            inData[1] = CheckName(data[1]);
            inData[2] = CheckTitle(data[2]);
            inData[3] = CheckFormat(data[3]);
            inData[4] = CheckSFX(data[4]);
            return inData;
        }
        private string CheckLocation(string location, string defaultLoc)
        {
            try
            {
                Directory.CreateDirectory(location);
                return location;
            }
            catch (Exception)
            {
                DialogResult clickedOK = MessageBox.Show("Path Is Not Valid.", "Screenshots Not Being Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (clickedOK == DialogResult.OK)
                    if (screenshotsLocation.ShowDialog() == DialogResult.OK)
                        return screenshotsLocation.SelectedPath;
                return defaultLoc;
            }
        }
        private string CheckName(string name)
        {
            if (name == "Title")
                return "Title";
            else if (name == "Timestamp")
                return "Timestamp";
            return "Title+Timestamp";
        }
        public string CheckTitle(string title)
        {
            if (title == "WindowTitle")
                return "WindowTitle";
            return "ProcessName";
        }
        public string CheckFormat(string format)
        {
            if (format == "jpg")
                return "jpg";
            return "png";
        }
        private string CheckSFX(string SFX)
        {
            string[] stateSFX = SFX.Split('%');
            if (stateSFX.Length == 1)
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%DefaultSound";
                else
                    return "SFXdisabled%DefaultSound";
            else if (stateSFX.Length > 1 && File.Exists($"{appFolder}\\{stateSFX[1]}"))
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%{stateSFX[1]}";
                else
                    return $"SFXdisabled%{stateSFX[1]}";
            else if (stateSFX.Length > 1 && stateSFX[1] == "DefaultSound")
                if (stateSFX[0] == "SFXenabled")
                    return $"{stateSFX[0]}%{stateSFX[1]}";
                else
                    return $"SFXdisabled%{stateSFX[1]}";
            return "SFXdisabled%DefaultSound";
        }
        public string[] SaveData(string fileLoc, string[] specifiedData)
        {
            File.WriteAllLines(fileLoc, specifiedData);
            return specifiedData;
        }
        private void SaveCapture(bool pngJpg, string FocusedWinFileName)
        {
            Image screenShot = null;
            try
            {
                if (!(screenShot = Clipboard.GetImage()).Equals(null))
                {
                    string imgName = getSSName(pngJpg, FocusedWinFileName);
                    if (!(imgName == null))
                        if (Directory.Exists(specifiedSSlocation))
                        {
                            screenShot.Save(imgName, GetImageFormat(pngJpg));
                            if (!(shutterSound == null))
                                shutterSound.Play();
                        }
                        else
                        {
                            Directory.CreateDirectory(specifiedSSlocation);
                            screenShot.Save(imgName, GetImageFormat(pngJpg));
                            if (!(shutterSound == null))
                                shutterSound.Play();
                        }
                    else
                        throw new Exception();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Windows was late to save data of screenshot to clipboard.", "No Image In Clipboard...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error.\n Application's data will be reseted.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }
            screenShot.Dispose();
        }
        private string getSSName(bool pngJpg, string FocusedWinFileName)
        {
            string fileName;
            string fileExt = StringParseImgFormat(pngJpg);
            string dateNow = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            if (specifiedSSname == "TimeStamp")
                fileName = $"{specifiedSSlocation}\\{dateNow}";
            else if (specifiedSSname == "Title")
                fileName = $"{specifiedSSlocation}\\{FocusedWinFileName}";
            else
                fileName = $"{specifiedSSlocation}\\{FocusedWinFileName}_{dateNow}";

            if (!File.Exists($"{fileName}.{fileExt}"))
            {
                return $"{fileName}.{fileExt}";
            }
            else
            {
                for (ulong i = 1; i < ulong.MaxValue; i++)
                {
                    string indexedFileName = $"{fileName} ({i}).{fileExt}";
                    if (!File.Exists(indexedFileName))
                        return indexedFileName;
                }
            }
            return null;
        }
        private void SaveClipboard(bool pngJpg)
        {
            Image screenShot = null;
            try
            {
                if (!(screenShot = Clipboard.GetImage()).Equals(null))
                {
                    string imgName = getSSName(pngJpg, "Clipboard");
                    if (Directory.Exists(specifiedSSlocation))
                    {
                        screenShot.Save(imgName, GetImageFormat(pngJpg));
                        if (!(shutterSound == null))
                            shutterSound.Play();
                    }
                    else
                    {
                        Directory.CreateDirectory(specifiedSSlocation);
                        screenShot.Save(imgName, GetImageFormat(pngJpg));
                        if (!(shutterSound == null))
                            shutterSound.Play();
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Copy an image to clipboard or\nPress 'PrintScreen' on your keyboard to take a screenshot.", "No Image In Clipboard...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error.\n Application's data will be reseted.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }
            screenShot.Dispose();
        }
        public string FocusedWindow(bool processName)
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return "Desktop";
            }
            uint pid;
            GetWindowThreadProcessId(activatedHandle, out pid);
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == pid)
                {
                    if (processName)
                        return p.ProcessName;
                    return string.IsNullOrEmpty(p.MainWindowTitle) ? p.ProcessName : p.MainWindowTitle;
                }
            }
            return "Desktop";
        }
        public ImageFormat GetImageFormat(bool pngJpg)
        {
            if (pngJpg)
                return ImageFormat.Png;
            return ImageFormat.Jpeg;
        }
        private void location_Click(object sender, EventArgs e)
        {
            screenshotsLocation.SelectedPath = specifiedSSlocation;
            if (screenshotsLocation.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(appData, new string[] { screenshotsLocation.SelectedPath, StringParseProcessName(prefixProcessName), StringParseImgFormat(pngFormat) });
                PopulateLocation(screenshotsLocation.SelectedPath);
            }
        }
        private void openFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", specifiedSSlocation);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Can't Access Folder", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveClipboard_Click(object sender, EventArgs e)
        {
            SaveClipboard(pngFormat);
        }
        private void disActivateBtn_Click(object sender, EventArgs e)
        {
            if (isHooked)
                StopHook();
            else
                try
                {
                    StartHook();
                }
                catch (Exception)
                {
                    MessageBox.Show("Gma.System.MouseKeyHook.dll is not found.\nSaving screenshots using 'PrintScreen' key will not work.", "Missing DLL...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        private void nameRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioBtn = (RadioButton)sender;
            if (radioBtn != null)
                if (radioBtn.Checked)
                {
                    File.WriteAllLines(appData, new string[] { specifiedSSlocation, radioBtn.Text, StringParseProcessName(prefixProcessName), StringParseImgFormat(pngFormat), specifiedSFX });
                    specifiedSSname = radioBtn.Text;
                    if (!(radioBtn.Name == "timestampRadio"))
                        titlePanel.Enabled = true;
                    else
                        titlePanel.Enabled = false;
                }
        }
        private void radioTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPName.Checked)
            {
                File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, "ProcessName", StringParseImgFormat(pngFormat), specifiedSFX });
                prefixProcessName = true;
            }
            else
            {
                File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, "WindowTitle", StringParseImgFormat(pngFormat), specifiedSFX });
                prefixProcessName = false;
            }
        }
        private void formatRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPngFormat.Checked)
            {
                File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, StringParseProcessName(prefixProcessName), "png", specifiedSFX });
                pngFormat = true;
            }
            else
            {
                File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, StringParseProcessName(prefixProcessName), "jpg", specifiedSFX });
                pngFormat = false;
            }
        }
        private void sfxChkBox_CheckedChanged(object sender, EventArgs e)
        {
            string[] stateSFX = specifiedSFX.Split('%');
            if (sfxChkBox.Checked)
                specifiedSFX = CheckSFX($"SFXenabled%{stateSFX[1]}");
            else
                specifiedSFX = CheckSFX($"SFXdisabled%{stateSFX[1]}");
            File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, StringParseProcessName(prefixProcessName), StringParseImgFormat(pngFormat), specifiedSFX });
            PopulateSFX(specifiedSFX);
        }
        private void chooseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog wavDialog = new OpenFileDialog();
            wavDialog.Title = "Select screenshot SFX audio";
            wavDialog.DefaultExt = "wav";
            wavDialog.Filter = "Audio Files(*.wav)|*.wav";
            wavDialog.CheckFileExists = true;
            wavDialog.CheckPathExists = true;
            if (DialogResult.OK == wavDialog.ShowDialog())
            {
                string oldSFX = $"{appFolder}\\specifiedSFX.Split('%')[1]";
                if (File.Exists(oldSFX))
                    File.Delete(oldSFX);
                string newSFX = $"{appFolder}\\{Path.GetFileName(wavDialog.FileName)}";
                File.Copy(wavDialog.FileName, newSFX, true);
                specifiedSFX = CheckSFX($"SFXenabled%{Path.GetFileName(newSFX)}");
                File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, StringParseProcessName(prefixProcessName), StringParseImgFormat(pngFormat), specifiedSFX });
                PopulateSFX(specifiedSFX);
            }
        }
        private void defaultSFXbtn_Click(object sender, EventArgs e)
        {
            string defaultSFX = defaultDataValues()[4];
            specifiedSFX = defaultSFX;
            File.WriteAllLines(appData, new string[] { specifiedSSlocation, specifiedSSname, StringParseProcessName(prefixProcessName), StringParseImgFormat(pngFormat), defaultSFX });
            PopulateSFX(defaultSFX);
        }
        public void StartHook()
        {
            hookPrntScrn = Hook.GlobalEvents();
            hookPrntScrn.KeyUp += GlobalHookKeyPress;
            isHooked = true;
            ActivateBtn.Text = "Deactivate ";
            deactivate.Text = "Deactivate ";
            this.Text = "PrintSSaver";
            inTray.Text = "PrintSSaver";
        }
        private void GlobalHookKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PrintScreen)
            {
                string FocusedWinFileName = string.Join("_", FocusedWindow(prefixProcessName).Split(Path.GetInvalidFileNameChars()));
                SaveCapture(pngFormat, FocusedWinFileName);
            }
        }
        public void StopHook()
        {
            hookPrntScrn.KeyUp -= GlobalHookKeyPress;
            isHooked = false;
            hookPrntScrn.Dispose();
            ActivateBtn.Text = "Activate";
            deactivate.Text = "Activate ";
            this.Text = "PrintSSaver (Inactive)";
            inTray.Text = "PrintSSaver (Inactive)";
        }
        private void PrintSSaver_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                inTray.Visible = true;
            }
        }
        private void inTray_Click(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            inTray.Visible = false;
        }
        private void PrinScrnSaver_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isHooked)
                StopHook();
        }
        private void exit_Click(object sender, EventArgs e)
        {
            if (isHooked)
                StopHook();
            this.Close();
        }
        private void PopulateData(string specifiedSSLoc, string specifiedSSname, bool prefixProcessName, bool pngFormat, string specifiedSFX)
        {
            PopulateLocation(specifiedSSLoc);
            PopulateName(specifiedSSname);
            PopulateTitle(prefixProcessName);
            PopulateImgFormat(pngFormat);
            PopulateSFX(specifiedSFX);
        }
        private void PopulateLocation(string path)
        {
            this.specifiedSSlocation = path;
            locationLabel.Text = path;
        }
        private void PopulateName(string name)
        {
            if (name == "Title")
                titleRadio.Checked = true;
            else if (name == "Timestamp")
                timestampRadio.Checked = true;
            titleNtimestampRadio.Checked = true;
        }
        private void PopulateTitle(bool title)
        {
            if (title)
                radioPName.Checked = true;
            else radioWName.Checked = true;
        }
        private void PopulateImgFormat(bool isPng)
        {
            if (isPng)
                radioPngFormat.Checked = true;
            else radioJpegFormat.Checked = true;
        }
        private void PopulateSFX(string SFX)
        {
            string[] SFXstate = SFX.Split('%');
            if (!(SFXstate[0] == "SFXdisabled"))
            {
                if (!(SFXstate[1] == "DefaultSound"))
                {
                    shutterSound = new SoundPlayer($"{appFolder}\\{SFXstate[1]}");
                }
                else
                {
                    shutterSound = new SoundPlayer(Properties.Resources.shutter);
                }
                sfxChkBox.Checked = true;
                chooseBtn.Enabled = true;
                defaultSFXbtn.Enabled = true;
            }
            else
            {
                shutterSound = null;
                sfxChkBox.Checked = false;
                chooseBtn.Enabled = false;
                if (!(SFXstate[1] == "DefaultSound"))
                {
                    defaultSFXbtn.Enabled = true;
                }
                else
                {
                    defaultSFXbtn.Enabled = false;
                }
            }
            sfxChkBox.Text = SFXstate[1];
        }
        public bool ParseProcessName(string PrefixPName)
        {
            if (PrefixPName == "ProcessName")
                return true;
            return false;
        }
        public string StringParseProcessName(bool PrefixPName)
        {
            if (PrefixPName)
                return "ProcessName";
            return "WindowTitle";
        }
        public bool ParseImgFormat(string imegPng)
        {
            if (imegPng == "png")
                return true;
            return false;
        }
        public string StringParseImgFormat(bool imegPng)
        {
            if (imegPng)
                return "png";
            return "jpg";
        }
        //public string LeftStr(string fullStr, int maxLength)
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
        //public void ToolTipSetter(string str, int strMaxLength, ref ToolTip toolTip, Control ctrl)
        //{
        //    if (str.Length > strMaxLength)
        //        toolTip.SetToolTip(ctrl, str);
        //    else
        //        toolTip.SetToolTip(ctrl, "");
        //}
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);

        private void aboutLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://t.me/GhostasDead");
        }
    }
}