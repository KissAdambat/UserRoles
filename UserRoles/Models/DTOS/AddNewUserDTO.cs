namespace UserRoles.Models.DTOS
{
    public class AddNewUserDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
