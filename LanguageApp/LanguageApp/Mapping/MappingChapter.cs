
namespace LanguageApp.Mapping
{
    public class MappingChapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ChapterDTORequest, Chapter>()
             .Map(dest => dest.Name, src => src.Name.Trim());
        

            config.NewConfig<Chapter, ChapterDTO>()
                .Map(dest => dest.LevelId, src => src.LevelId);
        }
    }
}
