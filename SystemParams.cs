using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Asn1.TeleTrust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X9;

namespace nl.surfnet.PolymorphicPseudonyms
{
    /**
     * The parameters of the curve used in the system.
     */
    public static class SystemParams
    {
        private static readonly X9ECParameters parameters = TeleTrusTNamedCurves.GetByOid(TeleTrusTObjectIdentifiers.BrainpoolP320R1);

        public static ECCurve Curve
        {
            get
            {
                return parameters.Curve;
            }
        }

        public static ECPoint G
        {
            get
            {
                return parameters.G;
            }
        }

        public static int FieldSizeBytes
        {
            get
            {
                return parameters.Curve.FieldSize / 8;
            }
        }
    }
}
