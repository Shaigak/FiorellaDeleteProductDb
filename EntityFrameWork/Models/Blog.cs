using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork.Models
{
    public class Blog:BaseEntity
    {

        public string Header { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage ="Dont be empty image")]
        [NotMapped]
        public IFormFile Photo { get; set; }

        public DateTime Date { get; set; }

    }
}
