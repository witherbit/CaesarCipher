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
        public static string CesarCryptoProvider(this string text, string offset, string aphabet, bool isDecrypt = false) // расширение, позволяющее расшифровать и зашифровать текст
        {
            try // процедура обработки исключения
            {
                int key = int.Parse(offset);                // строковый тип к целочисленному с проверкой
                if (isDecrypt)                              // если надо расшифровать, то меняем направление ключа в - 
                    key *= -1;
                var length = aphabet.Length;                // длина алфавита
                var result = "";                            // переменная для результата
                foreach (var letter in text.ToUpper())      // цикл для перебора текста (получает каждую итерацию символ из целого текста)
                {
                    var index = aphabet.IndexOf(letter);    // поиск индекса символа в алфавите
                    if (index < 0)                          // если индекс в алфавите не найден, то добавляем в строку символ "как есть".
                        result += letter;
                    else                                    // иначе добавляем символ со сдвигом
                        result += aphabet[(length + index + key).ModAbs(length)]; 
                    
                }

                return result;
            }
            catch (Exception e) // отправляем сообщение об ошибке, вместо ответа
            {
                return e.Message;
            }
            
        }

        public static bool IsNullOrWhiteSpace(this string value) // расширение, позволяющее быстро узнать, пустая ли переменная, или имеет символы
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string AlphabetRus // св-во с алфавитом
        { 
            get => "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        }    

        public static int ModAbs(this int value, int modValue) // исключает неправильный результат деления по модулю, если ключ превысит длину алавита при дешифровании
        {
            var result = value % modValue;  // деление по модулю
            if (result < 0)                 // если значения < 0, то прибавляем длину алфавита
                result += modValue;
            return result;
        }
    }
}
