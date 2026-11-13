namespace DBOperationWIthEFCoreApp.Data
{
    public class Language
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
    
}
