using NUnit.Framework;

namespace CommonNet.Cryptography.Tests
{
    [Parallelizable]
    public class Tests
    {
        [Test]
        public void EncryptDecryptTest()
        {
            ITextEncoder textEncoder = new Aes256TextEncoder();
            string testText = "This text should be the same\r\n";
            string password = "PASSWORD WITH SPACES";

            string cypherText = textEncoder.EncryptText(testText, password);
            Assert.IsFalse(string.IsNullOrWhiteSpace(cypherText));

            string clearText = textEncoder.DecryptText(cypherText, password);
            Assert.IsFalse(string.IsNullOrWhiteSpace(clearText));

            Assert.AreEqual(testText, clearText);
            Assert.AreNotEqual(cypherText, clearText);
        }
    }
}