using System;
using System.Windows;
using System.Windows.Controls;

namespace JamLinkUI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ShowPasswordCheck_Checked(object sender, RoutedEventArgs e)
        {
            // show plain text and copy current password
            passwordTextBox.Text = passwordBox.Password;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Focus();
            passwordTextBox.Select(passwordTextBox.Text.Length, 0);
        }

        private void ShowPasswordCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            // hide plain text and copy back to PasswordBox
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
            passwordBox.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // keep plaintext textbox in sync while visible
            if (showPasswordCheck.IsChecked == true)
                passwordTextBox.Text = passwordBox.Password;
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // keep password box in sync while hidden (so login reads correct value)
            if (showPasswordCheck.IsChecked == true)
                passwordBox.Password = passwordTextBox.Text;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // clear previous errors
            usernameError.Text = string.Empty;
            passwordError.Text = string.Empty;

            // simple validation (keeps XAML simple, no complex bindings)
            if (string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                usernameError.Text = "Username is required.";
                usernameBox.Focus();
                return;
            }

            // read password from the control currently shown
            string password = passwordBox.Visibility == Visibility.Visible
                ? passwordBox.Password
                : passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(password))
            {
                passwordError.Text = "Password is required.";
                if (passwordBox.Visibility == Visibility.Visible)
                    passwordBox.Focus();
                else
                    passwordTextBox.Focus();
                return;
            }

            // TODO: perform login
        }
    }
}
