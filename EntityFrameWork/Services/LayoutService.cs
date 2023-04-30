using EntityFrameWork.Data;
using EntityFrameWork.Services.Interfaces;
using EntityFrameWork.ViewModels;
using Newtonsoft.Json;

namespace EntityFrameWork.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketService _basketService;

        public LayoutService(AppDbContext context, IBasketService basketService,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
        }

        public LayoutVM GetSettingDatas()
        {
            Dictionary<string,string> settings = _context.Settings.AsEnumerable().ToDictionary( m => m.Key, m => m.Value);
            List<BasketVM> basketDatas = _basketService.GetBasketDatas();

            //int count = basketDatas.Sum(m => m.Count);

            //LayoutVM model = new()
            //{
            //    Settings = settings,
            //    BasketCount = count
            //};

            return new LayoutVM { Settings = settings, BasketCount = basketDatas.Sum(m => m.Count) };
        }
    }
}
