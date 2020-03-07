using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
