namespace EntityFrameWork.Models
{
    public class About: BaseEntity
    {
        public string? Title { get; set; }
        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Info { get; set; }

        public ICollection<Advantage> Advantages { get; set; }


    }
}
