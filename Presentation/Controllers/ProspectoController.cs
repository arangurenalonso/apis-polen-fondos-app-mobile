namespace Presentation.Controllers
{
    using Application.Features.Prospecto.Command.EliminarDeal;
    using Application.Features.Prospecto.Command.PruebaBitrix;
    using Application.Features.Prospecto.Command.RegistrarMaestroProspecto;
    using Application.Features.Prospecto.Command.RegistrarPorIdDeal;
    using Application.Features.Prospecto.Command.RegistrarProspecto;
    using Application.Features.Prospecto.Command.RegistrarProspectoExistenteEnBitrix;
    using Application.Features.Prospecto.Command.RegistrarRedesSociales;
    using Application.Features.Prospecto.Query.ObtenerDatosFoja;
    using Application.Features.Prospecto.Query.ObtenerDatosProspectoPorVendedor;
    using Application.Features.Prospecto.Query.ObtenerDiscrepanciaDeOrigenes;
    using Application.Mappings.Prospecto.DTO;
    using DocumentFormat.OpenXml.Spreadsheet;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Controllers.Common;
    using Presentation.Filtros;
    using System.Net;
    public class ProspectoController : BaseApiController
    {
        public ProspectoController(IMediator mediator) : base(mediator) { }

        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        [HttpGet("Vendedor/{CodVendedor}/Documento/{nroDocumento}", Name = "ObtenerDatosProspectoPorVendedor")]
        [ProducesResponseType(typeof(SPGetDatosProspectoVendResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SPGetDatosProspectoVendResponse>> ObtenerDatosProspectoPorVendedor(string CodVendedor,
        string nroDocumento)
        {
            var query = new ObtenerDatosProspectoPorVendedorQuery(CodVendedor, nroDocumento);
            var grupos = await _mediator.Send(query);
            return Ok(grupos);
        }

        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        [HttpGet("Foja/{ProspectoId}", Name = "ObtenerProspectoDatosFoja")]
        [ProducesResponseType(typeof(SpGetListaDatosFojaResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SpGetListaDatosFojaResult>> ObtenerProspectoDatosFoja(int ProspectoId)
        {
            var query = new ObtenerDatosFojaProspectoQuery(ProspectoId);
            var fojaDataList = await _mediator.Send(query);
            return Ok(fojaDataList);
        }
        [HttpGet("RegistrarMaestroProspecto", Name = "RegistrarMaestroProspecto")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarMaestroProspecto([FromQuery] RegistrarMaestroProspectoCommand command)
        {
            var maestroProspectoId = await _mediator.Send(command);
            return Ok(maestroProspectoId);
        }
        [HttpGet("RegistrarProspecto", Name = "RegistrarProspecto")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarProspecto([FromQuery] RegistrarProspectoCommand command)
        {
            var ProspectoId = await _mediator.Send(command);
            return Ok(ProspectoId);
        }

        [HttpPost("RedesSociales", Name = "RegistrarProspectoRedesSociales")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarProspectoRedesSociales([FromBody] RegistrarProspectoRedesSocialesCommandDTORequest dtoRequest)
        {
            var command = new RegistrarProspectoRedesSocialesCommand(
                dtoRequest.Anuncio,
                dtoRequest.Plataforma,
                dtoRequest.Nombre,
                dtoRequest.Apellido,
                dtoRequest.Telefono,
                dtoRequest.Email,
                false,
                false,
                1,
                true
                );
            var prospecto = await _mediator.Send(command);
            return Ok(prospecto);
        }


        [HttpPost("RedesSociales/Masivo", Name = "RegistrarProspectoRedesSocialesMasivo")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> RegistrarProspectoRedesSocialesMasivo([FromBody] List<RegistrarProspectoRedesSocialesCommandDTORequest> listDTOS)
        {

            var contar = 0;
            var numRegistro = 0;
            foreach (var item in listDTOS)
            {
                numRegistro++;
                bool esUltimoRegistro = numRegistro == listDTOS.Count;

                Console.WriteLine($"Inicio Nro:{numRegistro}");
                var command = new RegistrarProspectoRedesSocialesCommand(
                item.Anuncio,
                item.Plataforma,
                item.Nombre,
                item.Apellido,
                item.Telefono,
                item.Email,
                true,
                esUltimoRegistro,
                numRegistro,
                true
                );
                var prospecto = await _mediator.Send(command);
                if (contar == 15)
                {
                    contar = 0;
                    await Task.Delay(TimeSpan.FromSeconds(120));
                }
                contar++;
                Console.WriteLine($"Fin Nro:{numRegistro}");
            }
            return Ok($"Se cargaron correctamente {listDTOS.Count}");
        }
        
        
        
        [HttpPost("RedesSociales/Masivo/SinBitrix", Name = "RegistrarProspectoRedesSocialesMasivoSinBitrix")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> RegistrarProspectoRedesSocialesMasivoSinBitrix([FromBody] List<RegistrarProspectoRedesSocialesCommandDTORequest> listDTOS)
        {

            var contar = 0;
            var numRegistro = 0;
            foreach (var item in listDTOS)
            {
                numRegistro++;
                bool esUltimoRegistro = numRegistro == listDTOS.Count;

                Console.WriteLine($"Inicio Nro:{numRegistro}");
                var command = new RegistrarProspectoRedesSocialesCommand(
                item.Anuncio,
                item.Plataforma,
                item.Nombre,
                item.Apellido,
                item.Telefono,
                item.Email,
                true,
                esUltimoRegistro,
                numRegistro,
                false
                );
                var prospecto = await _mediator.Send(command);
                //if (contar == 15)
                //{
                //    contar = 0;
                //    await Task.Delay(TimeSpan.FromSeconds(120));
                //}
                contar++;
                Console.WriteLine($"Fin Nro:{numRegistro}");
            }
            return Ok($"Se cargaron correctamente {listDTOS.Count}");
        }

        [HttpPost("IdDeal/Masivo", Name = "RegistrarPorIdDeal")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> RegistrarPorIdDeal([FromBody] List<int> listaIds)
        {
            var contar = 0;
            var numRegistro = 0;
            foreach (var id in listaIds)
            {
                numRegistro++;
                Console.WriteLine($"Inicio Nro:{numRegistro}");
                var command =new  RegistrarPorIdDealCommand(id);
                var prospecto = await _mediator.Send(command);

                if (contar == 15)
                {
                    contar = 0;
                    await Task.Delay(TimeSpan.FromSeconds(120));
                }
                contar++;

                Console.WriteLine($"Fin Nro:{numRegistro}");
            }
            return Ok($"Se cargaron correctamente {listaIds.Count}");
        }
        //[HttpPost("IdDeal/Masivo/Eliminar", Name = "EliminarDealMasico")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<string>> EliminarDealMasico([FromBody] List<int> listaIds)
        //{
        //    var numRegistro = 0;
        //    foreach (var id in listaIds)
        //    {
        //        numRegistro++;
        //        Console.WriteLine($"Inicio Nro:{numRegistro}");
        //        var command = new EliminarDealCommand(id);
        //        var prospecto = await _mediator.Send(command);

        //        Console.WriteLine($"Fin Nro:{numRegistro}");
        //    }
        //    return Ok($"Se eliminaron correctamente {listaIds.Count}");
        //}
        [HttpPost("Bitrix/RegistrarProspectosExistente/Masivo", Name = "RegistrarProspectoExistenteEnBitrix")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> RegistrarProspectoExistenteEnBitrixMasivo([FromBody] List<int> prospectosIdList)
        {
            var contar = 0;
            var numRegistro = 0;
            foreach (var id in prospectosIdList)
            {
                numRegistro++;
                Console.WriteLine($"Inicio Nro:{numRegistro}");
                var command = new RegistrarProspectoExistenteEnBitrixCommand(id);
                var prospecto = await _mediator.Send(command);

                if (contar == 15)
                {
                    contar = 0;
                    await Task.Delay(TimeSpan.FromSeconds(15));
                }
                contar++;

                Console.WriteLine($"Fin Nro:{numRegistro}");
            }
            return Ok($"Se cargaron correctamente {prospectosIdList.Count}");
        }

        [HttpPost("Bitrix/RegistrarProspectosExistente")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarProspectoExistenteEnBitrix([FromBody] RegistrarProspectoExistenteEnBitrixCommand command)
        {
                var prospecto = await _mediator.Send(command);

            return prospecto;
        }
        [HttpPost("IdDeal/Post/PathParam/{id}", Name = "RegistrarPorIdDealPostPathParam")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegistrarPorIdDealPostPathParam(int id)
        {
            var command = new RegistrarPorIdDealCommand(id);
            var prospecto = await _mediator.Send(command);
            return Ok(prospecto);
        }
        //[HttpGet("IdDeal/Prueba/{id}", Name = "PruebaRegistroBitrix")]
        //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<int>> PruebaRegistroBitrix(int id)
        //{
        //    return Ok(await _mediator.Send(new PruebaBitrixQuery(id)));
        //}
        //[HttpPost("ObtenerDiscrepaciaOrigenes")]
        //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<int>> ObtenerDiscrepaciaOrigenes()
        //{
        //    return Ok(await _mediator.Send(new ObtenerDiscrepanciaDeOrigenesQuery()));
        //}
    }
}