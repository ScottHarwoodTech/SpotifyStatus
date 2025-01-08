
using System.Diagnostics;

namespace SpotifyStatus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcessesByName("spotify");
            string title = "";
            foreach (var item in processes)
            {
                if (item.MainWindowTitle != "")
                {
                    title = item.MainWindowTitle;
                    break;
                };       
            }

            if (title == "Spotify Premium")
            {
                Console.Write("Not Playing");
            } else
            {
                Console.Write(title);
            }
        }
    }
}