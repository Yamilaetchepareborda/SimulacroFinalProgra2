using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace Observers
{
    public class ReservaService // servicio que controla los observers 
    {
        private readonly List<IReservaObserver> _observers = new List<IReservaObserver>();

        public void Suscribir(IReservaObserver observer)
        {
            _observers.Add(observer);
        }

        public void ConfirmarReserva(Reserva reserva)
        {
            foreach (var observer in _observers)
            {
                observer.OnReservaConfirmada(reserva);
            }
        }
    }
}

