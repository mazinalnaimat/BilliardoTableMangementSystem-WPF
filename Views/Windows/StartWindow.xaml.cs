using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.ViewModels.Windows;
using System.Windows;

namespace BilliardGameTablesManagement.Views
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            DataContext = new StartViewModel(new WindowService());
        }
    }
}