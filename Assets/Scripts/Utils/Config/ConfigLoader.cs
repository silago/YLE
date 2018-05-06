using System.Collections.Generic;
using System.IO;

namespace Utils.Config
{
    public class ConfigLoader
    {
        private readonly Dictionary<string, string> data;

        private ConfigLoader(Dictionary<string, string> data)
        {
            this.data = data;
        }

        private string this[string key]
        {
            get { return data[key]; }
        }

        public string Get(string key)
        {
            return this[key];
        }


        //TODO:: support load from env
        public static ConfigLoader LoadConfig(string path = "Assets/Resources/yle.config")
        {
            var result = new Dictionary<string, string>();
            var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var parts = line.Split('=');
                result.Add(parts[0], parts[1]);
            }

            reader.Close();
            return new ConfigLoader(result);
        }
    }
}