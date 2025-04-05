using FIAP.TECH.CORE.DOMAIN.Entities;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation;

public class ScheduleInsertValidation : AbstractValidator<Schedule>
{
    public ScheduleInsertValidation()
    {
        #region Attributes
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.IdDoctor)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");



        #endregion
    }
}