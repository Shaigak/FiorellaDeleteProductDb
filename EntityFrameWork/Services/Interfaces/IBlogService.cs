using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface IBlogService
    {
      Task<IEnumerable<Blog>> GetAll();
      Task<BlogHeader> GetBlogHeader();
     
    }
}
