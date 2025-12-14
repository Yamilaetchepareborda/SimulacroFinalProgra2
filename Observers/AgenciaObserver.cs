using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Observers
{
    public class AgenciaObserver : IReservaObserver
    {
        public void OnReservaConfirmada(Reserva reserva)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Agencia || Nueva reserva registrada.");
            Console.WriteLine($"Viajero: {reserva.Viajero} | Transporte: {reserva.MedioTransporte}");
            Console.ResetColor();
        }
    }
}

