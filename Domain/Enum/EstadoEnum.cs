using Domain.Entities;

namespace Domain.Enum
{
    public enum EstadoEnum
    {
        NoContactado=1,
        NoContactadoReasignado=2,
        Contactado=3,
        ConProforma=4,
        CitaProgramada=5,
        CitaRealizada=6,
        CitaNoRealizada=7,
        PagoPendiente=8,
        ConPago=9,
        AprobadoConObservacion=10,
        Aprobado=11,
        Inscrito=12,
    }
}
