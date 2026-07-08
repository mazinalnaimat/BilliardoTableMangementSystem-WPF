namespace BilliardGameTablesManagement.Business.DTOs
{
    public class LoginResultDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int? UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public int? PersonId { get; set; }

        public int? ActiveUserId { get; set; }

        public long? NationalityNumber { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

    }
}
