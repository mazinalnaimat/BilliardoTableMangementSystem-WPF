using System.Windows;


namespace BilliardGameTablesManagement.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.StartViewModel();
        }
    }
}
