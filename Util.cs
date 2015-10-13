using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Crypto.Digests;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     * Static class that provides different helper functions.
     */
    public static class Util
    {
        static IRandomGenerator prng = new DigestRandomGenerator(new Org.BouncyCastle.Crypto.Digests.Sha1Digest());

        /**
         * Generate secure random bytes.
         *
         * @param num The number of random bytes to generate
         * @return byte array of length num containing secure random bytes
         */
        public static byte[] RandomBytes(int num)
        {
            byte[] bytes = new byte[num];
            prng.NextBytes(bytes);
            return bytes;
        }

        /**
         * Generate a secure random {@link BigInteger}
         * @return A secure random {@link BigInteger}
         */
        public static BigInteger Random()
        {
            return new BigInteger(1, RandomBytes(SystemParams.FieldSizeBytes));
        }

        /**
         * Embeds data into the elliptic curve.
         *
         * @param bytes The data to embed
         * @return The {@link ECPoint} representing the data
         */
        public static ECPoint Embed(byte[] bytes)
        {
            byte counter = 0;
            ECFieldElement x = null, y = null;
            while (y == null)
            {
                x = SystemParams.Curve.FromBigInteger(new BigInteger(bytes));

                y = x.Square().Multiply(x)
                    .Add(x.Multiply(SystemParams.Curve.A))
                    .Add(SystemParams.Curve.B)
                    .Sqrt();
                
                if (counter == 0)
                {
                    byte[] newBytes = new byte[bytes.Length];
                    bytes.CopyTo(newBytes, 0);
                }
                counter++;
                bytes[bytes.Length - 1] = counter;
            }
            return SystemParams.Curve.CreatePoint(x.ToBigInteger(), y.ToBigInteger(), true);
        }

        /**
         * Hashes the given data.
         *
         * @param data The data to hash
         * @return byte-array containing the hashed data
         */
        public static byte[] Hash(byte[] data)
        {
            Sha256Digest sha256 = new Sha256Digest();
            sha256.BlockUpdate(data, 0, data.Length);
            byte[] hash = new byte[sha256.GetDigestSize()];
            sha256.DoFinal(hash, 0);
            return hash;
        }
    }
}
