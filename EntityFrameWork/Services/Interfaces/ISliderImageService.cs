using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface ISliderImageService
    {
        Task<IEnumerable<Slider>> GetImage();
    }
}
