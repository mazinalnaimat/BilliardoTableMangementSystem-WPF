using BilliardGameTablesManagement.Services;
using BilliardGameTablesManagement.ViewModels.Windows;
using System;
using System.Windows;

namespace BilliardGameTablesManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
            : this(new MainViewModel(ApplicationServices.CreateTableSessionService()))
        {
        }

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));
        }
    }
}
