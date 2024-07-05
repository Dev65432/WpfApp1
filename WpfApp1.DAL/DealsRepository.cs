using System.Linq;
using WpfApp1.DAL.Context;
using WpfApp1.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.DAL
{
    class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items;

        public Deal GetById(int id)
        {
            return base.Get(id);
        }
        


        public DealsRepository(cntxDBWpfApp1 db) : base(db) { }
    }
}