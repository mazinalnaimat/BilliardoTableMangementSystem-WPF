


namespace BilliardGameTablesManagement.Domain.Entities.User
{
    public class ActiveUser : User
    {
        public ActiveUser(int PersonId, long NationalityNum, string FirstName, string LastName, DateTime DoB,
                    char Gender, string Email, string Phone, string Address, int UserId, string Username,
                     string Password, int ActiveUserId)
            : base(PersonId, NationalityNum, FirstName, LastName, DoB, Gender, Email, Phone, Address, UserId, Username,
                  Password)
        {
            this.ActiveUserId = ActiveUserId;
        }
        public int ActiveUserId { get; set; }
    }
}
