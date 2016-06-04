using System;
using System.Text;

namespace Business
{
    public class RandomStringGenerator
    {
        private const int N = 62;
        private readonly byte[] _codes = new byte[N];
        public RandomStringGenerator()
        {
            byte i = 0;
            //1-9
            for (byte j = 48; j <= 57; i++, j++)
            {
                _codes[i] = j;
            }

            //A-Z
            for (byte j = 65; j <= 90; i++, j++)
            {
                _codes[i] = j;
            }
            //a-z
            for (byte j = 97; j <= 122; i++, j++)
            {
                _codes[i] = j;
            }
        }

        public string GetString(int length)
        {
            Random rnd = new Random();
            var bytes = new byte[length];

            for (int i = 0; i < length; i++)
            {
                bytes[i] = _codes[rnd.Next(N)];
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
