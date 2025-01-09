
using System.Diagnostics;

namespace SpotifyStatus
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() { }
        public InvalidParameterException(string item) : base("Invalid parameter: '" + item + "' use: --help to output available parameters")
        {
        }
        public InvalidParameterException(string message, Exception innerException)
        : base(message, innerException) { }
    }

    public readonly struct SpotifySong(string title, string artist)
    {
        public readonly string title = title;
        public readonly string artist = artist;
    }
    internal class Program
    {
        public static string TITLE_FILE = "title-file";
        public static string ARTIST_FILE = "artist-file";
        public static string REFRESH_RATE = "refresh-rate";
        public static string[] ACCEPTABLE_PARAMETERS = [ARTIST_FILE, TITLE_FILE, REFRESH_RATE];
        public static string HELP_STRING = "";
        static int Main(string[] args)
        {

            if (args.Contains("--help"))
            {
                Console.WriteLine(HELP_STRING);
                return 0;
            }
            try
            {
                Dictionary<string, string> parameters = ParseParams(args); //This really should change to a custom struct at some point but cba
                int iterationTimeMs = 0;

                if (parameters.ContainsKey(REFRESH_RATE))
                { 
                    if(Int32.TryParse(parameters[REFRESH_RATE], out int result)) {
                        iterationTimeMs = result * 1000;
                    } else
                    {
                        throw new Exception("Invalid refresh rate provided: " + parameters[REFRESH_RATE]);
                    }
                }
                if (iterationTimeMs == 0)
                {
                    Update(parameters);
                    return 0;
                }

                while (true) // Should probably handle control c here :joy:
                {
                    Update(parameters);
                    Thread.Sleep(iterationTimeMs);
                }

            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }

        private static void Update(Dictionary<string, string> parameters)
        {
            SpotifySong s = GetTitle();

            if (!parameters.ContainsKey(TITLE_FILE) && !parameters.ContainsKey(ARTIST_FILE))
            {
                Console.WriteLine(s.artist + " - " + s.title); // Output to stdout if title file and artist file not provided
            }

            if (parameters.ContainsKey(TITLE_FILE))
            {
                UpdateFile(parameters[TITLE_FILE], s.title);
            }

            if (parameters.ContainsKey(ARTIST_FILE))
            {
                UpdateFile(parameters[ARTIST_FILE], s.artist);
            }
        }
        private static void UpdateFile(string fileName, string content) {
            using (StreamWriter sw = new(fileName)) { 
                sw.WriteLine(content); 
            }
        }
        private static Dictionary<string, string> ParseParams(string[] args)
        {
            Dictionary<string, string> parameters = new();

            foreach (var item in args)
            {
                string[] p = item.Split("=");

                if (p.Length != 2 || !p[0].StartsWith("--"))
                {
                    throw new InvalidParameterException(item);
                }

                string parametername = p[0].TrimStart('-');

                if (!ACCEPTABLE_PARAMETERS.Contains(parametername))
                {
                    throw new InvalidParameterException(item);
                }
                string parametervalue = p[1];

                parameters.Add(parametername, parametervalue);
            }

            return parameters;
        }

        private static SpotifySong GetTitle()
        {
            Process[] processes = Process.GetProcessesByName("spotify");
            string title = string.Empty;

            foreach (var item in processes)
            {
                if (item.MainWindowTitle != "")
                {
                    title = item.MainWindowTitle;
                    break;
                };
            }

            if (title == string.Empty)
            {
                return new SpotifySong("Spotify Not Found", "Spotify Not Found");
            }
            else if (title == "Spotify Premium")
            {
                return new SpotifySong("Not Playing", "Not Playing");
            }
            else
            {
                string[] split = title.Split(" - ");
                if (split.Length == 1)
                {
                    return new SpotifySong("Error", "Error");
                }
                return new SpotifySong(String.Join(" - ", split.Skip(1)), split[0]);
            }
        }
    }
}