using Models;
using Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public interface IReservaBuilder
    {
        void Reset();
        void ConViajero(string nombre);
        void ConFecha(DateTime fecha);
        void ConDestino(Destino destino);
        void ConTransporte(ITransporteStrategy transporte);
        Reserva Build();
    }
}
