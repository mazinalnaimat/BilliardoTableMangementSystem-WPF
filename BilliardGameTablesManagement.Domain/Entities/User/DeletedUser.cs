using System;
using System.Collections.Generic;
using System.Text;

namespace BilliardGameTablesManagement.Domain.Entities.User
{
    internal class DeletedUser : User
    {
        public DeletedUser(int PersonId, long NationalityNum, string FirstName, string LastName, DateTime DoB,
                char Gender, string Email, string Phone, string Address, int UserId, string Username,
                 string Password, int DeletedUserId)
        : base(PersonId, NationalityNum, FirstName, LastName, DoB, Gender, Email, Phone, Address, UserId, Username,
              Password)
        {
            this.DeletedUserId = DeletedUserId;
        }
        int DeletedUserId { get; set; }
    }
}
