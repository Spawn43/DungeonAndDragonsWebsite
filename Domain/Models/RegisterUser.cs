namespace Domain.Models
{
    public class RegisterUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
