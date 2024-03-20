using Ejercicio_CRUD_Mvvm.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_CRUD_Mvvm.Services
{
    public class ProductoService
    {
        private readonly SQLiteConnection _dbConnection;

        public ProductoService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Productos.db3");
            _dbConnection = new SQLiteConnection(dbPath);
            _dbConnection.CreateTable<Productos>();
        }

        public List<Productos> GetAll()
        {
            return _dbConnection.Table<Productos>().ToList();
        }

        public int Insert(Productos productos)
        {
            return _dbConnection.Insert(productos);
        }

        public int Update(Productos productos)
        {
            return _dbConnection.Update(productos);
        }

        public int Delete(Productos productos)
        {
            return _dbConnection.Delete(productos);
        }
    }
}
