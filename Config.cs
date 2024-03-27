using System;
using System.Collections.Generic;
using System.IO;


namespace JoinHouses
{
    public class Config
    {
        private string configFilePath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 26) + "Modules\\JoinHouses\\config.txt";
        private Dictionary<string, double> configValues = new Dictionary<string, double>();
        private string configFileString = "-- GENERAL CONFIG --\r\n\r\nbaseRelationNeeded=35.0\r\n> Set the base relation needed for acceptance of joining houses proposal. 35 by default\r\nThe math done to calculate relation needed proceeds as follows with 35;\r\n- If the player is a ruler, subtract 40\r\n- If the other hero is a ruler, add 50\r\n- For every tier in the player's clan, subtract 8\r\n- For every tier in the other hero's clan, add 10\r\n";

        private void CreateConfigFile()
        {
            StreamWriter streamWriter = new StreamWriter(this.configFilePath);
            streamWriter.WriteLine(this.configFileString);
            streamWriter.Close();
        }

        public void LoadConfig()
        {
            StreamReader streamReader = new StreamReader(this.configFilePath);
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                int length = str.IndexOf('=');
                if (length != -1)
                    this.configValues[str.Substring(0, length)] = Convert.ToDouble(str.Substring(length + 1));
            }
            streamReader.Close();
        }

        public Config()
        {
            if (!File.Exists(this.configFilePath))
                this.CreateConfigFile();
            this.LoadConfig();
        }

        private object GetValue(string key)
        {
            try
            {
                return this.configValues[key];
            }
            catch (KeyNotFoundException ex)
            {
                File.Delete(this.configFilePath);
                this.CreateConfigFile();
                this.LoadConfig();
                return this.configValues[key];
            }
        }

        public int GetValueInt(string key) => Convert.ToInt32(this.GetValue(key));

        public double GetValueDouble(string key) => Convert.ToDouble(this.GetValue(key));

        public bool GetValueBool(string key) => Convert.ToBoolean(this.GetValue(key));
    }
}
