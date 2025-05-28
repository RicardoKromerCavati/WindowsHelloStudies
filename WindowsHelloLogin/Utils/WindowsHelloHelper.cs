using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace WindowsHelloLogin.Utils
{
    public static class WindowsHelloHelper
    {
        /// <summary>
        /// Checks to see if Windows Hello is ready to be used.
        /// 
        /// Windows Hello has dependencies on:
        ///     1. Having a connected Microsoft Account
        ///     2. Having a Windows PIN set up for that account on the local machine
        /// </summary>
        public static async Task<bool> WindowsHelloAvailableCheckAsync()
        {
            bool keyCredentialAvailable = await KeyCredentialManager.IsSupportedAsync();
            if (keyCredentialAvailable == false)
            {
                // Key credential is not enabled yet as user 
                // needs to connect to a Microsoft Account and select a PIN in the connecting flow.
                Debug.WriteLine("Windows Hello is not set up!\nPlease go to Windows Settings and set up a PIN to use it.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a Windows Hello key on the machine using the account ID provided.
        /// </summary>
        /// <param name="accountId">The account ID associated with the account that we are enrolling into Windows Hello</param>
        /// <returns>Boolean indicating if creating the Windows Hello key succeeded</returns>
        public static async Task<bool> CreateWindowsHelloKeyAsync(string accountId)
        {
            KeyCredentialRetrievalResult keyCreationResult = await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting);

            switch (keyCreationResult.Status)
            {
                case KeyCredentialStatus.Success:
                    Debug.WriteLine("Successfully created key");

                    // In the real world, authentication would take place on a server.
                    // So, every time a user migrates or creates a new Windows Hello
                    // account, details should be pushed to the server.
                    // The details that would be pushed to the server include:
                    // The public key, keyAttestation (if available), 
                    // certificate chain for attestation endorsement key (if available),  
                    // status code of key attestation result: keyAttestationIncluded or 
                    // keyAttestationCanBeRetrievedLater and keyAttestationRetryType.
                    // As this sample has no concept of a server, it will be skipped for now.
                    // For information on how to do this, refer to the second sample.

                    // For this sample, just return true
                    return true;
                case KeyCredentialStatus.UserCanceled:
                    Debug.WriteLine("User cancelled sign-in process.");
                    break;
                case KeyCredentialStatus.NotFound:
                    // User needs to set up Windows Hello
                    Debug.WriteLine("Windows Hello is not set up!\nPlease go to Windows Settings and set up a PIN to use it.");
                    break;
                default:
                    break;
            }

            return false;
        }
    }
}
