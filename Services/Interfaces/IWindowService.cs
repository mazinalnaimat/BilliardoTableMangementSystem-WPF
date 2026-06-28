namespace BilliardGameTablesManagement.Services.Interfaces
{
    public interface IWindowService
    {
        void ShowBilliardTablesWindow();

        void CloseWindowByDataContext(object dataContext);

        void Shutdown();
    }
}