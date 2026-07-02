using BilliardGameTablesManagement.Domain.Entities;
using BilliardGameTablesManagement.Domain.Entities.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilliardGameTablesManagement.DataAccess.Repositories
{
    public class PersonRepository
    {
        public static Person? GetPersonByPersonId(int PersonId)

        {
            string query = @"
                            SELECT PersonId, NationalityNum, FirstName, LastName, DoB,
                            Gender, Email, Phone, Address
                            FROM People
                            WHERE PersonId = @PersonId";

            using SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@PersonId", SqlDbType.Int).Value = PersonId;

            try
            {
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    long NationalityNum = (long)reader["NationalityNum"];
                    string FirstName = (string)reader["FirstName"];
                    string LastName = (string)reader["LastName"];
                    DateTime DoB = (DateTime)reader["DoB"];
                    char Gender = Convert.ToChar(reader["Gender"]);
                    string Email = (string)reader["Email"];
                    string Phone = (string)reader["Phone"];
                    string Address = (string)reader["Address"];

                    return new Person(PersonId, NationalityNum, FirstName, LastName, DoB, Gender, Email, Phone, Address);

                }

                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
