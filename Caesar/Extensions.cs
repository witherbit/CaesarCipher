using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caesar
{
    internal static class Extensions
    {
        public static string CesarCryptoProvider(this string text, string offset, string aphabet, bool isDecrypt = false)
        {
            try
            {
                int key = int.Parse(offset);
                if (isDecrypt)
                    key *= -1;
                var length = aphabet.Length;
                var result = "";
                foreach (var letter in text.ToUpper())
                {
                    var index = aphabet.IndexOf(letter);
                    if (index < 0)
                        result += letter;
                    else
                        result += aphabet[(length + index + key).ModAbs(length)];
                    
                }

                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string AlphabetRus
        { 
            get => "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        }   

        public static int ModAbs(this int value, int modValue)
        {
            var result = value % modValue;
            if (result < 0) 
                result += modValue;
            return result;
        }
    }
}
