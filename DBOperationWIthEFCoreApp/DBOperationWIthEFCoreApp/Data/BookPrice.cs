namespace DBOperationWIthEFCoreApp.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public int CurrencId { get; set; }
        public virtual Book book { get; set; }
        public virtual CurrencyType currency { get; set; }

    }
}
