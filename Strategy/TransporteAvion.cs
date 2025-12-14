using Models;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class TransporteAvion : ITransporteStrategy
    {
        public string Nombre => "Avión";

        public decimal CalcularCosto(IEnumerable<Destino> destinos, decimal subtotal)
        {
            var cfg = ConfigManager.Instance;
            // recargo sobre el subtotal
            return subtotal * (1 + cfg.RecargoAvion);
        }
    }
}
