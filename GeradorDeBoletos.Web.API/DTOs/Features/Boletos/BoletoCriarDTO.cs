using FluentValidation;

namespace GeradorDeBoletos.Web.API.DTOs.Features.Boletos;

public class BoletoCriarDTO
{
    public string NomePagador { get; set; }
    public string CPFCNPJPagador { get; set; }
    public string NomeBeneficiario { get; set; }
    public string CPFCNPJBeneficiario { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public string Observacao { get; set; }
    public int BancoId { get; set; }

    public class CreateBoletoDtoValidator : AbstractValidator<BoletoCriarDTO>
    {
        public CreateBoletoDtoValidator()
        {
            RuleFor(x => x.NomePagador)
                .NotEmpty().WithMessage("O nome do pagador é obrigatório")
                .MaximumLength(200).WithMessage("O nome do pagador deve ter no máximo 200 caracteres");

            RuleFor(x => x.CPFCNPJPagador)
                .NotEmpty().WithMessage("O CPF/CNPJ do pagador é obrigatório")
                .MaximumLength(20).WithMessage("O CPF/CNPJ do pagador deve ter no máximo 20 caracteres")
                .Matches(@"^(?:\d{3}\.?\d{3}\.?\d{3}\-?\d{2}|\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2})$")
                .WithMessage("O CPF/CNPJ do pagador deve estar em um formato válido (CPF ou CNPJ)");

            RuleFor(x => x.NomeBeneficiario)
                .NotEmpty().WithMessage("O nome do beneficiário é obrigatório")
                .MaximumLength(200).WithMessage("O nome do beneficiário deve ter no máximo 200 caracteres");

            RuleFor(x => x.CPFCNPJBeneficiario)
                .NotEmpty().WithMessage("O CPF/CNPJ do beneficiário é obrigatório")
                .MaximumLength(20).WithMessage("O CPF/CNPJ do beneficiário deve ter no máximo 20 caracteres")
                .Matches(@"^(?:\d{3}\.?\d{3}\.?\d{3}\-?\d{2}|\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2})$")
                .WithMessage("O CPF/CNPJ do beneficiário deve estar em um formato válido (CPF ou CNPJ)");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero");

            RuleFor(x => x.DataVencimento)
                .NotEmpty().WithMessage("A data de vencimento é obrigatória")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("A data de vencimento não pode ser no passado");

            RuleFor(x => x.Observacao)
                .MaximumLength(500).WithMessage("A observação deve ter no máximo 500 caracteres");

            RuleFor(x => x.BancoId)
                .GreaterThan(0).WithMessage("O id do banco deve ser um valor válido, maior que 1");
        }
    }
}