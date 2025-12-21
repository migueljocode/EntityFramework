namespace LibraryTestConsole.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public String Title { get; set; }
        public String? Description { get; set; }
        public String Author { get; set; }
        public String ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CopiesAvailable { get; set; }

        public ICollection<Loan> Loans { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
