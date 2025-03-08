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

        public RegisterResponse Register(RegisterRequest request)
        {

            var response = new RegisterResponse();

            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username))
            {
                response.IsInformationCorrect = false;
                response.Message = "Username or password cannot be null or white space";
                return response;
            }
            else
            {
                var selectedUser = _userRepository.GetUserbyUsername(request.Username);
                if (selectedUser != null)
                {
                    response.IsInformationCorrect = false;
                    response.Message = "username is already exists try another one";
                    return response;
                }
                else
                {
                    string salt = Guid.NewGuid().ToString(); //create a anew guid for salt 
                    string passwordHash = _passwordService.HashPassword(request.Password, salt); // give password and new generated salt to hasher 

                    var user = new User(request.Username, passwordHash, salt);

                    //make sure that users save in db make sure IUserRepository does have a method to save newly created user !!!!!!!!!!!!!!
                    _userRepository.CreateUser(user); //thats more like it 
                    _userRepository.SaveChanges();

                    var savedUser = _userRepository.GetUserbyUsername(user.Username);
                    var newSession = new Session(savedUser.PrimaryId, Guid.NewGuid().ToString());

                    _sessionRepository.CreateSession(newSession);
                    _sessionRepository.SaveChanges();

                    response.IsInformationCorrect = true;
                    response.Message = "register was successful";
                }
                return response;
            }

        }
        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();


            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                response.IsInformationCorrect = false;
                response.Message = "Username or password cannot be null or white space";
                response.Token = "";
                 
            }
            else
            {
                var fetchedUser = _userRepository.GetUserbyUsername(request.Username);
                if (fetchedUser == null)
                {      
                    response.IsInformationCorrect = false;
                    response.Message = "Invalid username or password";
                    response.Token = "";

                }
                else
                {
                    bool isPasswordValid = _passwordService.VerifyPassword(request.Password, fetchedUser.Salt, fetchedUser.Password);
                    if (isPasswordValid)
                    {
                        string sessionId = Guid.NewGuid().ToString();

                        var newSession = new Session(fetchedUser.PrimaryId, sessionId);

                        _sessionRepository.CreateSession(newSession);
                        _sessionRepository.SaveChanges();

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

            }
            return response;
        }
    }
}
