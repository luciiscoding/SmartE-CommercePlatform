namespace Application.DTOs.User
{
    public class UserDTO : CreateUserDTO
    {
        public Guid? Id { get; set; }

        public UserDTO() : base()
        {
        }

        public UserDTO(Guid id, string username, string password, string email) : base(username, password, email)
        {
            Id = id;
        }
    }
}
