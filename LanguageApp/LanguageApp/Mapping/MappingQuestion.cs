namespace LanguageApp.Mapping
{
    public class MappingQuestion : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Question, QuestionDTO>();

            config.NewConfig<QuestionDTORequest, Question>();
        }


    }
}
