using AutoMapper;
using FluentValidation;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;

namespace HelpersLayer.Helpers.Bases
{
    public abstract class AppService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public AppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
        protected static async Task DoValidationAsync<TValidator, TRequest>(TRequest request, params object[] constructorParameters)
            where TValidator : AbstractValidator<TRequest>
        {
            if (request == null)
                throw new Exception(
                    $"يا ااااايمن اتأكد من ام الركويست عشان جاييلي بنل،" +
                    $" في حاجة بعتها غلط ،" +
                    $" شوف لو تايب معين بعتها غلط ،" +
                    $" يعني مثلا بدل ما تبعت نامبر بعت استرينج وهكذا"
                    );
            var instance = (TValidator)Activator.CreateInstance(typeof(TValidator), constructorParameters)!;

            var validateResult = await instance.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
        }

    }
}
