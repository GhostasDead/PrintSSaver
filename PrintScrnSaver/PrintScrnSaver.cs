using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gma.System.MouseKeyHook;
using System.Media;
using static PrintSSaver.AppDefaultProberties;
using System.Drawing.Imaging;

namespace PrintSSaver
{
    public partial class PrintSSaver : Form
    {
        private string appDir, appDataFile, screenshotsSavePath;

        // bool and byte variables, in a way to let the operations proceed as fast as possible.
        // converted when loading and saving data in Data.txt, and in population methods
        byte ssNameAppends;
        private bool proccesNameNotWindowTitle, pngNotJpg;

        // "...%..." string used with split('%') to manipulate one proberty and be saved as one line app's settings
        // first proberty to indicate if SFX is enabled "SFXenabled%..." or disabled "SFXdisabled%..."
        // the second is SFX's fileName in appDir "...%shutter.wav" (choosen sfx gets copied to app's folder in "%appdata$\local"), or if SFX is app's default shutter sound "...%DefaultSound"
        private string sfxParameters;
        private SoundPlayer shutterSound;
        ImageFormat pngOrJpgImgFormat;
        private IKeyboardMouseEvents hookPrntScrn;
        private bool isHooked;
        public PrintSSaver()
        {
            InitializeComponent();
            appDir = GetAppDir(this.Name);
            appDataFile = GetSettingsFile(appDir, "Data.txt");
            string[] defaultData = DefaultDataValues();
            AppDataInit(appDir, appDataFile, defaultData);
            LoadData(appDataFile, defaultData);
            try
            {
                StartHook();
            }
            catch (Exception)
            {
                MessageBox.Show("Gma.System.MouseKeyHook.dll is not found.\n\nSaving screenshots using 'PrintScreen' key will not work.", "Missing DLL...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopHook();
            }
        }
        #region Initialize, check and save app's settings
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
        private void LoadData(string appDataFile, string[] defalutData)
        {
            string[] readData = File.ReadAllLines(appDataFile);
            string[] data = SaveData(appDataFile, CheckFixData(defalutData, readData), readData);

            //the app rely on this variables when operating
            screenshotsSavePath = data[0];
            ssNameAppends = Converty.ByteAppends(data[1]);
            proccesNameNotWindowTitle = Converty.BoolTitle(data[2]);
            pngNotJpg = Converty.BoolImgFormat(data[3]);
            pngOrJpgImgFormat = Converty.GetImageFormat(pngNotJpg);
            sfxParameters = data[4];

            PopulateData(screenshotsSavePath, ssNameAppends, proccesNameNotWindowTitle, pngNotJpg, sfxParameters);
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
            inData[0] = Converty.CheckPath(data[0], defaultData[0], ref screenshotsLocation);
            inData[1] = Converty.CheckName(data[1]);
            inData[2] = Converty.CheckTitle(data[2]);
            inData[3] = Converty.CheckFormat(data[3]);
            inData[4] = Converty.CheckSFX(appDir, data[4]);
            return inData;
        }
        public string[] SaveData(string filePath, string[] settings, string[] readData)
        {
            bool sameSettings;
            try
            {
                sameSettings = readData.SequenceEqual(settings);
            }
            catch (ArgumentNullException)
            {
                sameSettings = false;
            }
            if (!sameSettings)
                File.WriteAllLines(filePath, settings);
            return settings;
        }
        #endregion

        #region Populate data to controls
        private void PopulateData(string ssPath, byte ssNameFormat, bool ProcessNameOrTitle, bool pngNotJpg, string SFX)
        {
            PopulateLocation(ssPath);
            PopulateName(ssNameFormat);
            PopulateTitle(ProcessNameOrTitle);
            PopulateImgFormat(pngNotJpg);
            PopulateSFX(SFX);
        }
        private void PopulateLocation(string path)
        {
            locationLabel.Text = path;
        }
        private void PopulateName(byte name)
        {
            if (name == 0)
            {
                timestampRadio.Checked = true;
                titlePanel.Enabled = false;
            }
            else if (name == 1)
            {
                titleRadio.Checked = true;
            }

            else
                titleNtimestampRadio.Checked = true;
        }
        private void PopulateTitle(bool title)
        {
            if (title)
                radioPName.Checked = true;
            else
                radioWName.Checked = true;
            proccesNameNotWindowTitle = title;
        }
        private void PopulateImgFormat(bool pngNotJpg)
        {

            if (pngNotJpg)
                radioPngFormat.Checked = true;
            else
                radioJpegFormat.Checked = true;
        }

        private void PopulateSFX(string SFX)
        {
            string[] SFXstate = SFX.Split('%');
            if (SFXstate[0] != "SFXdisabled")
            {
                if (SFXstate[1] != "DefaultSound")
                {
                    shutterSound = new SoundPlayer($"{appDir}\\{SFXstate[1]}");
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
                if (SFXstate[1] != "DefaultSound")
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
        #endregion

        #region Hooking keyboard and Capturing screenshots

        public void StartHook()
        {
            hookPrntScrn = Hook.GlobalEvents();
            hookPrntScrn.KeyUp += PrintScreenDetect;
            isHooked = true;

            ActivateBtn.Text = "Deactivate";
            activateItem.Text = ActivateBtn.Text;
            this.Text = "PrintSSaver";
            inTray.Text = this.Text;
            inTray.BalloonTipText = "Capturing 'PrintScreen'";
            inTray.BalloonTipTitle = string.Empty;
            inTray.BalloonTipIcon = ToolTipIcon.None;
        }
        private void PrintScreenDetect(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PrintScreen)
            {
                SaveCapture();
            }
        }
        public void StopHook()
        {
            hookPrntScrn.KeyUp -= PrintScreenDetect;
            isHooked = false;
            hookPrntScrn.Dispose();
            ActivateBtn.Text = "Activate";
            activateItem.Text = ActivateBtn.Text;
            this.Text = "PrintSSaver (Inactive)";
            inTray.Text = this.Text;
            inTray.BalloonTipText = "Not Capturing 'PrintScreen'";
            inTray.BalloonTipTitle = "Deactivated";
            inTray.BalloonTipIcon = ToolTipIcon.Warning;
        }
        private void SaveCapture()
        {
            Image screenShot;
            string imgName = GetScreenshotName(pngNotJpg);
            try
            {
                screenShot = Clipboard.GetImage();
                screenShot.Save(imgName, pngOrJpgImgFormat);
                if (shutterSound != null)
                    shutterSound.Play();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Windows was unable/late to save data of screenshot to clipboard.\n", "No Image In Clipboard...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error.\n Application's data will be checked and/or reseted.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string appData = GetSettingsFile(GetAppDir(this.Name), "Data.txt");
                LoadData(appData, DefaultDataValues());
                return;
            }
            screenShot.Dispose();
        }
        private string GetScreenshotName(bool pngNotJpg)
        {
            string fileName;
            if (ssNameAppends == 0)
            {
                var dateNow = DateTime.Now;
                fileName = $"{dateNow:yyyy.MM.dd_HH.mm.ss}";
            }
            else
            {
                string FocusedWinFileName = string.Join("_", FocusedWindow(proccesNameNotWindowTitle).Split(Path.GetInvalidFileNameChars()));
                if (ssNameAppends == 1)
                    fileName = $"{FocusedWinFileName}";
                else
                {
                    var dateNow = DateTime.Now;
                    fileName = $"{FocusedWinFileName}_{dateNow:yyyy.MM.dd_HH.mm.ss}";
                }
            }
            string fileExt = Converty.StringImgFormat(pngNotJpg);
            try
            {
                return Converty.GetFileIncrementName(screenshotsSavePath, fileName, fileExt);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(screenshotsSavePath);
                return Converty.GetFileIncrementName(screenshotsSavePath, fileName, fileExt);
            }
            catch (Exception)
            {
                return $"Screenshot_{DateTime.Now:yyyy.MMdd_HH.mm.ss.ffffff}.{fileExt}";
            }
        }

        private void SaveClipboard()
        {
            Image screenShot;
            string imgName = GetScreenshotName(pngNotJpg, true);
            try
            {
                screenShot = Clipboard.GetImage();
                screenShot.Save(imgName, pngOrJpgImgFormat);
                if (shutterSound != null)
                    shutterSound.Play();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Copy an image to clipboard or\nPress 'PrintScreen' on your keyboard to take a screenshot.", "No Image In Clipboard...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error.\n Application's data will be checked and/or reseted.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string appData = GetSettingsFile(GetAppDir(this.Name), "Data.txt");
                LoadData(appData, DefaultDataValues());
                return;
            }
            screenShot.Dispose();
        }
        private string GetScreenshotName(bool pngNotJpg, bool clipboard)
        {
            string fileName;
            if (ssNameAppends == 0) //Timestamp
            {
                fileName = $"{DateTime.Now:yyyy.MM.dd_HH.mm.ss}";
            }
            else
            {
                if (ssNameAppends == 1) //Title
                    fileName = "Clipboard";
                else //Title+Timestamp
                    fileName = $"Clipboard_{DateTime.Now:yyyy.MM.dd_HH.mm.ss}";
            }
            string fileExt = Converty.StringImgFormat(pngNotJpg);
            try
            {
                return Converty.GetFileIncrementName(screenshotsSavePath, fileName, fileExt);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(screenshotsSavePath);
                return Converty.GetFileIncrementName(screenshotsSavePath, fileName, fileExt);
            }
            catch (Exception)
            {
                return $"Screenshot_{DateTime.Now:yyyy.MMdd_HH.mm.ss.ffffff}.{fileExt}";
            }
        }
        public string FocusedWindow(bool proccesNameNotWindowTitle)
        {
            IntPtr activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return "Screenshot";
            }
            uint processId;
            GetWindowThreadProcessId(activatedHandle, out processId);
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == processId)
                {
                    if (proccesNameNotWindowTitle)
                        return p.ProcessName;
                    string windowTitle = p.MainWindowTitle;
                    return string.IsNullOrEmpty(windowTitle) ? "Screenshot" : windowTitle;
                }
            }
            return "Screenshot";
        }
        #endregion

