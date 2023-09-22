using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public class MemoryCosmeticsService : ICosmeticsService
    {
        private List<Cosmetics> _cosmetics;
        private List<Category> _categories;
        static private IConfiguration _config;
        public MemoryCosmeticsService([FromServices] IConfiguration? config, IShopService shopService, int pageNo = 1)
        {
            if(_config == null)
                _config = config;
            _categories = shopService.GetCategoryListAsync().Result.Data;
            SetupData();
        }
        public Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Cosmetics>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            int itemsPerPage = Int32.Parse(_config["ItemsPerPage"]);
            List<Cosmetics> rightData = _cosmetics.FindAll(item => (categoryNormalizedName == null || item.Category?.NormalizedName == categoryNormalizedName));
            int countOfProducts = rightData.Count();

            var result = new ResponseData<ProductListModel<Cosmetics>>();
            result.Data = new ProductListModel<Cosmetics>();
            result.Data.Items = rightData.Skip(itemsPerPage * (pageNo - 1)).Take(itemsPerPage).ToList();
            result.Data.TotalPages = countOfProducts / itemsPerPage;
            if (countOfProducts % itemsPerPage != 0)
            {
                ++result.Data.TotalPages;
            }
            result.Data.CurrentPage = pageNo;
            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Cosmetics product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetupData()
        {
            _cosmetics = new List<Cosmetics>
         {
             new Cosmetics {Id = 1, Name="LA ROCHE-POSAY TOLERIANE очищающая гель-пенка двойного действия",
                Description="Эффективно очищает кожу от загрязнений, макияжа (включая макияж для глаз) и загрязняющих частиц окружающей среды. ",
                Price =70, Image="/Images/LA_ROCHE_POSAY_TOLERIANE.jpeg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("face"))},
             new Cosmetics { Id = 2, Name="Vichy Mineral 89 Крем увлажняющий для сухой кожи",
                Description="Насыщенный увлажняющий крем с высокой концентрацией липидов природного происхождения, восстанавливающий защитный барьер, восполняющий уровень липидов кожи, обеспечивая длительное увлажнение до 72 часов.",
                Price =49.80, Image="/Images/Vichy_Mineral_89.jpeg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("face"))},
             new Cosmetics {Id = 3, Name="La Roche-Posay Toleriane Rosaliac AR Крем уход для лица",
                Description="Мгновенно уменьшает видимость покраснений и предупреждает их повторное появление, выравнивает тон кожи, увлажняет.",
                Price =71.14, Image="/Images/La_Roche_Posay_Toleriane_Rosaliac.jpeg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("face"))},
             new Cosmetics { Id = 4, Name="VICHY NORMADERM Сыворотка пробиотическая",
                Description="Лаборатории VICHY объединили Вулканическую воду VICHY с 5% Салициловой + Гликолевой кислотами и Пробиотической фракцией.",
                Price =49.80, Image="/Images/VICHY_NORMADERM.jpeg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("face"))},
             new Cosmetics { Id = 5, Name="Виши Супрем Лифтактив Концентрированная сыворотка с витамином С",
                Description="Гипоаллергенная формула обогащена вулканической водой Vichy и эффективными антиоксидантами: чистый витамин С в концентрации 15%, пикногенол и Витамин Е.",
                Price =49.80, Image="/Images/VICHY_LIFTACTIV_SUPREME.jpeg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("face"))},
             new Cosmetics {Id = 6, Name="FARCOM HD Мусс для волос для локонов KERATIN PROVITAMIN B5",
                Description="Мусс для укладки волос ультрасильной, объемной и стойкой фиксации на основе кератина и провитамина B5.  Обеспечивает максимальный контроль над укладкой для создания уникальных современных причесок.",
                Price =52.32, Image="/Images/KERATIN_PROVITAMIN_B5.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("hair"))},
             new Cosmetics { Id = 7, Name="Inebrya Blondesse Фиолетовая обесцвечивающая пудра до 7 тонов Инебрия",
                Description="Miracle Gentle Lightener-Protect — это обесцвечивающий порошок, содержащий фиолетовый пигмент, для безопасного и защищенного осветления за один шаг до 7 тонов.",
                Price =4.29, Image="/Images/Inebrya_Blondesse.png",
                Category=_categories.Find(c=>c.NormalizedName.Equals("hair"))},
             new Cosmetics {Id = 8, Name="Farcom MEA NATURA Olive Увлажняющее молочко для тела Олива Натуральное",
                Description="Увлажняющее молочко для тела на основе органически выращенного оливкового масла первого отжима *, витамина Е и миндального масла - эксклюзивной смеси натуральных ингредиентов с антиоксидантными свойствами.",
                Price =20.74, Image="/Images/Farcom_MEA_NATURA_Olive.png",
                Category=_categories.Find(c=>c.NormalizedName.Equals("body"))},
             new Cosmetics { Id = 9, Name="Farcom MEA NATURA Olive Увлажняющее и питательное крем-масло для тела с оливковым маслом Натуральное",
                Description="Интенсивное увлажняющее крем-масло для тела с органическим оливковым маслом*, витамином Е и пчелиным воском, натуральная смесь ингредиентов с антиоксидантными.",
                Price =20.65, Image="/Images/Farcom_MEA_NATURA_Olive_Butter.png",
                Category=_categories.Find(c=>c.NormalizedName.Equals("body"))},
             new Cosmetics { Id = 10, Name="Норева Эксфолиак Интенсивный очищающий пенящийся гель для тела",
                Description="Для кожи, пораженной серьезными пятнами, может потребоваться более глубокое очищение.",
                Price =48.70, Image="/Images/Noreva_Exfoliac.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("body"))},
        };
        }
    }

}
