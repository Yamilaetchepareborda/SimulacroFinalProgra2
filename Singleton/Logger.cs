using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Logger
    {
        private static Logger _instance;             // 1) instancia única
        private readonly List<string> _mensajes;     // 2) historial

        private Logger()                             // 3) constructor privado
        {
            _mensajes = new List<string>();
        }

        public static Logger Instance                // 4) acceso global
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }

        public void Info(string mensaje)             // 5) log con prefijo
        {
            string linea = $"[INFO] {mensaje}";
            _mensajes.Add(linea);
            Console.WriteLine(linea);
        }

        public void Dump()                           // 6) muestra historial
        {
            Console.WriteLine("---- DUMP LOG ----");
            foreach (var m in _mensajes)
                Console.WriteLine(m);
        }
    }
}
