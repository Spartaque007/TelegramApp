using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramApp
{
    public static class AppDir
    {
        public static string Dir { get; } = $@"{ConfigurationManager.AppSettings.Get("DefaultDir") ?? "."}";
        public static string MeetingsFile { get; } = $@"{ConfigurationManager.AppSettings.Get("DefaultDir") ?? "."}{ConfigurationManager.AppSettings.Get("MeetingFile") ?? "Meetings"}";
        public static bool CheckPathFile()
        {
            if (!Directory.Exists(Dir))
            {
                Console.WriteLine("Config folder is not found, but I took care of it and created it ");
                Directory.CreateDirectory(Dir);

            }

            try
            {
                File.Open(MeetingsFile, FileMode.Open, FileAccess.Read, FileShare.None);
                Console.WriteLine("Directory is OK");
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"file is not found, but I created her ");
                File.Create(MeetingsFile);
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Invalid directory ");
                return false;
            }


        }


    }
}
