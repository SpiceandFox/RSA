using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class KeyGenerator
    {
        public BigInteger PublicKey { get; private set; }
        public BigInteger Totient { get; private set; }
        public BigInteger PrivateKey { get; private set; }
        public BigInteger Product { get; private set; }


        public KeyGenerator(BigInteger p, BigInteger q)
        {
            Totient = GenerateTotient(p, q);
            Product = p * q;
            PublicKey = GeneratePublicKey(Totient);
            PrivateKey = GeneratePrivateKey(Totient, PublicKey);
        }

        public BigInteger GeneratePrivateKey(BigInteger totient, BigInteger publicKey)
        {
            BigInteger result = 0;

            while (true)
            {
                if (((result * publicKey) % totient) == 1)
                {
                    break;
                }
                result++;
            }

            return result;
        }

        public BigInteger GeneratePublicKey(BigInteger totient)
        {
            for (BigInteger publicKey = totient - 1; publicKey > 0; publicKey--)
            {
                if ((IsPrimeNumber(publicKey)) && (totient % publicKey != 0))
                {
                    return publicKey;
                }
            }

            throw new Exception("No public key found");
        }

        public BigInteger GenerateTotient(BigInteger p, BigInteger q)
        {
            if (!IsPrimeNumber(p))
            {
                throw new Exception("p is not a prime number");
            }

            if (!IsPrimeNumber(q))
            {
                throw new Exception("q is not a prime number");
            }
            return ((p - 1) * (q - 1));
        }

        public bool IsPrimeNumber(BigInteger p)
        {
            BigInteger counter = 0;
            for (BigInteger i = 1; i <= p; i++)
            {
                if (p % i == 0)
                {
                    counter++;
                }
            }

            if (counter == 2)
            {
                return true;
            }
            return false;
        }

        public string EncryptMessage(string message)
        {
            long[] messageInArray = message.Select(c => (long)c).ToArray();
            long[] encryptedMessageInArray = new long[messageInArray.Length];

            for (int i = 0; i < messageInArray.Length; i++)
            {
                BigInteger buffer = IntPow(messageInArray[i], PublicKey);
                buffer = buffer % Product;
                encryptedMessageInArray[i] = (long)buffer;
            }

            message = new string(encryptedMessageInArray.Select(b => (char)b).ToArray());

            return message;
        }

        public string DecryptMessage(string message)
        {
            long[] encryptedMessageInArray = message.Select(c => (long)c).ToArray();
            long[] messageInArray = new long[encryptedMessageInArray.Length];

            for (int i = 0; i < encryptedMessageInArray.Length; i++)
            {
                BigInteger buffer = IntPow(encryptedMessageInArray[i], PrivateKey);
                buffer = buffer % Product;
                messageInArray[i] = (long)buffer;
            }

            message = new string(messageInArray.Select(b => (char)b).ToArray());
            return message;
        }

        private BigInteger IntPow(BigInteger baseInt, BigInteger exponent)
        {
            if (exponent < 0 )
            {
                throw new Exception("No negative numbers in exponent allowed");
            }

            BigInteger result = 1;
            for (BigInteger i = 0; i < exponent; i++)
            {
                result *= baseInt;
            }

            return result;
        }
    }
}
