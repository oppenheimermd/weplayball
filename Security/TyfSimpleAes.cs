using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WePlayBall.Security
{
    //  See: http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp
    public class TyfSimpleAes
    {
        private static readonly byte[] Key = { 182, 63, 62, 93, 105, 103, 17, 35, 91, 148, 102, 193, 140, 219, 127, 100, 187, 36, 174, 252, 12, 172, 193, 13, 190, 212, 208, 42, 26, 30, 255, 221 };
        private static readonly byte[] Vector = { 14, 8, 19, 5, 238, 25, 156, 183, 50, 177, 40, 254, 164, 53, 53, 232 };
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _decryptor;
        private readonly UTF8Encoding _encoder;

        public TyfSimpleAes()
        {
            RijndaelManaged rm = new RijndaelManaged();
            _encryptor = rm.CreateEncryptor(Key, Vector);
            _decryptor = rm.CreateDecryptor(Key, Vector);
            _encoder = new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(_encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return _encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        private byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, _encryptor);
        }

        private byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, _decryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}
