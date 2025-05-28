using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsHelloLogin.Utils;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsHelloLogin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check if Windows Hello is set up and available on this machine
            if (await WindowsHelloHelper.WindowsHelloAvailableCheckAsync())
            {
            }
            else
            {
                // Windows Hello isn't set up, so inform the user
                WindowsHelloStatus.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                WindowsHelloStatusText.Text = $"Windows Hello is not set up!{Environment.NewLine}Please go to Windows Settings and set up a PIN to use it.";
                LoginButton.IsEnabled = false;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }
    }
}
