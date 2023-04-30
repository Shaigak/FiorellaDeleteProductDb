using System.ComponentModel.DataAnnotations;

namespace EntityFrameWork.Areas.Admin.View_Models
{
    public class ProductCreateVM
    {
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Count { get; set; }
       
        [Required]
        public string Price { get; set; }
       
        [Required]
        public List<IFormFile> Photos { get; set; }

        public int CategoryId { get; set; }
    }
}