        #region Form controls interactions

        private void browse_Click(object sender, EventArgs e)
        {
            screenshotsLocation.SelectedPath = screenshotsSavePath;
            if (screenshotsLocation.ShowDialog() == DialogResult.OK)
            {
                Converty.WriteLineIn(0, screenshotsLocation.SelectedPath, MemoryDataValues(), appDataFile);
                this.screenshotsSavePath = screenshotsLocation.SelectedPath;
                PopulateLocation(screenshotsLocation.SelectedPath);
            }
        }
        public string[] MemoryDataValues()
        {
            // App's specific current in-memory values.
            return new string[] { screenshotsSavePath, Converty.StringAppends(ssNameAppends), Converty.StringTitle(proccesNameNotWindowTitle), Converty.StringImgFormat(pngNotJpg), sfxParameters };
        }
        private void openFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", screenshotsSavePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Access Folder", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveClipboard_Click(object sender, EventArgs e)
        {
            SaveClipboard();
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
                    StopHook();
                }
        }
        private void titleNtimestampRadio_Click(object sender, EventArgs e)
        {
            Converty.WriteLineIn(1, titleNtimestampRadio.Text, MemoryDataValues(), appDataFile);
            ssNameAppends = Converty.ByteAppends(titleNtimestampRadio.Text);
            titlePanel.Enabled = true;
        }
        private void titleRadio_Click(object sender, EventArgs e)
        {
            Converty.WriteLineIn(1, titleRadio.Text, MemoryDataValues(), appDataFile);
            ssNameAppends = Converty.ByteAppends(titleRadio.Text);
            titlePanel.Enabled = true;
        }

