using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.Extensions
{
    public static class FluentValidationServiceCollections
    {
        public static void AddFluentValidationValidators(this IServiceCollection services)
        {
            var validatorTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsAssignableToGenericType(typeof(AbstractValidator<>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var genericArg = validatorType.BaseType!.GetGenericArguments().FirstOrDefault();
                if (genericArg != null)
                {
                    var serviceType = typeof(IValidator<>).MakeGenericType(genericArg);
                    services.AddScoped(serviceType, validatorType);
                }
            }
        }
    }
}
