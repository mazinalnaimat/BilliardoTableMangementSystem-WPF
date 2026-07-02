using Microsoft.Data.SqlClient;
using System.Data;
using BilliardGameTablesManagement.Domain.Interfaces;
using BilliardGameTablesManagement.Domain.Entities.User;
using BilliardGameTablesManagement.Domain.Entities;
using BilliardGameTablesManagement.DataAccess.Repositories;

namespace BilliardGameTablesManagement.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ActiveUser? GetActiveUserByUsername(string Username)
        {
            string query = @"
                            SELECT UserID, ActiveUserId, UserName, Password, PersonId
                            FROM vw_ActiveUsersInfo
                            WHERE UserName = @UserName";

            using SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = Username;

            try
            {
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int PersonId = (int)reader["PersonId"];
                    Person ?Person = PersonRepository.GetPersonByPersonId(PersonId);
                    if (Person == null)
                    {
                        return null;
                    }

                    int UserId = (int)reader["UserId"];
                    int ActiveUserId = (int)reader["ActiveUserId"];
                    string Password = (string)reader["Password"];
                    return new ActiveUser(Person.PersonId, Person.NationalityNum, Person.FirstName, Person.LastName, Person.DoB,
                                          Person.Gender, Person.Email, Person.Phone, Person.Address, UserId, Username, Password,
                                          ActiveUserId);

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