        private void timestampRadio_Click(object sender, EventArgs e)
        {
            Converty.WriteLineIn(1, timestampRadio.Text, MemoryDataValues(), appDataFile);
            ssNameAppends = Converty.ByteAppends(timestampRadio.Text);
            titlePanel.Enabled = false;
        }
        private void radioPName_Click(object sender, EventArgs e)
        {
            proccesNameNotWindowTitle = true;
            Converty.WriteLineIn(2, "ProcessName", MemoryDataValues(), appDataFile);
        }
        private void radioWName_Click(object sender, EventArgs e)
        {
            proccesNameNotWindowTitle = false;
            Converty.WriteLineIn(2, "WindowTitle" , MemoryDataValues(), appDataFile);
        }
        private void radioPngFormat_Click(object sender, EventArgs e)
        {
            pngNotJpg = true;
            pngOrJpgImgFormat = Converty.GetImageFormat(pngNotJpg);
            Converty.WriteLineIn(3, radioPngFormat.Text.ToLower(), MemoryDataValues(), appDataFile);
        }
        private void radioJpegFormat_Click(object sender, EventArgs e)
        {
            pngNotJpg = false;
            pngOrJpgImgFormat = Converty.GetImageFormat(pngNotJpg);
            Converty.WriteLineIn(3, radioJpegFormat.Text.ToLower(), MemoryDataValues(), appDataFile);
        }
        private void sfxChkBox_Click(object sender, EventArgs e)
        {
            string[] SFXstate = sfxParameters.Split('%');
            if (!sfxChkBox.Checked)
            {
                sfxParameters = $"SFXdisabled%{SFXstate[1]}";
                shutterSound = null;
                chooseBtn.Enabled = false;
                if (SFXstate[1] != "DefaultSound")
                {
                    defaultSFXbtn.Enabled = true;
                }
                else
                {
                    defaultSFXbtn.Enabled = false;
                }
            }
            else
            {
                sfxParameters = Converty.CheckSFX(appDir, $"SFXenabled%{SFXstate[1]}");
                if (SFXstate[1] != "DefaultSound")
                {
                    shutterSound = new SoundPlayer($"{appDir}\\{sfxParameters}");
                }
                else
                {
                    shutterSound = new SoundPlayer(Properties.Resources.shutter);
                }
                chooseBtn.Enabled = true;
                defaultSFXbtn.Enabled = true;
            }
            Converty.WriteLineIn(4, sfxParameters, MemoryDataValues(), appDataFile);
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
                shutterSound.Dispose();
                string oldSFX = $"{appDir}\\{sfxParameters.Split('%')[1]}";
                if (File.Exists(oldSFX))
                    File.Delete(oldSFX);
                string newSFXname = Path.GetFileName(wavDialog.FileName);
                string newSFXpath = $"{appDir}\\{newSFXname}";
                File.Copy(wavDialog.FileName, newSFXpath, true);
                sfxParameters = $"SFXenabled%{newSFXname}";
                shutterSound = new SoundPlayer(newSFXpath);
                sfxChkBox.Text = newSFXname;
                Converty.WriteLineIn(4, sfxParameters, MemoryDataValues(), appDataFile);
            }
        }
        private void defaultSFXbtn_Click(object sender, EventArgs e)
        {
            string defaultSFX = "SFXdisabled%DefaultSound";
            sfxParameters = defaultSFX;
            shutterSound = null;
            sfxChkBox.Checked = false;
            chooseBtn.Enabled = false;
            defaultSFXbtn.Enabled = false;
            sfxChkBox.Text = "DefaultSound";
            Converty.WriteLineIn(4, defaultSFX, MemoryDataValues(), appDataFile);
        }
        private void PrintSSaver_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                inTray.Visible = true;
                inTray.ShowBalloonTip(3000);
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
                hookPrntScrn.Dispose();
        }
        private void exit_Click(object sender, EventArgs e)
        {
            if (isHooked)
                hookPrntScrn.Dispose();
            this.Close();
        }
        private void aboutLabel_Click(object sender, EventArgs e)
        {
            // opens Github repo in browser
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/GhostasDead/PrintSSaver");
        }
        #endregion

        #region DllImports to get processes proberties (for FocusedWindow method)
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
        #endregion
    }
}