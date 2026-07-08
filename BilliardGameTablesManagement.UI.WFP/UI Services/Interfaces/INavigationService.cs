namespace BilliardGameTablesManagement.Services.Interfaces
{
    public interface INavigationService
    {
        void NavigateToLogin(object currentDataContext);

        void NavigateToBilliardTables(object currentDataContext);

        void Shutdown();
    }
}
