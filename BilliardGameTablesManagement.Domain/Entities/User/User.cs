namespace BilliardGameTablesManagement.Domain.Entities.User
{
    public class User : Person
    {
        public User(int PersonId, long NationalityNum, string FirstName, string LastName, DateTime DoB,
                    char Gender, string Email, string Phone, string Address, int UserId, string Username,
                     string Password)
            :base(PersonId, NationalityNum, FirstName, LastName, DoB, Gender, Email, Phone, Address)
        {
            this.UserId = UserId;
            this.Username = Username;
            this.Passowrd = Password;
        }
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Passowrd { get; set; }

    }
}