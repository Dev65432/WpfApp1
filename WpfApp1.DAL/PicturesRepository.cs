using System.Linq;
using WpfApp1.DAL.Context;
using WpfApp1.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.DAL
{
    class PicturesRepository : DbRepository<Picture>
    {
        public override IQueryable<Picture> Items => base.Items;

        public PicturesRepository(cntxDBWpfApp1 db) : base(db) { }
    }
}