using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     *  A party in the federation. Parties can receive encrypted pseudonyms.
     */
    public class Party
    {
        private PPKeyPair keyPair;
        private BigInteger closingKey;

        /**
         * Constructs a Party.
         *
         * @param id The id for this Party
         * @param keyPair The {@link PPKeyPair} with the public and private key for this Party
         * @param closingKey The closing key this Party uses to form the final pseudonym for its users
         */
        public Party(string id, PPKeyPair keyPair, BigInteger closingKey)
        {
            this.Id = id;
            this.keyPair = keyPair;
            this.closingKey = closingKey;
        }

        /**
         * Decrypt an Encrypted Pseudonym.
         *
         * @param ep The Encrypted {@link Pseudonym}
         * @return The pseudonym from the Encrypted Pseudonym
         */
        public byte[] DecryptPseudonym(Pseudonym ep)
        {
            ECPoint pseudonym = ep.B.Subtract(ep.A.Multiply(keyPair.PrivateKey));
            pseudonym = pseudonym.Multiply(closingKey);
            return Util.Hash(pseudonym.GetEncoded());
        }

        public string Id
        {
            get;
        }
    }
}