namespace Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
