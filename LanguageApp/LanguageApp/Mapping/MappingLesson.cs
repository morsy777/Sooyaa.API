using LanguageApp.DTOS;

namespace LanguageApp.Mapping
{
    public class MappingLesson : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Lesson, LessonDTO>();


            config.NewConfig<LessonDTORequest, Lesson>();

        }
    }
}
