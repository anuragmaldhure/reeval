namespace Online_Exam.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Role { get; set; } // "User" or "Admin"
    }
}
