using EntityFrameWork.Models;

namespace EntityFrameWork.ViewModels
{
    public class HomeVm
    {
        public List<Slider> Sliders { get; set; }
        public IEnumerable<Category>Categories { get; set; }

        public IEnumerable<Product> Products { get; set; }

    

        public SliderInfo Sliderİnfo { get; set; }

        public IEnumerable<About> Abouts { get; set; }

        public IEnumerable<Advantage> Advantages { get; set; }

        public IEnumerable<Flower> Flowers { get; set; }

        public IEnumerable<Workers> Workers { get; set; }


        public IEnumerable<Florist> Florists { get; set; }

        public IEnumerable<Instagram> Instagrams { get; set; }

        public IEnumerable<Subscribe> Subscribes { get; set; }






    }
}
