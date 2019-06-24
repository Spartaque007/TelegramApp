using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    public static class LocalFile
    {
        public static string Dir { get; } = $@"{ConfigurationManager.AppSettings.Get("DefaultDir") ?? "."}";

        public static bool CheckUsersFolder()
        {
            if (!Directory.Exists(Dir))
            {
                Console.WriteLine("Config folder is not found, but I took care of it and created it ");
                Directory.CreateDirectory(Dir);
                return false;
            }
            else
            {
                Console.WriteLine("Directory is OK");
                return true;
            }
        }

        public static bool CheckPathFile(string name)
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
                Console.WriteLine($"file is not found, but I created her ");
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
                return " ";
            }
        }

        public static void SaveTextToFile(string fileName, string text)
        {
            CheckPathFile(fileName);
            File.WriteAllText($@"{Dir}\{fileName}", text);
        }

    }
}