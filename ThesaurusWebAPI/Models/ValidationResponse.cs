namespace ThesaurusWebAPI.Models
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public static ValidationResponse Success() => new ValidationResponse { IsValid = true };
        public static ValidationResponse Fail(string message) => new ValidationResponse { IsValid = false, ErrorMessage = message };
    }
}
