using FluentValidation;
using TestProject.General;
using TestProject.Services.Anchors;
using TestProject.Services.Contracts;
using TestProject.Services.Contracts.Exceptions;
using TestProject.Services.Contracts.Models;
using TestProject.Services.Validators;

namespace TestProject.Services.Services
{
    public class ServicesValidator : IServiceValidator
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ServicesValidator()
        {
            validators.Add(typeof(OrderModel), new OrderModelValidator());
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TestProjectValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
