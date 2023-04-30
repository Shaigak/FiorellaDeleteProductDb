using EntityFrameWork.Models;

namespace EntityFrameWork.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public  BlogHeader BlogHeader { get; set; }

    }
}
