using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using static yt_downloaders.youTubeManager;

namespace yt_downloaders
{
    public partial class mainForm : Form
    {
        public string defaultTitle = null;
        public string fetchedURL = null;
        public Dictionary<string, string> codecNames = null;
        public Dictionary<string, string> resolutionNames = null;

        private infoFetcher _fetcher = new infoFetcher();

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (!isytdlpInstalled())
            {
                statusWaiting.BackColor = Color.LightCoral;
                statusWaiting.Text = "Could not detect yt-dlp; please download it by clicking this text";
                statusWaiting.Cursor = Cursors.Hand;
                statusWaiting.Tag = "ytdlplink";

                inputURL.Enabled = false;
                return;
            }

            defaultTitle = Text;
            chkHighestAvailableSettings.Enabled = false;
            dropdownQuality.Enabled = false;
            dropdownFormat.Enabled = false;
            dropdownCodec.Enabled = false;
            inputURL.Enabled = true;
            codecNames = new Dictionary<string, string>();

            statusWaiting.Select();
        }

        private bool isytdlpInstalled()
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "yt-dlp";
                process.StartInfo.Arguments = "--version";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return process.ExitCode == 0 && !string.IsNullOrEmpty(output);
            }
            catch
            {
                return false;
            }
        }

        private string simplifyCodecName(string codec)
        {
            if (codec.StartsWith("avc1.64")) return "H.264 (High Quality)";
            else if (codec.StartsWith("avc1.4D")) return "H.264 (Standard)";
            else if (codec.StartsWith("avc1.42")) return "H.264 (Compatible)";
            else if (codec.StartsWith("vp09")) return "VP9 (Best Quality)";
            else return null;
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void inputURL_TextChanged(object sender, EventArgs e)
        {

        }

        private async void inputURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the ding sound on Enter key press
                e.Handled = true; // Mark the event as handled

                string url = inputURL.Text.Trim();
                if (!string.IsNullOrEmpty(url))
                {
                    if (!url.StartsWith("https://www.youtube.com/"))
                    {
                        MessageBox.Show("Please input a valid YouTube link.", Text, MessageBoxButtons.OK);
                    }
                    else
                    {
                        try
                        {
                            // clear and reset controls before fetching
                            fetchedURL = url;

                            inputURL.Clear();
                            dropdownQuality.Items.Clear();
                            dropdownFormat.Items.Clear();
                            dropdownCodec.Items.Clear();
                            codecNames.Clear();

                            string formattedURL = url.Trim().Replace("https://www.youtube.com/", string.Empty);
                            Text = defaultTitle + " - fetching " + formattedURL;
                            chkHighestAvailableSettings.Enabled = false;
                            dropdownQuality.Enabled = false;
                            dropdownFormat.Enabled = false;
                            dropdownCodec.Enabled = false;
                            statusVideoTitle.Visible = false;
                            panelDownload.Visible = false;
                            statusWaiting.Visible = true;

                            statusWaiting.Text = "Fetching from URL, please wait";
                            statusWaiting.BackColor = Color.Silver;

                            youTubeManager _manager = new youTubeManager();
                            var videoInfo = await _manager.getVideoInfo(url);
                            if (videoInfo == null)
                            {
                                MessageBox.Show("Invalid link provided, please try another one.", Text, MessageBoxButtons.OK);
                                inputURL.Text = fetchedURL; // restore the input URL if it errors out
                                return;
                            }

                            // fetch success

                            Text = defaultTitle + " - previewing " + formattedURL;
                            chkHighestAvailableSettings.Enabled = true;

                            if (chkHighestAvailableSettings.Checked)
                            {
                                dropdownQuality.Enabled = false;
                                dropdownFormat.Enabled = false;
                                dropdownCodec.Enabled = false;
                            }
                            else
                            {
                                dropdownQuality.Enabled = true;
                                dropdownFormat.Enabled = true;
                                dropdownCodec.Enabled = true;
                            }

                            statusVideoTitle.Visible = true;
                            panelDownload.Visible = true;

                            statusWaiting.Text = "Fetch success, waiting to download";
                            statusWaiting.BackColor = Color.Silver;

                            dropdownQuality.Items.AddRange(videoInfo.getAvailableResolutions().ToArray());
                            dropdownFormat.Items.AddRange(videoInfo.getAvailableFormats().ToArray());

                            var codecInfos = videoInfo.getAvailableCodecs();
                            foreach (string codec in videoInfo.getAvailableCodecs())
                            {
                                string originalCodec = codec;
                                string simplifiedCodec = simplifyCodecName(codec);
                                if (simplifiedCodec != null && !dropdownCodec.Items.Contains(simplifiedCodec))
                                {
                                    dropdownCodec.Items.Add(simplifiedCodec);
                                    codecNames.Add(simplifiedCodec, originalCodec);
                                }
                            }

                            if (dropdownQuality.Items.Count > 0)
                                dropdownQuality.SelectedIndex = 0;

                            if (dropdownFormat.Items.Count > 0)
                                dropdownFormat.SelectedIndex = 0;

                            if (dropdownCodec.Items.Count > 0)
                                dropdownCodec.SelectedIndex = 0;

                            statusTitle.Text = videoInfo.Title;

                            titleStrip.SetToolTip(statusTitle, videoInfo.Title);
                            statusTitle.Tag = fetchedURL;
                        }
                        catch (Exception ex){}
                    }
                }
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            string downloadsPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string formattedURL = fetchedURL.Trim().Replace("https://www.youtube.com/", string.Empty);
            string videoNameTrimmed = statusTitle.Text;
            string videoName = $"{videoNameTrimmed}.%(ext)s";
            Text = defaultTitle + " - downloading " + formattedURL;

            string ytdlp_arg = null;

            string fullPathMP4 = Path.Join(downloadsPath, videoNameTrimmed + ".mp4");
            string fullPathWEBM = Path.Join(downloadsPath, videoNameTrimmed + ".webm");
            string fullPathMKV = Path.Join(downloadsPath, videoNameTrimmed + ".mkv");
            string fullPathMOV = Path.Join(downloadsPath, videoNameTrimmed + ".mov");

            // checking most common video extensions to
            // detect whether the video the user is about
            // to download already exists on disk

            bool fullPathMP4Exists = File.Exists(fullPathMP4);
            bool fullPathWEBMExists = File.Exists(fullPathWEBM);
            bool fullPathMKVExists = File.Exists(fullPathMKV);
            bool fullPathMOVExists = File.Exists(fullPathMOV);

            if (fullPathMP4Exists)
            {
                if (MessageBox.Show("\"" + videoNameTrimmed + ".mp4\" already exists on disk, do you want to overwrite it?", Text, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return; // user chose to not overwrite the file on disk
                }
            }
            if (fullPathWEBMExists)
            {
                if (MessageBox.Show("\"" + videoNameTrimmed + ".webm\" already exists on disk, do you want to overwrite it?", Text, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return; // user chose to not overwrite the file on disk
                }
            }
            if (fullPathMKVExists)
            {
                if (MessageBox.Show("\"" + videoNameTrimmed + ".mkv\" already exists on disk, do you want to overwrite it?", Text, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return; // user chose to not overwrite the file on disk
                }
            }
            if (fullPathMOVExists)
            {
                if (MessageBox.Show("\"" + videoNameTrimmed + ".mov\" already exists on disk, do you want to overwrite it?", Text, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return; // user chose to not overwrite the file on disk
                }
            }

            statusWaiting.Visible = true;
            statusWaiting.Text = "Starting download, please wait";
            statusWaiting.BackColor = Color.Silver;

            if (chkHighestAvailableSettings.Checked)
            {
                string formatOption = "-f \"bestvideo+bestaudio/best\"";
                ytdlp_arg = $"{formatOption} -o \"{downloadsPath}\\{videoName}\" \"{fetchedURL}\"";
            }
            else
            {
                if (dropdownCodec.Items.Count > -1)
                {
                    if (dropdownCodec.SelectedItem != null)
                    {
                        var selectedCodecInfo = (CodecInformation)dropdownCodec.SelectedItem;
                        if (selectedCodecInfo == null)
                        {
                            MessageBox.Show("Please select a codec before downloading.", Text, MessageBoxButtons.OK);
                            return;
                        }

                        string? preferredExt = dropdownQuality.SelectedItem?.ToString();
                        string? preferredFormat = dropdownFormat.SelectedItem?.ToString();

                        if (preferredExt != null && preferredFormat != null)
                        {
                            string? maxHeight = preferredFormat.Replace("p", string.Empty);
                            string audioExt = preferredFormat;

                            string originalCodecName = selectedCodecInfo.originalName;
                            ytdlp_arg = $"-f \"bestvideo[vcodec^={originalCodecName}][ext={preferredExt}][height<={maxHeight}]+bestaudio[ext={audioExt}]\" -o \"{downloadsPath}\\{videoName}\" \"{fetchedURL}\"";
                        }
                    }
                }
            }

            await Task.Run(() =>
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "yt-dlp";
                    process.StartInfo.Arguments = ytdlp_arg;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.OutputDataReceived += (s, ev) =>
                    {
                        if (!string.IsNullOrEmpty(ev.Data) && ev.Data.Contains("[download]"))
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                statusWaiting.Text = ev.Data; // update status with live progress
                                statusWaiting.BackColor = Color.Silver;
                            }));
                        }
                    };

                    process.ErrorDataReceived += (s, ev) =>
                    {
                        if (!string.IsNullOrEmpty(ev.Data))
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                statusWaiting.Text = "Error: " + ev.Data;
                                statusWaiting.BackColor = Color.LightCoral;
                            }));
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
            });

            statusWaiting.Text = "Download complete!";
            statusWaiting.BackColor = Color.MediumSeaGreen;

            if (chkOpenFolderOnFinish.Checked)
            {
                try
                {
                    Process.Start("explorer.exe", downloadsPath);
                }
                catch (Exception ex){}
            }
        }

        private void chkHighestAvailableSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHighestAvailableSettings.Checked)
            {
                dropdownQuality.Enabled = false;
                dropdownFormat.Enabled = false;
                dropdownCodec.Enabled = false;
            }
            else
            {
                dropdownQuality.Enabled = true;
                dropdownFormat.Enabled = true;
                dropdownCodec.Enabled = true;
            }
        }

        private void statusTitle_MouseEnter(object sender, EventArgs e)
        {
            statusTitle.Font = new Font(statusTitle.Font.OriginalFontName, statusTitle.Font.Size, FontStyle.Underline);
        }

        private void statusTitle_MouseLeave(object sender, EventArgs e)
        {
            statusTitle.Font = new Font(statusTitle.Font.OriginalFontName, statusTitle.Font.Size, FontStyle.Regular);
        }

        private void statusTitle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Open link: " + fetchedURL + "?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenUrl(fetchedURL);
            }
        }

        private void statusWaiting_MouseDown(object sender, MouseEventArgs e)
        {
            if (statusWaiting.Tag?.ToString() == "ytdlplink")
            {
                OpenUrl("https://github.com/yt-dlp/yt-dlp/releases");
                Application.Exit();
            }
        }

        private void panelInputURL_MouseDown(object sender, MouseEventArgs e)
        {
            inputURL.Select();
        }

        private void panelInputURLColorBar_MouseDown(object sender, MouseEventArgs e)
        {
            inputURL.Select();
        }
    }
}
