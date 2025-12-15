using Builder;
using Models;
using Singleton;
using Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class ReservaBuilder : IReservaBuilder
    {
        private Reserva _reserva = new Reserva();
        private ITransporteStrategy _transporte ;

        public void Reset()
        {
            _reserva = new Reserva();
            _transporte = null;
        }

        public void ConViajero(string nombre)
        {
            _reserva.Viajero = nombre;
        }

        public void ConFecha(DateTime fecha)
        {
            _reserva.FechaInicio = fecha;
        }

        public void ConDestino(Destino destino)
        {
            _reserva.Destinos.Add(destino); //agrego destino a la lista de destinos en reserva.
            _reserva.Subtotal += destino.PrecioBase; // el subtotal de la reserva va a sumar al precio base del destino.
        }

        public void ConTransporte(ITransporteStrategy transporte)
        {
            _transporte = transporte;
            _reserva.MedioTransporte = transporte.Nombre; //guaro en MedioTransporte de reserva, el nombre del transporte.
        }

        public Reserva Build() // creo la reserva completa.
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(_reserva.Viajero))
                throw new InvalidOperationException("El nombre del viajero es obligatorio.");

            if (_reserva.Destinos.Count == 0)
                throw new InvalidOperationException("Debe agregar al menos un destino.");

            if (_reserva.FechaInicio.Date <= DateTime.Today)
                throw new InvalidOperationException("La fecha de inicio debe ser futura.");

            if (_transporte == null)
                throw new InvalidOperationException("Debe seleccionar un medio de transporte.");

            // CÁLCULO DEL TOTAL usando Strategy
            _reserva.Total = _transporte.CalcularCosto(_reserva.Destinos, _reserva.Subtotal);

            // Regla extra usando Singleton: descuento por muchos días
            int totalDias = _reserva.Destinos.Sum(d => d.DuracionDias);
            if (totalDias >= 7)
            {
                var cfg = ConfigManager.Instance; // llamo a la instancia de config singleton
                var descuento = _reserva.Total * cfg.DescuentoPorMuchosDias;
                _reserva.Total -= descuento;
                _reserva.Notas = $"Se aplicó descuento del {cfg.DescuentoPorMuchosDias:P0} por viaje largo.";
            }

            return _reserva;
        }
    }
}
