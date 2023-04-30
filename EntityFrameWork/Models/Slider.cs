using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }


        [NotMapped]
        public IFormFile Photo { get; set; } 

    }
}
