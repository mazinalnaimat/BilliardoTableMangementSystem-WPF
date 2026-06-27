using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.ViewModels;  // <-- add this
using System.Windows;

namespace BilliardGameTablesManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create ViewModels for each table
            var table1VM = new BilliardTableViewModel(new DispatcherTimerService()) {  TableNumber = 1, RatePerHour = 10.0m };
            var table2VM = new  BilliardTableViewModel(new DispatcherTimerService()) { TableNumber = 2, RatePerHour = 10.0m };
            var table3VM = new  BilliardTableViewModel(new DispatcherTimerService()) { TableNumber = 3, RatePerHour = 10.0m };
            var table4VM = new  BilliardTableViewModel(new DispatcherTimerService()) { TableNumber = 4, RatePerHour = 10.0m };
            var table5VM = new  BilliardTableViewModel(new DispatcherTimerService()) { TableNumber = 5, RatePerHour = 12.0m };
            var table6VM = new  BilliardTableViewModel(new DispatcherTimerService()) { TableNumber = 6, RatePerHour = 12.0m };

            // Assign each ViewModel to the corresponding UserControl
            btmTable1.DataContext = table1VM;
            btmTable2.DataContext = table2VM;
            btmTable3.DataContext = table3VM;
            btmTable4.DataContext = table4VM;
            btmTable5.DataContext = table5VM;
            btmTable6.DataContext = table6VM;
        }
    }
}