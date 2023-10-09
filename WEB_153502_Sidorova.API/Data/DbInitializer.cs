using CosmeticsShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WEB_153502_Sidorova.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Выполнение миграций
            await context.Database.MigrateAsync();
            context.Categories.AddRange(
                new Category
                {
                    Name = "Для лица",
                    NormalizedName = "face"
                },
                new Category
                {
                    Name = "Для тела",
                    NormalizedName = "body"
                },
                new Category
                {
                    Name = "Для волос",
                    NormalizedName = "hair"
                }
            );
            context.CosmeticsSet.AddRange(
             new Cosmetics
             {
                 Name = "LA ROCHE-POSAY TOLERIANE очищающая гель-пенка двойного действия",
                 Description = "Эффективно очищает кожу от загрязнений, макияжа (включая макияж для глаз) и загрязняющих частиц окружающей среды. ",
                 Price = 70,
                 Image = app.Configuration["applicationUrl"] + "/Images/LA_ROCHE_POSAY_TOLERIANE.jpeg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("face"))
             },
             new Cosmetics
             {
                 Name = "Vichy Mineral 89 Крем увлажняющий для сухой кожи",
                 Description = "Насыщенный увлажняющий крем с высокой концентрацией липидов природного происхождения, восстанавливающий защитный барьер, восполняющий уровень липидов кожи, обеспечивая длительное увлажнение до 72 часов.",
                 Price = 49.80,
                 Image = app.Configuration["applicationUrl"] + "/Images/Vichy_Mineral_89.jpeg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("face"))
             },
             new Cosmetics
             {
                 Name = "La Roche-Posay Toleriane Rosaliac AR Крем уход для лица",
                 Description = "Мгновенно уменьшает видимость покраснений и предупреждает их повторное появление, выравнивает тон кожи, увлажняет.",
                 Price = 71.14,
                 Image = app.Configuration["applicationUrl"] + "/Images/La_Roche_Posay_Toleriane_Rosaliac.jpeg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("face"))
             },
             new Cosmetics
             {
                 Name = "VICHY NORMADERM Сыворотка пробиотическая",
                 Description = "Лаборатории VICHY объединили Вулканическую воду VICHY с 5% Салициловой + Гликолевой кислотами и Пробиотической фракцией.",
                 Price = 49.80,
                 Image = app.Configuration["applicationUrl"] + "/Images/VICHY_NORMADERM.jpeg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("face"))
             },
             new Cosmetics
             {
                 Name = "Виши Супрем Лифтактив Концентрированная сыворотка с витамином С",
                 Description = "Гипоаллергенная формула обогащена вулканической водой Vichy и эффективными антиоксидантами: чистый витамин С в концентрации 15%, пикногенол и Витамин Е.",
                 Price = 49.80,
                 Image = app.Configuration["applicationUrl"] + "/Images/VICHY_LIFTACTIV_SUPREME.jpeg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("face"))
             },
             new Cosmetics
             {
                 Name = "FARCOM HD Мусс для волос для локонов KERATIN PROVITAMIN B5",
                 Description = "Мусс для укладки волос ультрасильной, объемной и стойкой фиксации на основе кератина и провитамина B5.  Обеспечивает максимальный контроль над укладкой для создания уникальных современных причесок.",
                 Price = 52.32,
                 Image = app.Configuration["applicationUrl"] + "/Images/KERATIN_PROVITAMIN_B5.jpg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("hair"))
             },
             new Cosmetics
             {
                 Name = "Inebrya Blondesse Фиолетовая обесцвечивающая пудра до 7 тонов Инебрия",
                 Description = "Miracle Gentle Lightener-Protect — это обесцвечивающий порошок, содержащий фиолетовый пигмент, для безопасного и защищенного осветления за один шаг до 7 тонов.",
                 Price = 4.29,
                 Image = app.Configuration["applicationUrl"] + "/Images/Inebrya_Blondesse.png",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("hair"))
             },
             new Cosmetics
             {
                 Name = "Farcom MEA NATURA Olive Увлажняющее молочко для тела Олива Натуральное",
                 Description = "Увлажняющее молочко для тела на основе органически выращенного оливкового масла первого отжима *, витамина Е и миндального масла - эксклюзивной смеси натуральных ингредиентов с антиоксидантными свойствами.",
                 Price = 20.74,
                 Image = app.Configuration["applicationUrl"] + "/Images/Farcom_MEA_NATURA_Olive.png",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("body"))
             },
             new Cosmetics
             {
                 Name = "Farcom MEA NATURA Olive Увлажняющее и питательное крем-масло для тела с оливковым маслом Натуральное",
                 Description = "Интенсивное увлажняющее крем-масло для тела с органическим оливковым маслом*, витамином Е и пчелиным воском, натуральная смесь ингредиентов с антиоксидантными.",
                 Price = 20.65,
                 Image = app.Configuration["applicationUrl"] + "/Images/Farcom_MEA_NATURA_Olive_Butter.png",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("body"))
             },
             new Cosmetics
             {
                 Name = "Норева Эксфолиак Интенсивный очищающий пенящийся гель для тела",
                 Description = "Для кожи, пораженной серьезными пятнами, может потребоваться более глубокое очищение.",
                 Price = 48.70,
                 Image = app.Configuration["applicationUrl"] + "/Images/Noreva_Exfoliac.jpg",
                 Category = context.Categories.ToList().Find(c => c.NormalizedName.Equals("body"))
             }
        );
            context.SaveChanges();
        }
    }
}
