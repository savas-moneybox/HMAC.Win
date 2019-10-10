using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HMAC.Win
{
    public partial class Form1 : Form
    {
        private const string WebHookSecret = "put the secret here";


        public Form1()
        {
            InitializeComponent();
        }

        private void hashButton_Click(object sender, System.EventArgs e)
        {
            var dataToBeHashed = requestTextBox.Text;
            //var dataToBeHashed = requestTextBox.Text.Replace("\n", "").Replace("\t", "");
            resultTextBox.Text = HashHMACHex(WebHookSecret, dataToBeHashed);
        }

        private string HashHMACHex(string keyHex, string message)
        {
            var hash = HashHMAC(HexDecode(keyHex), StringEncode(message));
            return HashEncode(hash);
        }

        private byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hmac = new HMACSHA256(key);
            return hmac.ComputeHash(message);
        }

        private byte[] StringEncode(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        private string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);

            return bytes;
        }
    }
}
