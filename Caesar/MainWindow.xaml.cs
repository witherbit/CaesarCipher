using Caesar.Windows;
using System.Windows;
using System.Windows.Input;

namespace Caesar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string PlainText     // свойство открытого текста
        {
            get => textBox_PlainText.Text;
            set => textBox_PlainText.Text = value;
        }
        public string CipherText    // свойство шифр текста
        {
            get => textBox_CipherText.Text;
            set => textBox_CipherText.Text = value;
        }
        public string Offset        // свойство ключа
        {
            get => textBox_Offset.Text;
            private set => textBox_Offset.Text = value;
        }
        public string Alphabet      // свойство пользовательского алфавита
        {
            get => textBox_Aphabet.Text.IsNullOrWhiteSpace() ? Extensions.AlphabetRus : textBox_Aphabet.Text.ToUpper();
        }

        public MainWindow() // конструктор класаа
        {
            InitializeComponent();
        }

        private void button_Encrypt_Click(object sender, RoutedEventArgs e) // событие, при нажатии на кнопку Encrypt
        {
            if (!BlockIfNotReady())
                return;
            CipherText = PlainText.CesarCryptoProvider(Offset, Alphabet);
            BlockIfNotReady();
        }

        private void button_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!BlockIfNotReady())
                return;
            PlainText = CipherText.CesarCryptoProvider(Offset, Alphabet, true);
            BlockIfNotReady();
        }

        private bool BlockIfNotReady()
        {
            button_Decrypt.IsEnabled = false;
            button_Encrypt.IsEnabled = false;
            button_Analyze.IsEnabled = false;
            if (textBox_PlainText.Text.IsNullOrWhiteSpace() && textBox_CipherText.Text.IsNullOrWhiteSpace() && textBox_Offset.Text.IsNullOrWhiteSpace())
                return false;
            if (!textBox_CipherText.Text.IsNullOrWhiteSpace() && !textBox_Offset.Text.IsNullOrWhiteSpace())
                button_Decrypt.IsEnabled = true;
            if (!textBox_PlainText.Text.IsNullOrWhiteSpace() && !textBox_Offset.Text.IsNullOrWhiteSpace())
                button_Encrypt.IsEnabled = true;
            if (!textBox_CipherText.Text.IsNullOrWhiteSpace())
                button_Analyze.IsEnabled = true;
            return true;
        }

        private void check_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            BlockIfNotReady();
        }

        private void button_Analyze_Click(object sender, RoutedEventArgs e)
        {
            if (!BlockIfNotReady())
                return;
            Analyzer window = new Analyzer(CipherText, Alphabet);
            if ((bool)window.ShowDialog() && window.Offset >= 0)
            {
                Offset = window.Offset.ToString();
                PlainText = window.PlainText;
                CipherText = window.CipherText;
            }
            window.Dispose();
            BlockIfNotReady();
        }
    }
}
