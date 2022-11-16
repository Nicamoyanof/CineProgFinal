using DllCineApi.Dominios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Datos.Interfaz
{
    public interface IDaoClientes
    {
        List<Clientes> ObtenerClientes();
        DataTable CargarSociosPorProvincia();
        Clientes ObtenerClienteById(int id);
        List<Ciudades> ObtenerCiudades();
        bool Crear(Clientes cliente);
        bool Actualizar(Clientes cliente, int id);
        bool Borrar(int nro);
    }
}
