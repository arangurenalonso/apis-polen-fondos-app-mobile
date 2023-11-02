namespace Application.Features.Vendedor.Command.DarDeBajaVendedor
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Domain.Entities;
    using Application.Contracts.ApiExterna;
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using Application.Models.ConsumoApi.Bitrix24.Models;
    using Application.Exception;

    public class DarDeBajaVendedorCommandhandler : IRequestHandler<DarDeBajaVendedorCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IBitrix24ApiService _bitrix24ApiService;

        public DarDeBajaVendedorCommandhandler(IUnitOfWork unitOfWork, IMapper mapper, IVendedorRepository vendedorRepository,
            IBitrix24ApiService bitrix24ApiService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendedorRepository = vendedorRepository;
            _bitrix24ApiService = bitrix24ApiService;
        }


        public async Task<string> Handle(DarDeBajaVendedorCommand request, CancellationToken cancellationToken)
        {
            var vendedorCesados = await ObtenerVendedorCesados(request.CodVendedor);
            if (vendedorCesados.VenFcese==null)
            {
                vendedorCesados.VenFcese = DateTime.Now;
                await _unitOfWork.Repository<Vendedores>().UpdateAsync(vendedorCesados);
            }
            await _bitrix24ApiService.DesactivarUsuarioBitrix(vendedorCesados.BitrixID);

            var prospectosVendedorCesados = await ObtenerProspectosDelVendedor(vendedorCesados);
            var contar = 0;
            var numRegistro = 0;


            Console.WriteLine($"Total Registros a procesar:{prospectosVendedorCesados.Count}");
            foreach (var item in prospectosVendedorCesados)
            {
                try
                {
                    numRegistro++;
                    Console.WriteLine($"Inicio Nro:{numRegistro}");

                    var deal = await _bitrix24ApiService.ValidarExistenciaDealEnBitrix(item.ProId);
                    var contact = await _bitrix24ApiService.CRMContactGet(deal.CONTACT_ID);

                    var vendedorAsignado = await _vendedorRepository.ObtenerVendedorAsignado(0, item.VenSupcod);

                    await ActualizarDealBitrix24(deal, vendedorAsignado);
                    await ActualizarContactoBitrix24(contact, vendedorAsignado);

                    Prospectos prospectoCopy = _mapper.Map<Prospectos, Prospectos>(item);
                    prospectoCopy.VenCod = vendedorAsignado.VenCod;
                    prospectoCopy.VenSupcod = vendedorAsignado.VenSupCod;
                    prospectoCopy.VenGescod = vendedorAsignado.VenGesCod;
                    prospectoCopy.VenGercod = vendedorAsignado.VenGerCod;
                    await _unitOfWork.Repository<Prospectos>().AddAsync(prospectoCopy);


                    item.MotdesId = 12;
                    item.Reasignado = true;
                    await _unitOfWork.Repository<Prospectos>().UpdateAsync(item);

                    Console.WriteLine($"Fin Nro:{numRegistro}");



                    contar++;
                    if (contar == 15)
                    {
                        contar = 0;
                        await Task.Delay(TimeSpan.FromSeconds(120));
                    }
                }
                catch (System.Exception e)
                {
                    var mensaje = $"Error ocurrido en DarDeBajaVendedor --- {e.Message}";
                    var log = new LogFondos()
                    {
                        Fecha = DateTime.Now,
                        Mensaje = mensaje,
                        Tipo = "Polen",
                        Valor = System.Text.Json.JsonSerializer.Serialize(item)
                    };
                    await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                    await Task.Delay(TimeSpan.FromSeconds(120));
                }
            }
            return "Vendedor Cesado";
        }
        public async Task<Vendedores> ObtenerVendedorCesados(string CodVendedor)
        {
            var vendedor = (await _unitOfWork.Repository<Vendedores>().GetAsync(x =>
                    //x.VenFcese == null &&
                    x.VenCarCod == "003" &&
                    x.VenCod.Trim() == CodVendedor.Trim()
                )).FirstOrDefault();
            if (vendedor == null)
            {
                throw new NotFoundException($"No se encontró el vendedor con Cod: '{CodVendedor}'");
            }
            return vendedor;
        }
        public async Task<List<Prospectos>> ObtenerProspectosDelVendedor(Vendedores vendedorCesado)
        {
            var prospectos = await _unitOfWork.Repository<Prospectos>().GetAsync(x =>
                    x.MotdesId == null &&
                    vendedorCesado.VenCod == x.VenCod
                );
            if (prospectos.Count == 0)
            {
                throw new NotFoundException($"el vendedor con Cod: '{vendedorCesado.VenCod}' no tiene prospectos a repartir");
            }
            return prospectos;
        }
        private async Task<string> ActualizarDealBitrix24(DealBitrix24 dealBitrix24, Vendedores vendedor)
        {

            dealBitrix24.ASSIGNED_BY_ID = vendedor.BitrixID == null ? "" : vendedor.BitrixID;
            var request = new UpdateBitrixModel<DealBitrix24>()
            {
                id = dealBitrix24.ID.ToString(),
                fields = dealBitrix24
            };

            var result = await _bitrix24ApiService.CRMDealUpdate(request);
            return result;
        }

        private async Task<string> ActualizarContactoBitrix24(ContactBitrix24 contactBitrix24, Vendedores vendedor)
        {
            contactBitrix24.ASSIGNED_BY_ID = vendedor.BitrixID == null ? "" : vendedor.BitrixID;
            var request = new UpdateBitrixModel<ContactBitrix24>()
            {
                id = contactBitrix24.ID,
                fields = contactBitrix24

            };
            var result = await _bitrix24ApiService.CRMContactUpdate(request);
            return result;
        }
    }
}
