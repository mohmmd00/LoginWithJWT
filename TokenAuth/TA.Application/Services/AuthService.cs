using TA.Application.DTO_s;
using TA.Domain.Entities;
using TA.Domain.Interfaces;


namespace TA.Application.Services
{
    public class AuthService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        public AuthService(ISessionRepository sessionRepository, IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task Register(RegisterRequest request)
        {
            var existingUser = _userRepository.GetUserby(request.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            string salt = Guid.NewGuid().ToString();
            string passwordHash = _passwordService.HashedPassword(request.Password, salt);

            var user = new User(request.Username, passwordHash, salt);



            //make sure that user saves in db make sure repository does have a method to save newly created user !!!!!!!!!!!!!!

        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var fetchedUser = _userRepository.GetUserby(request.Username);
            if (fetchedUser == null)
                throw new Exception("Invalid username or password");
            bool isPasswordValid = _passwordService.VerifiedPassword(request.Password, fetchedUser.Salt, fetchedUser.Password);
            if (!isPasswordValid)
                throw new Exception("Invalid username or password");

            string sessionId = Guid.NewGuid().ToString();

            var newSession = new Session(fetchedUser.PrimaryId, sessionId);

            _sessionRepository.SaveChanges(newSession);

            string token = _jwtService.GenerateToken(fetchedUser.PrimaryId, sessionId);


            return new LoginResponse { Token = token };
        }
    }
}
