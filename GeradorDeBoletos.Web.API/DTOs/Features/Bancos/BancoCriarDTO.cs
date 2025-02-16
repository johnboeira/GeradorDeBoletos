using FluentValidation;

namespace GeradorDeBoletos.Web.API.DTOs.Features.Bancos;

public class BancoCriarDTO
{
    public string NomeBanco { get; set; }
    public string CodigoBanco { get; set; }
    public decimal PercentualDeJuros { get; set; }

    public class BancoValidator : AbstractValidator<BancoCriarDTO>
    {
        public BancoValidator()
        {
            RuleFor(b => b.NomeBanco)
                .NotEmpty().WithMessage("O nome do banco é obrigatório")
                .MaximumLength(100).WithMessage("O nome do banco deve ter no máximo 100 caracteres");

            RuleFor(b => b.CodigoBanco)
                .NotEmpty().WithMessage("O código do banco é obrigatório")
                .MaximumLength(20).WithMessage("O código do banco deve ter no máximo 20 caracteres");

            RuleFor(b => b.PercentualDeJuros)
                .NotNull().WithMessage("O percentual de juros é obrigatório")
                .GreaterThanOrEqualTo(0).WithMessage("Percentual deve ser igual ou maior que 0");
        }
    }
}