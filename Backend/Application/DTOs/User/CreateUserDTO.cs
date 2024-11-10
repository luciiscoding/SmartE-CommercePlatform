namespace Application.DTOs.User
{
    public class CreateUserDTO : LoginUserDTO
    {
        public string Email { get; set; }

        public CreateUserDTO(string username, string password, string email) : base(username, password)
        {
            Email = email;
        }

        public CreateUserDTO()
        {
        }
    }
}

