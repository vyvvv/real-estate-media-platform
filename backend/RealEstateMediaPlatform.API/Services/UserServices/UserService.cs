using AutoMapper;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.DTOs.Admin;
using RealEstateMediaPlatform.API.DTOs.Agent;
using RealEstateMediaPlatform.API.DTOs.PhotographyCompany;
using RealEstateMediaPlatform.API.DTOs.User;
using RealEstateMediaPlatform.API.DTOs.User.IUser;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.Repositories.UserRepositories;
using RealEstateMediaPlatform.API.Services.EmailSenderServices;
using RealEstateMediaPlatform.API.Collections;


namespace RealEstateMediaPlatform.API.Services.UserServices
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtTokenService _jwtTokenService;
        private readonly RealEstateDbContext _dbContext;
        private readonly MongoDbContext _mongoDbContext;
        private readonly IEmailSenderService _emailSenderService;
        public UserService(IUserRepository userRepository,IMapper mapper, JwtTokenService jwtTokenService, RealEstateDbContext dbContext, MongoDbContext mongoDbContext, IEmailSenderService emailSenderService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _dbContext = dbContext;
            _mongoDbContext = mongoDbContext;
            _emailSenderService = emailSenderService;
        }


        //Create Admin user
        public async Task<AdminRegisterResponseDto> CreateAdminAccountAsync(AdminRegisterRequestDto adminRegisterRequestDto, string role)
        {

            var newAdminUser = await CreateUserWithRoleAsync(adminRegisterRequestDto, role);
            return _mapper.Map<AdminRegisterResponseDto>(newAdminUser);
           
        }

        //Create Agent
        public async Task<AgentRegisterResponseDto> CreateAgentAsync(AgentRegisterRequestDto agentRegisterRequestDto,string role)
        {
            //Generate the random password
            var generatedPassword = PasswordHelper.GenerateRandomPassword();
            agentRegisterRequestDto.Password = generatedPassword;
            var newAgentUser = await CreateUserWithRoleAsync(agentRegisterRequestDto,role);

            // Send the email with login credentials
            await _emailSenderService.SendEmailAsync(
                agentRegisterRequestDto.Email,
                "Your Agent Account Created",
                $"Email: {agentRegisterRequestDto.Email}\nPassword: {generatedPassword}\n Please Change Password ASAP"
            );

            return _mapper.Map< AgentRegisterResponseDto>(newAgentUser);

        }


        //Create Photography Company user
        public async Task<PhotographyCompanyRegisterResponseDto> CreatePhotographyCompanyAsync(PhotographyCompanyRegisterRequestDto photographyCompanyRegisterRequestDto, string role)
        { 
            var newPhotographCompanyUser = await CreateUserWithRoleAsync(photographyCompanyRegisterRequestDto, role);
            return _mapper.Map<PhotographyCompanyRegisterResponseDto>(newPhotographCompanyUser);
        }


        //Create User with role
        private async Task<User> CreateUserWithRoleAsync(IUserRegisterRequestDto dto, string role)
        {
            //Use transaction to ensure data consistency during user creation
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            //Check the email already exists
            try {
                var existUserByEmail = await _userRepository.FindByEmailAsync(dto.Email);

                if (existUserByEmail != null)
                {
                    throw new InvalidOperationException($"Email {dto.Email} is already registered");
                }
                User user;

                // Create different user types based on role
                if (role == "PhotographyCompany" && dto is PhotographyCompanyRegisterRequestDto pcDto)
                {
                 
                    user = _mapper.Map<PhotographyCompany>(pcDto);

                }
                else if (role == "Agent" && dto is AgentRegisterRequestDto agentDto)
                {
                    user = _mapper.Map<Agent>(agentDto);                    
                }
                else if (role == "Admin")
                {
                    // Check if any admin users already exist in the system
                    var existingAdmins = await _userRepository.GetUsersByRoleAsync("Admin");

                    //only one admin allowed in the system
                    if (existingAdmins.Any())
                    {
                        var existingAdmin = existingAdmins.First();
                        throw new InvalidOperationException($"Admin account already exists. {existingAdmin.Email}");
                    }
                    user = _mapper.Map<User>(dto);
                }
                else
                {
                    user = _mapper.Map<User>(dto);
                }

                await _userRepository.CreateAsync(user, dto.Password!);
                await _userRepository.AddToRoleAsync(user, role);
                await transaction.CommitAsync();

                //Log successful registration event
                await SaveLogEventAsync(
                    EventTypes.USER_REGISTER,
                    $"User registered successfully: {user.Email}, Role: {role}",
                    true, user.Id);

                //return registered user
                return user;
            }
            catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            };
        }

        //User login
        public async Task<UserLoginResponseDto> LoginAsync(UserLoginRequestDto userLoginRequestDto) 
        
        {
            //Check the email already exists
            var user = await _userRepository.FindByEmailAsync(userLoginRequestDto.Email);
            if (user == null)
            {
                await SaveLogEventAsync(
                        EventTypes.USER_LOGIN,
                        $"Login failed for {userLoginRequestDto.Email}: User not found",
                        false,null
                    );
                throw new InvalidOperationException($"Email {userLoginRequestDto.Email} is not registered");
            }

            // Verify the provided password against stored password hash

            var isPasswordValid = await _userRepository.CheckPasswordAsync(user, userLoginRequestDto.Password);
            if (!isPasswordValid)
            {        
                await SaveLogEventAsync(EventTypes.USER_LOGIN,
                    $"Login failed for {userLoginRequestDto.Email}: Invalid password",
                    false, null
                    );
                throw new UnauthorizedAccessException("Invalid email or password");

            }
            // get role
            var roles = await _userRepository.GetRolesAsync(user);          

            // generate JWT Token
            var token = _jwtTokenService.GenerateJwtToken(user, roles.ToList());

            //Return login details and Token

            var loginUser = _mapper.Map<UserLoginResponseDto>(user);
            loginUser.Token = token;
            loginUser.Roles = roles.ToList();
            loginUser.LoginTime = DateTime.Now;

            await SaveLogEventAsync(
                EventTypes.USER_LOGIN,
                $"User Id:{loginUser.Id}, UserName:{loginUser.UserName} logged in",
                true, loginUser.Id
                );
            return loginUser;
        }

        //Get current user "me" infornmations
        public async Task<UserCurrentResponseDto> GetCurrentUserAsync(string userId, string role) { 
        
            var currentUser = await _userRepository.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            List<int> userListingCasesId;

            //Get listing case IDs based on user role permissions
            switch (role.ToLower())
            {
                case "admin":
                    userListingCasesId = await _userRepository.GetAdminListingCasesId();
                    break;
                case "agent":
                    userListingCasesId = await _userRepository.GetAgentListingCasesId(userId);
                    break;
                case "photographycompany":
                    userListingCasesId = await _userRepository.GetCompanyListingCasesId(userId);
                    break;
                default:
                    userListingCasesId = new List<int>();
                    break;

            }

            var meUser = _mapper.Map<UserCurrentResponseDto>(currentUser);

            //Add user's role and listingcases id information to response
            meUser.Roles = [role];
            meUser.ListingCaseIds = userListingCasesId;

            return meUser;
        }


        //Get all Photography Company user
        public async Task<List<PhotographyCompanyRegisterResponseDto>> GetAllPhotographyCompanyAsync()
        {
            var companies = await _userRepository.GetAllPhotographyCompanyAsync();
            return _mapper.Map<List<PhotographyCompanyRegisterResponseDto>>(companies);
        }

        public async Task<List<AgentRegisterResponseDto>> GetAllAgentAsync()
        {
            var agents = await _userRepository.GetAllAgentAsync();
            return _mapper.Map<List<AgentRegisterResponseDto>>(agents);
        }


        //Save event log to database
        private async Task SaveLogEventAsync(string eventType,string message,bool isSuccess, string? userId)
        {
            var eventLog = new RealEstateEventLog
            {
                EventType = eventType,
                EventMessage = message,
                IsSuccess = isSuccess,
                OperatorUserId = userId

            };

            await _mongoDbContext.UserEventLogs.InsertOneAsync(eventLog);
        }


        public async Task<bool> UpdateAccountPasswordAsync(string userId, UserUpdatePasswordRequestDto updatePasswordDto)
        {
          //Get Valid User
           var currentUser = await _userRepository.GetUserByIdAsync(userId);
            if (currentUser == null) {
                throw new KeyNotFoundException("User not found");
            }

            //Update Password
            await _userRepository.UpdateAccountPasswordAsync(currentUser, updatePasswordDto.CurrentPassword,updatePasswordDto.NewPassword);
            
            await SaveLogEventAsync(
                          EventTypes.USER_ACCOUNT_UPDATE,
                          $"{currentUser.UserName} password updated",
                          true, currentUser.Id
                          );

            return true;

        }


        //Search Agent by agent's email
        public async Task<AgentGetDetailResponseDto> SearchAgentByEmailAsync(string agentEmail) {

            if (string.IsNullOrEmpty(agentEmail)) {

                throw new ArgumentException("Agent email cannot be empty", nameof(agentEmail));
            
            }

            var targetAgent = await _userRepository.GetAgentByEmailAsync(agentEmail);
            if (targetAgent == null) {
                throw new InvalidOperationException($"No agent found with email{agentEmail}");
            }

            return _mapper.Map<AgentGetDetailResponseDto>(targetAgent);
        }



        public async Task<bool>AddAgentToCompanyByEmailAsync(string companyId, string agentEmail)
        {


            var photoCompany = await _userRepository.GetPhotographyCompanyByIdAsync(companyId);

            if (photoCompany == null) {
               
                throw new InvalidOperationException($"No company found with Id {companyId}");

            }
            var targetAgent = await _userRepository.GetAgentByEmailAsync(agentEmail);

            if(targetAgent == null) {
                throw new InvalidOperationException($"No agent found with email{agentEmail}");
            }

            if(photoCompany.Agents.Any(a => a.Id == targetAgent.Id))
            {
                throw new InvalidOperationException($"Company already has this agent");

            }

            return await _userRepository.AddAgentToPhotoCompanyAsync(photoCompany, targetAgent);

        }

        //Get all agents of photography company
        public async Task<List<AgentGetDetailResponseDto>> GetAgentsForCompanyAsync(string companyId) {

            var photoCompany = await _userRepository.GetPhotographyCompanyByIdAsync(companyId);

            var agents = _userRepository.GetAgentsForCompanyAsync(photoCompany);
            return _mapper.Map<List<AgentGetDetailResponseDto>>(agents);
        }

    }  
  }