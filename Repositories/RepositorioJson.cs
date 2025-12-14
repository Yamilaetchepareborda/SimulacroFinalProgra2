using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Repositories
{
    public class RepositorioJson<T> : IRepositorio<T>
    {
        private readonly string _ruta;
        private static readonly JsonSerializerOptions _opts = new JsonSerializerOptions { WriteIndented = true };

        public RepositorioJson(string ruta)
        {
            _ruta = ruta;
        }

        public IEnumerable<T> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return Enumerable.Empty<T>();

            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<T>>(json, _opts)?? new List<T>();
        }

        public void Guardar(T entidad)
        {
            var lista = ObtenerTodos().ToList();
            lista.Add(entidad);
            var json = JsonSerializer.Serialize(lista, _opts);
            File.WriteAllText(_ruta, json);
        }
    }
}

