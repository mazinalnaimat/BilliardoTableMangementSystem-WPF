

namespace BilliardGameTablesManagement.Domain.Entities
{
    public class Person
    {
        public Person(int PersonId, long NationalityNum, string FirstName, string LastName, DateTime DoB,
                      char Gender, string Email, string Phone, string Address)
        { 
            this.PersonId = PersonId;
            this.NationalityNum = NationalityNum;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DoB = DoB;
            this.Gender = Gender;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;

        }
        public int PersonId { get; set; }

        public long NationalityNum { get; set; }

        public  string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        public char Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
