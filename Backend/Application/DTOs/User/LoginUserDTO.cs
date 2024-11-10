namespace Application.DTOs.User
{
    public class LoginUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public LoginUserDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginUserDTO()
        {
        }
    }
}
