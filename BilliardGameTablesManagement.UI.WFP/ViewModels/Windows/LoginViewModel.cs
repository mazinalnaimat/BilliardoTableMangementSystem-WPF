using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Helpers;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IWindowService _windowService;

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private string _usernameError = string.Empty;
        private string _passwordError = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                if (!string.IsNullOrWhiteSpace(_username))
                {
                    UsernameError = string.Empty;
                }

                ErrorMessage = string.Empty;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                if (!string.IsNullOrWhiteSpace(_password))
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

        public LoginViewModel(IAuthService authService, IWindowService windowService)
        {
            _authService = authService
                ?? throw new ArgumentNullException(nameof(authService));

            _windowService = windowService
                ?? throw new ArgumentNullException(nameof(windowService));

            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            if (!ValidateLoginInput())
                return;

            var result = _authService.Login(new LoginRequest
            {
                Username = Username,
                Password = Password
            });

            if (!result.Success)
            {
                ErrorMessage = result.Message;
                return;
            }

            _windowService.ShowBilliardTablesWindow();
            _windowService.CloseWindowByDataContext(this);
        }

        private bool ValidateLoginInput()
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
