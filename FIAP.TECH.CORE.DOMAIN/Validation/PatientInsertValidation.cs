using FIAP.TECH.CORE.DOMAIN.Entities;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation;

public class PatientInsertValidation : AbstractValidator<Patient>
{
    public PatientInsertValidation()
    {
        #region Attributes
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.CPF)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .Matches("^(\\d{3}.\\d{3}.\\d{3}-\\d{2})|(\\d{11})$").WithMessage("O campo {PropertyName} precisa ser valido.");

        #endregion
    }
}