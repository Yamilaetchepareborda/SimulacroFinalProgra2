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
            Logger.Instance.Info("Calculando costo de transporte, en Avion");
            var cfg = ConfigManager.Instance;
            // recargo sobre el subtotal
            return subtotal * (1 + cfg.RecargoAvion);
        }
    }
}
