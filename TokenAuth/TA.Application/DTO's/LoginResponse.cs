namespace TA.Application.DTO_s
{
    public class LoginResponse
    {
        public bool IsInformationCorrect { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
