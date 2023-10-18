namespace MvcMovie.Models
{
    public class ToolsHistory
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
