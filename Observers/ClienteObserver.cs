using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Observers
{
    public class ClienteObserver : IReservaObserver
    {
        public void OnReservaConfirmada(Reserva reserva)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Cliente || Gracias {reserva.Viajero}, su reserva fue confirmada.");
            Console.WriteLine($"Total: ${reserva.Total}");
            Console.ResetColor();
        }
    }
}

