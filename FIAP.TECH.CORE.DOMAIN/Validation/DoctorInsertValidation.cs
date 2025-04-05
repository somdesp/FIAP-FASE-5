using FIAP.TECH.CORE.DOMAIN.Entities;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation;

public class DoctorInsertValidation : AbstractValidator<Doctor>
{
    public DoctorInsertValidation()
    {
        #region Attributes
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.CRM)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido corretamente.");


        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido corretamente.");

        #endregion
    }
}