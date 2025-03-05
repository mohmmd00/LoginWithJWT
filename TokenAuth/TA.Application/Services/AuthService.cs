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

        public void Register(RegisterRequest request)
        {
            var selectedUser = _userRepository.GetUserbyUsername(request.Username);
            if (selectedUser != null) //check if any user exists in db with this username
                throw new Exception("Username already exists.");


            string salt = Guid.NewGuid().ToString(); //create a anew guid for salt 
            string passwordHash = _passwordService.HashPassword(request.Password, salt); // give password and new generated salt to hasher 

            var user = new User(request.Username, passwordHash, salt);



            //make sure that users save in db make sure IUserRepository does have a method to save newly created user !!!!!!!!!!!!!!
            _userRepository.SaveNewUser(user); //thats more like it 

        }
        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();

            var fetchedUser = _userRepository.GetUserbyUsername(request.Username);

            if (fetchedUser == null)
            {
                response.IsInformationCorrect = false;
                response.Message = "Invalid username or password";
                response.Token = "";

            }
            else if (fetchedUser != null)
            {
                bool isPasswordValid = _passwordService.VerifyPassword(request.Password, fetchedUser.Salt, fetchedUser.Password);
                if (isPasswordValid)
                {
                    string sessionId = Guid.NewGuid().ToString();

                    var newSession = new Session(fetchedUser.PrimaryId, sessionId);

                    _sessionRepository.SaveNewSession(newSession);

                    response.IsInformationCorrect = true;
                    response.Message = "Login successful";
                    response.Token = _jwtService.GenerateToken(fetchedUser.PrimaryId, sessionId);
                }
                else
                {
                    response.IsInformationCorrect = false;
                    response.Message = "Invalid username or password";
                    response.Token = "";
                }
                
            }
            else
            {
                throw new Exception("somthing went wrong!");
            }







            return response;
        }
    }
}
