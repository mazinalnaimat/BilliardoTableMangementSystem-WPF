using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Models;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class UserInfoViewModel : BaseViewModel
    {
        public UserInfoViewModel(UserInfoModel userInfo, IWindowService windowService)
        {
            UserInfo = userInfo
                ?? throw new ArgumentNullException(nameof(userInfo));

            if (windowService == null)
                throw new ArgumentNullException(nameof(windowService));

            CloseCommand = new CloseWindowCommand(windowService, this);
        }

        public UserInfoModel UserInfo { get; }

        public ICommand CloseCommand { get; }
    }
}
