using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderInfo>> GetSliderData();
    }
}
