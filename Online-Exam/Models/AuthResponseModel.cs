namespace Online_Exam.Models
{
    public class AuthResponseModel
    {
        public string Token { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }
    }
}
