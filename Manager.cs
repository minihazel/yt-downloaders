using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yt_downloaders
{
    public class youTubeManager
    {
        #region Private Fields
        private readonly string _ytDlpPath;
        #endregion

        #region Constructor
        public youTubeManager(string ytDlpPath = "yt-dlp")
        {
            _ytDlpPath = ytDlpPath;
        }
        #endregion

        #region Public Methods
        public async Task<VideoInfo> getVideoInfo(string url)
        {
            string json = await getVideoJson(url);
            var videoInfo = parseVideoJson(json);
            videoInfo.Url = url; // Store the URL for analysis
            return videoInfo;
        }

        public async Task<List<string>> getAvailableResolutionsAsync(string url)
        {
            var videoInfo = await getVideoInfo(url);
            return videoInfo.getAvailableResolutions();
        }

        public async Task<string> getHighestQualityResolutionAsync(string url)
        {
            var videoInfo = await getVideoInfo(url);
            return videoInfo.getHighestQualityResolution();
        }

        public async Task<List<string>> getAvailableFormatsAsync(string url)
        {
            var videoInfo = await getVideoInfo(url);
            return videoInfo.getAvailableFormats();
        }

        public async Task<List<string>> getAvailableCodecsAsync(string url)
        {
            var videoInfo = await getVideoInfo(url);
            return videoInfo.getAvailableCodecs();
        }
        #endregion

        #region Private Methods - JSON Fetching
        private async Task<string> getVideoJson(string url)
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
        #endregion

        #region Private Methods - JSON Parsing
        private VideoInfo parseVideoJson(string json)
        {
            var jsonObj = JObject.Parse(json);

            var videoInfo = new VideoInfo
            {
                Title = jsonObj["title"]?.ToString(),
                Duration = jsonObj["duration_string"]?.ToString()
            };

            var formats = jsonObj["formats"]?.ToArray();
            if (formats != null)
            {
                foreach (var format in formats)
                {
                    var videoFormat = new VideoFormat
                    {
                        FormatId = format["format_id"]?.ToString(),
                        Extension = format["ext"]?.ToString(),
                        Height = format["height"]?.ToObject<int?>(),
                        Width = format["width"]?.ToObject<int?>(),
                        Fps = format["fps"]?.ToObject<double?>(),
                        VideoCodec = format["vcodec"]?.ToString(),
                        AudioCodec = format["acodec"]?.ToString(),
                        Filesize = format["filesize"]?.ToObject<long?>(),
                        Quality = format["quality"]?.ToString(),
                        HasVideo = format["vcodec"]?.ToString() != "none",
                        HasAudio = format["acodec"]?.ToString() != "none"
                    };

                    videoInfo.Formats.Add(videoFormat);
                }
            }

            return videoInfo;
        }
        #endregion

        #region Nested Classes
        public class VideoFormat
        {
            public string FormatId { get; set; }
            public string Extension { get; set; }
            public int? Height { get; set; }
            public int? Width { get; set; }
            public double? Fps { get; set; }
            public string VideoCodec { get; set; }
            public string AudioCodec { get; set; }
            public long? Filesize { get; set; }
            public string Quality { get; set; }
            public bool HasVideo { get; set; }
            public bool HasAudio { get; set; }

            public override string ToString()
            {
                if (!HasVideo) return $"Audio only - {AudioCodec} - {Extension}";

                string resolution = (Height.HasValue && Width.HasValue) ?
                    getResolutionFromDimensions(Width.Value, Height.Value) : "Unknown";
                string fps = Fps.HasValue ? $" @ {Fps}fps" : "";
                string size = Filesize.HasValue ? $" ({Filesize / 1024 / 1024:F1} MB)" : "";

                return $"{resolution}{fps} - {VideoCodec} - {Extension}{size}";
            }

            private string getResolutionFromDimensions(int width, int height)
            {
                // For portrait videos, use height as the resolution
                // For landscape videos, use height as the resolution (standard)
                int resolutionValue = height;

                return resolutionValue switch
                {
                    >= 2160 => "4K",
                    >= 1440 => "1440p",
                    >= 1080 => "1080p",
                    >= 720 => "720p",
                    >= 480 => "480p",
                    >= 360 => "360p",
                    >= 240 => "240p",
                    _ => $"{resolutionValue}p"
                };
            }
        }

        public class VideoInfo
        {
            public string Title { get; set; }
            public string Duration { get; set; }
            public string Url { get; set; }
            public List<VideoFormat> Formats { get; set; } = new List<VideoFormat>();

            public bool isPortrait()
            {
                if (!string.IsNullOrEmpty(Url) && Url.Contains("shorts"))
                    return true;

                // check video format for portrait mode
                var videoFormats = Formats.Where(f => f.HasVideo && f.Width.HasValue && f.Height.HasValue).ToList();
                if (videoFormats.Any())
                {
                    var portraitCount = videoFormats.Count(f => f.Height > f.Width);
                    return portraitCount > videoFormats.Count / 2;
                }

                return false;
            }

            public string getOrientation() => isPortrait() ? "Portrait" : "Landscape";

            public List<string> getAvailableResolutions()
            {
                var resolutions = Formats
                    .Where(f => f.HasVideo && f.Height.HasValue && f.Width.HasValue)
                    .Select(f => getResolutionFromDimensions(f.Width.Value, f.Height.Value))
                    .Distinct()
                    .OrderByDescending(r => getResolutionSortOrder(r))
                    .ToList();

                return resolutions;
            }

            public string getHighestQualityResolution()
            {
                var formats = Formats.Where(f => f.HasVideo && f.Height.HasValue && f.Width.HasValue).ToList();
                if (!formats.Any()) return "Unknown";

                var bestFormat = formats.OrderByDescending(f => Math.Max(f.Width.Value, f.Height.Value)).First();
                return getResolutionFromDimensions(bestFormat.Width.Value, bestFormat.Height.Value);
            }

            private string getResolutionFromDimensions(int width, int height)
            {
                // Use the larger dimension as the "resolution"
                int maxDimension = Math.Max(width, height);

                return maxDimension switch
                {
                    >= 2160 => "4K",
                    >= 1440 => "1440p",
                    >= 1080 => "1080p",
                    >= 720 => "720p",
                    >= 480 => "480p",
                    >= 360 => "360p",
                    >= 240 => "240p",
                    _ => $"{maxDimension}p"
                };
            }

            private int getResolutionSortOrder(string resolution)
            {
                return resolution switch
                {
                    "4K" => 2160,
                    "1440p" => 1440,
                    "1080p" => 1080,
                    "720p" => 720,
                    "480p" => 480,
                    "360p" => 360,
                    "240p" => 240,
                    _ => int.TryParse(resolution.TrimEnd('p'), out int val) ? val : 0
                };
            }

            public List<string> getAvailableFormats()
            {
                return Formats
                    .Where(f => f.HasVideo && !string.IsNullOrEmpty(f.Extension))
                    .Select(f => f.Extension)
                    .Distinct()
                    .OrderBy(ext => ext)
                    .ToList();
            }

            public List<string> getAvailableCodecs()
            {
                return Formats
                    .Where(f => f.HasVideo && !string.IsNullOrEmpty(f.VideoCodec) && f.VideoCodec != "none")
                    .Select(f => f.VideoCodec)
                    .Distinct()
                    .OrderByDescending(codec => codec)
                    .ToList();
            }

            private string convertHeightToResolution(int height)
            {
                return height switch
                {
                    >= 2160 => "4K",
                    >= 1440 => "1440p",
                    >= 1080 => "1080p",
                    >= 720 => "720p",
                    >= 480 => "480p",
                    >= 360 => "360p",
                    >= 240 => "240p",
                    _ => $"{height}p"
                };
            }

            public List<VideoFormat> getVideoFormats()
            {
                return Formats
                    .Where(f => f.HasVideo)
                    .OrderByDescending(f => f.Height ?? 0)
                    .ThenByDescending(f => f.Fps ?? 0)
                    .ToList();
            }

            public List<VideoFormat> getAudioFormats()
            {
                return Formats
                    .Where(f => f.HasAudio && !f.HasVideo)
                    .ToList();
            }
        }
        #endregion
    }
}
