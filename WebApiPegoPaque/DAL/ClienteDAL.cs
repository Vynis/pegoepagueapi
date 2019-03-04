using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.DAL
{
    public class ClienteDAL
    {
        private DataContext db = new DataContext();

        public void RegistrarCliente(Cliente cliente)
        {
            db.Clientes.Add(cliente);
            db.SaveChanges();
        }

        public async Task<List<Cliente>> Listagem()
        {
            return await Task.Run(() =>
           {
               return db.Clientes.ToList();
           });

        }

        public Cliente RetornaCliente(int id)
        {
            return db.Clientes.Find(id);
        }

        public bool AtualizarCliente(int id, Cliente cliente)
        {
            if (db.Clientes.Count(c => c.Id == id) == 0)
                return false;

            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();

            return true;

        }

        public bool DeletarCliente(int id)
        {
            if (id <= 0)
                return false;

            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
                return false;

            db.Clientes.Remove(cliente);
            db.SaveChanges();

            return true;

        }
    }
}
