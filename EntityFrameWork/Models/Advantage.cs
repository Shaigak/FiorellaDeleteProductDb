namespace EntityFrameWork.Models
{
    public class Advantage:BaseEntity
    {
        public string? Name { get; set; }

        public int AboutId { get; set; }

        public About About { get; set; }

        
    }
}
