using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork.Models
{
    public class Workers:BaseEntity
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }


        public string? Position { get; set; }
    }
}
