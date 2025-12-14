using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
        public class Reserva
        {
            public string Viajero { get; set; } = string.Empty;
            public DateTime FechaInicio { get; set; }
            public List<Destino> Destinos { get; set; } = new List<Destino>();

            public string MedioTransporte { get; set; } = string.Empty;
            public decimal Subtotal { get; set; }
            public decimal Total { get; set; }

            // Campo “extra” para mostrar de dónde salen las reglas
            public string Notas { get; set; } = string.Empty;
        }
    

}
