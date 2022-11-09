
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Datos.Implementacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DllCineApi.Fachada.Interfaz;

public class ClientesApiImp : IClientesApi
{
    private IDaoClientes daoClientes;

    public ClientesApiImp()
    {
        daoClientes = new ClientesDao();
    }
    public List<Clientes> ObtenerClientes()
    {
        return daoClientes.ObtenerClientes();
    }
    public bool Crear(Clientes cliente)
    {
        return daoClientes.Crear(cliente);
    }

    public bool Actualizar(Clientes cliente, int id)
    {
        return daoClientes.Actualizar(cliente, id);
    }

    public bool Borrar(int nro)
    {
        return daoClientes.Borrar(nro);
    }

    public DataTable CargarSociosPorProvincia()
    {
        return daoClientes.CargarSociosPorProvincia();
    }

    public List<Ciudades> CargarCiudades()
    {
        return daoClientes.ObtenerCiudades();
    }
}
