using BilliardGameTablesManagement.DependencyInjection;
using BilliardGameTablesManagement.ViewModels.Windows;
using System;
using System.Windows;

namespace BilliardGameTablesManagement.Views
{
    public partial class StartWindow : Window
    {
        public StartWindow()
            : this(ApplicationServices.CreateStartViewModel())
        {
        }

        public StartWindow(StartViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));
        }
    }
}
