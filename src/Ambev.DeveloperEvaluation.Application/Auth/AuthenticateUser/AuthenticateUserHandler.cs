using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthenticateUserHandler> _logger;

        public AuthenticateUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<AuthenticateUserHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to authenticate user with email: {Email}", request.Email);

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            _logger.LogInformation("User found. Verifying password...");
            var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.Password);

            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password for user: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            _logger.LogInformation("Password verified. Checking user status...");
            var activeUserSpec = new ActiveUserSpecification();
            if (!activeUserSpec.IsSatisfiedBy(user))
            {
                _logger.LogWarning("User is not active: {Email}", request.Email);
                throw new UnauthorizedAccessException("User is not active");
            }

            _logger.LogInformation("User is active. Generating token...");
            var token = _jwtTokenGenerator.GenerateToken(user);

            _logger.LogInformation("Authentication successful for user: {Email}", request.Email);
            return new AuthenticateUserResult
            {
                Token = token,
                Email = user.Email,
                Name = user.Username,
                Role = user.Role.ToString()
            };
        }
    }
}
