using LanguageApp.DTOS;

namespace LanguageApp.Mapping
{
    public class MappingLevels : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<Level, LevelDTO>.NewConfig()
            .Map(dest => dest.LanId, src => src.LanguageId);

            TypeAdapterConfig<Level, LevelDTORequest>.NewConfig()
           .Map(dest => dest.LanId, src => src.LanguageId);
        }
    }
}
