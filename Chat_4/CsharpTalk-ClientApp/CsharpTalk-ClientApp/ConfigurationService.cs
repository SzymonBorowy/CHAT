using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace CsharpTalk_ClientApp
{
    /// <summary>
    /// Klasa sluzaca do obslugi plikow INI.
    /// </summary>
    class ConfigurationService
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        
        FileInfo ConfigurationFile;
        /// <summary>
        /// Tworzy nowa instancje klasy obslugujacej wybrany plik konfiguracyjny INI.
        /// </summary>
        /// <param name="ConfigurationFile">Relatywna lub bezwzgledna sciezka do pliku konfiguracyjnego, na ktorym bedziemy operowac. Jesli plik nie istnieje, zostanie utworzony pusty.</param>
        public ConfigurationService(string ConfigurationFile)
        {
            this.ConfigurationFile = new FileInfo(ConfigurationFile);
            if (!this.ConfigurationFile.Exists)
                this.ConfigurationFile.Create();
        }

        public bool GetBooleanValue(string Section, string Parameter, bool Default)
        {
            try
            {
                return Convert.ToBoolean(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public byte GetByteValue(string Section, string Parameter, byte Default)
        {
            try
            {
                return Convert.ToByte(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public char GetCharValue(string Section, string Parameter, char Default)
        {
            try
            {
                return Convert.ToChar(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public DateTime GetDateTimeValue(string Section, string Parameter, DateTime Default)
        {
            try
            {
                return Convert.ToDateTime(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public decimal GetDecimalValue(string Section, string Parameter, decimal Default)
        {
            try
            {
                return Convert.ToDecimal(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public double GetDoubleValue(string Section, string Parameter, double Default)
        {
            try
            {
                return Convert.ToDouble(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public short GetInt16Value(string Section, string Parameter, short Default)
        {
            try
            {
                return Convert.ToInt16(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public int GetInt32Value(string Section, string Parameter, int Default)
        {
            try
            {
                return Convert.ToInt32(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public long GetInt64Value(string Section, string Parameter, long Default)
        {
            try
            {
                return Convert.ToInt64(GetStringValue(Section, Parameter, Default));
            }
            catch
            {
                return Default;
            }
        }

        public string GetStringValue(string Section, string Parameter, object Default)
        {
            StringBuilder sb = new StringBuilder(512);
            GetPrivateProfileString(Section, Parameter, Default.ToString(), sb, sb.Capacity, ConfigurationFile.FullName);
            return sb.ToString();
        }

        public void WriteValue(string Section, string Parameter, object Value)
        {
            WritePrivateProfileString(Section, Parameter, Value.ToString(), ConfigurationFile.FullName);
        }
    }
}
