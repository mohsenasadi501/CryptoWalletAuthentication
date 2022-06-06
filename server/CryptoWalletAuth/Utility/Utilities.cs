using Nethereum.Signer;

namespace CryptoWalletAuth.Utility
{
    public static class Utilities
    {
        public static string SignMessage(string address, string privateKey, string message)
        {
            var signer = new EthereumMessageSigner();
            var signature = signer.EncodeUTF8AndSign(message, new EthECKey(privateKey));
            return signature ?? "";
        }
        public static string GetAddress(string signature, string message)
        {
            var signer = new EthereumMessageSigner();
            var account = signer.EncodeUTF8AndEcRecover(message, signature);
            return account ?? "";
        }
    }
}
