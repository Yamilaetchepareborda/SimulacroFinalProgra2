using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Logger
    {
        private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());    // 1) instancia Lazy
        private readonly List<string> _mensajes;     // 2) lista para historial

        private Logger()                             // 3) constructor privado, declaramos mensajes
        {
            _mensajes = new List<string>();
        }

        public static Logger Instance => _instance.Value; // 4) acceso global, publico.
       
        public void Info(string mensaje)             // 5) log con prefijo
        {
            string linea = $"[INFO] {mensaje}";
            _mensajes.Add(linea);

        }

        public IEnumerable<string> Dump() // 6) Devuelve el historial
        {
            return _mensajes.ToList();
        }

    }
}
