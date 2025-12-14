using Models;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class TransporteAuto : ITransporteStrategy
    {
        public string Nombre => "Auto";

        public decimal CalcularCosto(IEnumerable<Destino> destinos, decimal subtotal)
        {
            Logger.Instance.Info("Calculando costo de transporte AUTO");
            var cfg = ConfigManager.Instance;
            // Costo fijo + subtotal, auto tiene costo fijo de 3000, no un porcentaje
            return subtotal + cfg.CostoFijoAuto; 
        }
    }
}
