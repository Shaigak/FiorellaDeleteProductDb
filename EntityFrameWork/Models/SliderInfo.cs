using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork.Models
{
    public class SliderInfo:BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string SignatureImage { get; set; }

        [NotMapped]
        [Required(ErrorMessage ="Dont Empty Image")]
        public IFormFile Photo { get; set; }
    }
}
