namespace BilliardGameTablesManagement.Services.Interfaces
{
    public interface IWindowService
    {
        void ShowLoginWindow();

        void ShowBilliardTablesWindow();

        void CloseWindowByDataContext(object dataContext);

        void Shutdown();
    }
}
