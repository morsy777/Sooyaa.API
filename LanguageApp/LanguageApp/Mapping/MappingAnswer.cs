namespace LanguageApp.Mapping
{
    public class MappingAnswer : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Answer, AnswerDTO>();
            config.NewConfig<AnswerDTORequest, Answer>();

        }
    }
}
