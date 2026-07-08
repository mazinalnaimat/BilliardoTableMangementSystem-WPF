using BilliardGameTablesManagement.Business.DTOs;

namespace BilliardGameTablesManagement.Models
{
    public class UserInfoModel
    {
        public int? UserId { get; set; }

        public int? PersonId { get; set; }

        public int? ActiveUserId { get; set; }

        public long? NationalityNumber { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string DisplayName => string.IsNullOrWhiteSpace(FullName) ? Username : FullName;

        public string Initials
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                    return $"{FirstName[0]}{LastName[0]}".ToUpperInvariant();

                if (!string.IsNullOrWhiteSpace(Username))
                    return Username[..Math.Min(2, Username.Length)].ToUpperInvariant();

                return "U";
            }
        }

        public static UserInfoModel FromLoginResult(LoginResultDto result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            return new UserInfoModel
            {
                UserId = result.UserId,
                PersonId = result.PersonId,
                ActiveUserId = result.ActiveUserId,
                NationalityNumber = result.NationalityNumber,
                Username = result.Username,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender == "m" ? "Male" : (result.Gender == "f") ? "Female": "",
                Email = result.Email,
                Phone = result.Phone,
                Address = result.Address
            };
        }
    }
}
