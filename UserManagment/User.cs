namespace UserManagment
{
    public class User
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "User Name field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
