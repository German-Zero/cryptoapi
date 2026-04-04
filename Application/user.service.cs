using cryptoapi.Domain;
using cryptoapi.Dto;
using cryptoapi.Entity;

namespace cryptoapi.Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> CreateUser(CreateUserDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);

            if (existing != null) 
                throw new Exception("El usuario ya existe");

            var user = new User
            {
                username = dto.Username,
                email = dto.Email,
                password = dto.Password
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangeAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.username,
                Email = user.email
            };
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null) 
                throw new Exception("Usuario no encontrado");
            
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.username,
                Email = user.email
            };
        }
    }
}
