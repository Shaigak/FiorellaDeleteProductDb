using EntityFrameWork.Models;

namespace EntityFrameWork.Areas.Admin.View_Models
{
    public class SliderUpdateVM
    {

        public string Title { get; set; }

        
        public string Description { get; set; }

        public string SignatureImage { get; set; }
        public IFormFile Photo { get; set; }
    }
}
