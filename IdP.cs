using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     * An Identity Provider creates polymorphic pseudonyms for its users.
     */
    public class IdP
    {
        ECPoint y_k;

        /**
         * Construct an Identity Provider.
         *
         * @param y_k The system-wide public key used in the federation
         */
        public IdP(ECPoint y_k)
        {
            this.y_k = y_k;
        }

        /**
         * Generate a polymorphic pseudonym for a specific user.
         *
         * @param uid The User ID of the user for whom a polymorphic pseudonym should be formed
         * @return The Polymorphic {@link Pseudonym} for the user specified by the given uid.
         */
        public Pseudonym GeneratePolymorphicPseudonym(string uid)
        {
            BigInteger k = Util.Random();
            ECPoint S = Util.Embed(System.Text.Encoding.UTF8.GetBytes(uid));
            return new Pseudonym(
                SystemParams.G.Multiply(k),
                S.Add(y_k.Multiply(k)),
                y_k);
        }
    }
}
