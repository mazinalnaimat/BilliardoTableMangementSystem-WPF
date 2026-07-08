namespace BilliardGameTablesManagement.Services.Interfaces
{
    public interface IWindowService
    {
        void ShowLoginWindow();

        void ShowBilliardTablesWindow();

        void ShowUserInfoWindow();

        void CloseWindowByDataContext(object dataContext);

        void Shutdown();
    }
}
