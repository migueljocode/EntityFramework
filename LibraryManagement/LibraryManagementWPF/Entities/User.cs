namespace LibraryTestConsole.Entities
{
    public enum Role
    {
        Admin,
        Member
    }

    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String Username { get; set; }
        public String PasswordHash { get; set; }
        public String Email { get; set; }
        public Role Role{ get; set; }

        public ICollection<Loan> Loans { get; set; }
        public ICollection<Review> Reviews { get; set;}
    }
}
