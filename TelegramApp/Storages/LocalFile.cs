using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using TelegramApp.Dependency;

namespace WorkWithFiles
{
    public static class LocalFile
    {
        private static string Dir { get; } = $@"{ConfigurationManager.AppSettings.Get("DefaultDir") ?? "."}";

        public static bool CheckUsersFolder()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool CheckPathFile(string name)
        {
            CheckUsersFolder();
            try
            {
                using (FileStream fs = File.Open($@"{Dir}\{name}", FileMode.Open, FileAccess.Read, FileShare.None)) { }
                return true;
            }
            catch (FileNotFoundException)
            {
                using (FileStream fs = File.Create($@"{Dir}\{name}")) { }
                return false;
            }
        }

        public static async Task<string> GetDataFromFile(string name)
        {
            if (CheckPathFile(name))
            {
                return await Task.Run(() =>
                {
                    string str = File.ReadAllText($@"{Dir}\{name}") ?? " ";
                    return str;
                });
            }
            else
            {
                return "";
            }
        }

        public static void SaveTextToFile(string fileName, string text)
        {
            CheckPathFile(fileName);
            File.WriteAllText($@"{Dir}\{fileName}", text);
        }

    }
}