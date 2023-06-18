namespace Mansur_TodoGF.Areas.Identity.Data
{
    public class Todo
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
