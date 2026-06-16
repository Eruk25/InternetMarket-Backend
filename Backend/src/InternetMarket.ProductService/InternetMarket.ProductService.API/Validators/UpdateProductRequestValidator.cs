using FluentValidation;
using InternetMarket.ProductService.API.DTOs.Requests;

namespace InternetMarket.ProductService.API.Validators
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Название товара обязательно")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно")
                .MaximumLength(2000);

            RuleFor(x => x.Price)
                .GreaterThan(0.9m).WithMessage("Цена должна быть больше 0.9 BYN");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Количество не может быть отрицательным");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Вес должен быть больше 0");

            RuleFor(x => x.Length)
                .GreaterThan(0).WithMessage("Длина должна быть больше 0");

            RuleFor(x => x.Width)
                .GreaterThan(0).WithMessage("Ширина должна быть больше 0");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Высота должна быть больше 0");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория обязательна");

            RuleFor(x => x.ProviderId)
                .NotEmpty().WithMessage("Поставщик обязателен");
        }
    }
}
