using FluentValidation;
using FluentValidation.Results;
using Sammak.Core.Common.Util;

namespace Sammak.SandBox.Models
{
    public abstract class ModelBase<T>
    {
        public static IValidator<T> Validator { get; private set; }

        static ModelBase()
        {
            Validator = DependencyResolver.GetInstance<IValidator<T>>();
        }

        public static bool Validate(T instance, out string validationMessage)
        {
            if(instance == null)
            {
                validationMessage = $"The model instance of type: {typeof(T).ToString()} cannot be null";
                return false;
            }
            var validationResult = Validator.Validate(instance);
            validationMessage = ValidationMessages(validationResult);
            return validationResult.IsValid;
        }

        public bool Validate(out string validationMessage)
        {
            var validationResult = Validator.Validate(this);
            validationMessage = ValidationMessages(validationResult);
            return validationResult.IsValid;
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }

        public static string ValidationMessages(ValidationResult validationResult)
        {
            var msg = "";
            if (validationResult.IsValid)
            {
                msg = "Model is valid.";
            }
            else
            {
                msg = "Model failed validation: ";
                foreach (var error in validationResult.Errors)
                {
                    msg += $"{error.ErrorMessage}; ";
                }
            }
            return msg;
        }

    }
}
