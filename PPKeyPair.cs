using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     * A public key pair.
     */
    public class PPKeyPair
    {
        /**
         * Construct a new public key pair. The public key is generated from the private key.
         * @param privateKey The private key of this key pair.
         */
        public PPKeyPair(BigInteger privateKey)
        {
            this.PrivateKey = privateKey;
            this.PublicKey = SystemParams.G.Multiply(privateKey);
        }

        public BigInteger PrivateKey
        {
            get;
        }

        public ECPoint PublicKey
        {
            get;
        }
    }
}