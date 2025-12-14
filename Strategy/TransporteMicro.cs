using Models;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class TransporteMicro : ITransporteStrategy
    {
        public string Nombre => "Micro";

        public decimal CalcularCosto(IEnumerable<Destino> destinos, decimal subtotal)
        {
            var cfg = ConfigManager.Instance;
            return subtotal * (1 + cfg.RecargoMicro);
        }
    }
}
