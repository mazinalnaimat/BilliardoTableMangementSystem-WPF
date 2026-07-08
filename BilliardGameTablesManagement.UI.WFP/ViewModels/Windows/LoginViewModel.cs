using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Models;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel
    {
        private string _errorMessage = string.Empty;
        private string _usernameError = string.Empty;
        private string _passwordError = string.Empty;

        internal LoginModel Login { get; } = new();

        public string Username
        {
            get => Login.Username;
            set
            {
                Login.Username = value;
                if (!string.IsNullOrWhiteSpace(Login.Username))
                {
                    UsernameError = string.Empty;
                }

                ErrorMessage = string.Empty;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => Login.Password;
            set
            {
                Login.Password = value;
                if (!string.IsNullOrWhiteSpace(Login.Password))
                {
                    PasswordError = string.Empty;
                }

                ErrorMessage = string.Empty;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public string UsernameError
        {
            get => _usernameError;
            set
            {
                _usernameError = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasUsernameError));
            }
        }

        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasPasswordError));
            }
        }

        public bool HasUsernameError => !string.IsNullOrWhiteSpace(UsernameError);

        public bool HasPasswordError => !string.IsNullOrWhiteSpace(PasswordError);

        public bool HasErrorMessage => !string.IsNullOrWhiteSpace(ErrorMessage);

        public ICommand LoginCommand { get; }

        public LoginViewModel(AuthenticationStore authenticationStore, INavigationService navigationService)
        {
            if (authenticationStore == null)
                throw new ArgumentNullException(nameof(authenticationStore));

            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            LoginCommand = new LoginCommand(this, authenticationStore, navigationService);
        }

        internal bool ValidateLoginInput()
        {
            UsernameError = string.Empty;
            PasswordError = string.Empty;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username))
            {
                UsernameError = "Username is required.";
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required.";
            }

            if (HasUsernameError && HasPasswordError)
            {
                ErrorMessage = "Please enter your username and password.";
            }

            return !HasUsernameError && !HasPasswordError;
        }
    }
}
