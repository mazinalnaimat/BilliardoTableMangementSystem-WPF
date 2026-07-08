using BilliardGameTablesManagement.DependencyInjection;
using BilliardGameTablesManagement.ViewModels.Windows;
using System;
using System.Windows;

namespace BilliardGameTablesManagement.Views.Windows
{
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow()
            : this(ApplicationServices.CreateUserInfoViewModel())
        {
        }

        public UserInfoWindow(UserInfoViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));
        }
    }
}
