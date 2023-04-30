namespace EntityFrameWork.Areas.Admin.View_Models
{
    public class ExpertUpdateVM
    {

        public string Name { get; set; }

        public string Position { get; set; }

        public string Image { get; set; }

        public IFormFile Photo { get; set; }
    }
}
