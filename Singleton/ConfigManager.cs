using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public sealed class ConfigManager
    {
        // Instancia única (Lazy, hilo-seguro)
        private static readonly Lazy<ConfigManager> _instancia = new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance => _instancia.Value;

        // Constructor privado para que nadie pueda hacer new ConfigManager()
        private ConfigManager() { }

        // Parámetros globales de la app
        public decimal RecargoAvion { get; set; } = 0.15m;   // 15%
        public decimal RecargoMicro { get; set; } = 0.05m;   // 5%
        public decimal CostoFijoAuto { get; set; } = 30000m; // 30k
        public decimal DescuentoPorMuchosDias { get; set; } = 0.10m; // 10% si suma días >= 7
    }
}

