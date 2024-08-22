using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLA
{
    public class RepositoryFactory
    {
        public static IRepository CreateRepositiry()
        {
            return new EFRepository(new Datos.ApplicationDbContext());
        }
    }
}
