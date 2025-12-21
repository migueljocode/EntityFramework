namespace LibraryTestConsole.Entities
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating {  get; set; }
        public String? Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
