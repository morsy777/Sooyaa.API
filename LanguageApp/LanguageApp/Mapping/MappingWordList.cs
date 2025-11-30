using LanguageApp.DTOS;

namespace LanguageApp.Mapping
{
    public class MappingWordList : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WordList, WordListDTO>();
            config.NewConfig<WordListRequestDTO, WordList>();
        }

    }
}
