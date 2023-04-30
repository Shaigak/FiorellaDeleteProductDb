using EntityFrameWork.Models;

namespace EntityFrameWork.Areas.Admin.View_Models
{
    public class ProductListVM
    {





        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }

        public string MainImage { get; set; }

        public string CategoryName { get; set; }
    }
}


