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

        private void Bruteforce()
        {
            Offset = -1;
            for(int i = 0; i < Alphabet.Length; i++)
            {
                var plainText = CipherText.CesarCryptoProvider(i.ToString(), Alphabet, true);
                stackPanel_CipherText.Children.Add(CreateButton(CipherText, false));
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
            button.Click += (sender, e) =>
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
