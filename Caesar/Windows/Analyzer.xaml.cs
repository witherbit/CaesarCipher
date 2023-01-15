using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Caesar.Windows
{
    /// <summary>
    /// Логика взаимодействия для Analyzer.xaml
    /// </summary>
    public partial class Analyzer : Window, IDisposable
    {
        public int Offset { get; set; }
        public string CipherText { get; set; }
        public string PlainText { get; set; }
        private string Alphabet { get; set; }
        public Analyzer(string cipherText, string alphabet)
        {
            InitializeComponent();
            CipherText = cipherText.ToUpper();
            Alphabet = alphabet;
            Bruteforce();
        }

        private void Bruteforce() // метод перебора значений, который выводит элементы на интерфейс
        {
            Offset = -1;                                                                        // стандартное значение для ключа.
            for (int i = 0; i < Alphabet.Length; i++)                                           // цикл с перебором всех сдвигов по длине алфавита
            {
                var plainText = CipherText.CesarCryptoProvider(i.ToString(), Alphabet, true);   // расшифровываем текст и пихаем в интерфейс
                stackPanel_CipherText.Children.Add(CreateButton(CipherText, false));            // с эти то же самое, только просто кидаем исходные варианты на интерфейс
                stackPanel_Offset.Children.Add(CreateButton(i.ToString()));
                stackPanel_PlainText.Children.Add(CreateButton(plainText, false));
            }
        }

        public Button CreateButton(string offset, bool isEnabled = true)
        {
            var button = new Button()
            {
                Content = offset,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Background = Brushes.Transparent,
                IsEnabled = isEnabled,
            };
            button.Click += (sender, e) => // событие при нажатии кнопки со значением offset, которое передает значения в основное окно программы
            {
                var btn = sender as Button;
                Offset = int.Parse(btn.Content as string);
                PlainText = CipherText.CesarCryptoProvider(Offset.ToString(), Alphabet, true);
                this.DialogResult = true;
            };
            return button;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
        }
    }
}
