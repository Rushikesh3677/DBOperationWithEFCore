namespace DBOperationWIthEFCoreApp.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public int CurrencId { get; set; }
        public Book book { get; set; }
        public CurrencyType currency { get; set; }

    }
}
