# Polymorphic Pseudonym - Java Implementation
This is a partial C# implementation for polymorphic pseudonyms, as descibed in <http://eprint.iacr.org/2015/1228>.
It contains everything needed to use polymorphic pseudonyms in an identity provider, for example using ADFS.

## Class description
### IdP
Provides the functionality needed by identity providers: generating polymorphic pseudonyms for users.

### Party
Provides the functionality for decrypting encrypted pseudonyms.

### Pseudonym
A triple (A, B, C) of ECPoints, forming polymorphic or encrypted pseudonyms. Includes functionality for encoding and decoding pseudonyms.

### PPKeyPair
A public key pair that can be used for polymorphic pseudonyms.

### SystemParams
Provides the paramaters for the used curve: brainpoolp320r1.

### Util
Provides some functions that are used on different places in the library: a Key Diversification Function (KDF), a function to embed data as a point on the elliptic curve, a hash function and secure random functions.
