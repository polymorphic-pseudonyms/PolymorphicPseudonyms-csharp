using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using System;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     * A triple of {@link ECPoint}s representing either a Polymorphic Pseudonym or an Encrypted Pseudonym.
     *
     * A pseudonym consists of the triple:
     * {@code (A, B, C) = EG(S, y, k) = (g^k, S * y^k, y)}
     * With {@code EG(S, y, k)} being an ElGamal encryption of plaintext {@code S} with public key {@code y}, using random {@code k}.
     *
     * @see IdP#GeneratePolymorphicPseudonym(string uid)
     * @see Party#DecryptPseudonym(Pseudonym ep)
     */
    public class Pseudonym
    {
        /**
         * Construct a pseudonym triple.
         *
         * @param A The first {@link ECPoint} of the triple
         * @param B The second {@link ECPoint} of the triple
         * @param C The third {@link ECPoint} of the triple
         */
        public Pseudonym(ECPoint A, ECPoint B, ECPoint C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        /**
         * Transforms a Pseudonym from {@code EG(S, y, k)} to {@code EG(S^z, y, k*z)}
         * @param z The power to exponentiate {@code S} by
         * @return The transformed Pseudonym
         */
        public Pseudonym Power(BigInteger z)
        {
            return new Pseudonym(
                A.Multiply(z),
                B.Multiply(z),
                C);
        }

        /**
         * Transforms a Pseudonym from {@code EG(S, y, k)} to {@code EG(S, y^(z^-1), k*z)}
         * @param z The inverse of the power to exponentiate {@code y} by
         * @return The transformed Pseudonym
         */
        public Pseudonym KeyPower(BigInteger z)
        {
            return new Pseudonym(
                A.Multiply(z),
                B,
                C.Multiply(z.ModInverse(((FpCurve)C.Curve).Q)));
        }

        /**
         * Randomizes a Pseudonym from {@code EG(S, y, k)} to {@code EG(S, y, k+z)}
         * @param z The random number to randomize the Pseudnym with
         * @return The randomized Pseudonym
         */
        public Pseudonym Randomize(BigInteger z)
        {
            return new Pseudonym(
                A.Add(SystemParams.G.Multiply(z)),
                B.Add(C.Multiply(z)),
                C);
        }

        /**
         * Randomizes a Pseudonym from {@code EG(S, y, k)} to {@code EG(S, y, k+z)}, for a randomly generated {@code z}
         * @return The randomized Pseudonym
         */
        public Pseudonym Randomize()
        {
            return Randomize(Util.Random());
        }

        /**
         * Encode the pseudonym
         * @return The base64 encoding of the encoded points, concatenated with ',' as separator.
         */
        public string Encode()
        {
            return string.Format("{0},{1},{2}",
                    Convert.ToBase64String(A.GetEncoded()),
                    Convert.ToBase64String(B.GetEncoded()),
                    Convert.ToBase64String(C.GetEncoded()));
        }

        /**
         * Decodes a pseudonym, that is decoded as described for {@link #Encode()}
         * @param encoded The encoded pseudonym
         * @return The pseudonym that is decoded from the passed String
         */
        public static Pseudonym Decode(string encoded)
        {
            string[] parts = encoded.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Encoded string must have 3 parts");
            }
            return new Pseudonym(
                    SystemParams.Curve.DecodePoint(Convert.FromBase64String(parts[0])),
                    SystemParams.Curve.DecodePoint(Convert.FromBase64String(parts[1])),
                    SystemParams.Curve.DecodePoint(Convert.FromBase64String(parts[2]))
            );
        }

        public override string ToString()
        {
            return string.Format("A: {0}\nB: {1}\nC: {2}",
                Convert.ToBase64String(A.GetEncoded()),
                Convert.ToBase64String(B.GetEncoded()),
                Convert.ToBase64String(C.GetEncoded()));
        }

        public ECPoint A
        {
            get;
        }
        public ECPoint B
        {
            get;
        }
        public ECPoint C
        {
            get;
        }
    }
}
