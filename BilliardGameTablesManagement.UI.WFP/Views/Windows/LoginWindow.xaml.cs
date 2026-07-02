using BilliardGameTablesManagement.DependencyInjection;
using BilliardGameTablesManagement.ViewModels.Windows;
using System;
using System.Windows;

namespace BilliardGameTablesManagement.Views.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
            : this(ApplicationServices.CreateLoginViewModel())
        {
        }

        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
