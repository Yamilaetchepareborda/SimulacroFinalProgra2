using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Observers;
using Repositories;
using Strategy;
using Builder;
using Singleton;

namespace Facade
{
    public class ReservaFacade
    {
        private readonly IReservaBuilder _builder;
        private readonly ReservaService _service;
        private readonly IRepositorio<Reserva> _repo;

        private readonly List<Destino> _destinosSeleccionados = new List<Destino>();
        private ITransporteStrategy _transporteActual;

        public ReservaFacade(IReservaBuilder builder,ReservaService service,IRepositorio<Reserva> repo)
        {
            _builder = builder;
            _service = service;
            _repo = repo;
        }

        public void AgregarDestino(string nombre, decimal precio, int dias)
        {
            var destino = new Destino
            {
                Nombre = nombre,
                PrecioBase = precio,
                DuracionDias = dias
            };
            _destinosSeleccionados.Add(destino);
            Logger.Instance.Info($"Destino agregado: {nombre}");
        }

        public void SeleccionarTransporte(ITransporteStrategy transporte)
        {
            _transporteActual = transporte;
            Logger.Instance.Info($"Transporte seleccionado: {transporte.Nombre}");
        }

        public void ConfirmarReserva(string viajero, DateTime fechaInicio)
        {
            Logger.Instance.Info("Iniciando confirmación de reserva"); //instancio singleton

            if (_transporteActual == null)
                throw new InvalidOperationException("Debe seleccionar un transporte antes de confirmar.");

            _builder.Reset();
            _builder.ConViajero(viajero);
            _builder.ConFecha(fechaInicio);

            foreach (var d in _destinosSeleccionados)
            {
                _builder.ConDestino(d);
            }

            _builder.ConTransporte(_transporteActual);

            var reserva = _builder.Build();

            _service.ConfirmarReserva(reserva); // dispara observers
            _repo.Guardar(reserva);      // persiste

            _destinosSeleccionados.Clear();
            Logger.Instance.Info("Reserva confirmada y guardada");
        }

        public IEnumerable<Reserva> ListarReservas()
        {
            return _repo.ObtenerTodos();
        }
    }
}

