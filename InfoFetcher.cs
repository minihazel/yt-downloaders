using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yt_downloaders
{
    public class infoFetcher
    {
        private readonly string _ytDlpPath;

        public infoFetcher()
        {
            _ytDlpPath = "yt-dlp";
        }

        public infoFetcher(string ytDlpPath)
        {
            _ytDlpPath = ytDlpPath;
        }

        public async Task<string> getInfo(string url)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = _ytDlpPath,
                Arguments = $"-j --no-warnings \"{url}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = processInfo };

            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception($"yt-dlp failed: {error}");
            }

            return output;
        }
    }
}
