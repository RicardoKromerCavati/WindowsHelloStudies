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
    }
}
