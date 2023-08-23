namespace Application.Features.Prospecto.Command.RegistrarProspecto
{
    using Application.Contracts.Repositories.Base;
    using Application.Helper;
    using Domain.Entities;
    using FluentValidation;
    public class RegistrarProspectoValidator : AbstractValidator<RegistrarProspectoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegistrarProspectoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.MaestroProspectoId)
                .MustAsync(isMaestroProspectoIdExist)
                .WithMessage(x => string.Format(ValidationMessages.ForeignKeyNotExist, x.MaestroProspectoId));
            RuleFor(x => x.linea)
                .MustAsync(isLineaIdExist)
                .WithMessage(x => string.Format(ValidationMessages.ForeignKeyNotExist, x.linea));
            RuleFor(x => x.OrigenVenta)
                .MustAsync(isOrigenVentaExist)
                .WithMessage(x => string.Format(ValidationMessages.ForeignKeyNotExist, x.OrigenVenta)); 
            RuleFor(x => x.Medio)
                .MustAsync(isMedioExist)
                .WithMessage(x => string.Format(ValidationMessages.ForeignKeyNotExist, x.OrigenVenta)); 
            RuleFor(x => x.Zona)
                .MustAsync(isZonaExist)
                .WithMessage(x => string.Format(ValidationMessages.ForeignKeyNotExist, x.Zona));

        }
        private async Task<bool> isMaestroProspectoIdExist(int maestroProspectoId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<MaestroProspecto>().AnyAsync(x => x.MaeId == maestroProspectoId, cancellationToken);
            return exist;
        }
        private async Task<bool> isLineaIdExist(int lineaId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<Lineas>().AnyAsync(x => x.NlinId == lineaId, cancellationToken);
            return exist;
        }
        private async Task<bool> isOrigenVentaExist(string origenVentaId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<OrigenVentas>().AnyAsync(x => x.Corivta == origenVentaId, cancellationToken);
            return exist;
        }
        private async Task<bool> isMedioExist(int medioId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<Medios>().AnyAsync(x => x.MedId == medioId, cancellationToken);
            return exist;
        }
        private async Task<bool> isEstadoExist(int estadoId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<Estados>().AnyAsync(x => x.Id == estadoId, cancellationToken);
            return exist;
        }
        private async Task<bool> isZonaExist(int zonaId, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<Zonas>().AnyAsync(x => x.ZonId == zonaId, cancellationToken);
            return exist;
        }
    }
}
