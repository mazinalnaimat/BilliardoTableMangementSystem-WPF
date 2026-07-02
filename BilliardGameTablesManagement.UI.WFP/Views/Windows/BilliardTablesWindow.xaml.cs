using BilliardGameTablesManagement.DependencyInjection;
using BilliardGameTablesManagement.ViewModels.Windows;
using System;
using System.Windows;

namespace BilliardGameTablesManagement
{
    public partial class BilliardTablesWindow : Window
    {
        public BilliardTablesWindow()
            : this(ApplicationServices.CreateBilliardTablesViewModel())
        {
        }

        public BilliardTablesWindow(BilliardTablesViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));
        }
    }
}
