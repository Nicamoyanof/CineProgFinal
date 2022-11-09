using DllCineApi.Dominios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Fachada.Interfaz
{
    public interface IClientesApi
    {
        public List<Clientes> ObtenerClientes();
        public DataTable CargarSociosPorProvincia();
        public List<Ciudades> CargarCiudades();
        bool Crear(Clientes cliente);
        bool Actualizar(Clientes cliente, int id);
        bool Borrar(int nro);
    }
}
