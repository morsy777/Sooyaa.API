using LanguageApp.DTOS;

namespace LanguageApp.Mapping
{
    public class MappingHomePage : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, homePageDTO>()
                .Map(dest => dest.streakScore,
                    src => src.UserStreak == null ? 0 : src.UserStreak.CurrentStreak);
                //.Map(dest => dest.userLevel,
                //    src => src.SelectedLevel == null ? "" : src.SelectedLevel.Name);
        }
    }

}
