using System;
using System.Text;

namespace CommonNet.Cryptography
{
    /// <summary>
    /// Performs AES 256 symmetric-key encryption.
    /// </summary>
    public class Aes256TextEncoder : ITextEncoder
    {
        private readonly Aes256ByteEncoder backingEncoder = new Aes256ByteEncoder();

        public string DecryptText(string input, string password)
            => Encoding.UTF8.GetString(this.backingEncoder.DecryptBytes(Convert.FromBase64String(input), password));

        public string DecryptText(string input, string password, string salt)
            => Encoding.UTF8.GetString(this.backingEncoder.DecryptBytes(Convert.FromBase64String(input), password, salt));

        public string EncryptText(string input, string password)
            => Convert.ToBase64String(this.backingEncoder.EncryptBytes(Encoding.UTF8.GetBytes(input), password));

        public string EncryptText(string input, string password, string salt)
            => Convert.ToBase64String(this.backingEncoder.EncryptBytes(Encoding.UTF8.GetBytes(input), password, salt));
    }
}
