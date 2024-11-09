namespace Application.DTOs.User
{
    public class CreateUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

      

        public CreateUserDTO(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public CreateUserDTO()
        {
        }
    }
}

