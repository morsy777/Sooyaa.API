using LanguageApp.DTOS;

namespace LanguageApp.Mapping
{
    public class MappingLanguage : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Language,LanguagesDTO>();
        }


    }
}
