
using WpfApp1.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.DAL.Context
{
    public class cntxDBWpfApp1 : DbContext
    {
        public DbSet<Deal> Deals { get; set; }        
        
        public DbSet<Picture> Pictures { get; set; }        

        public cntxDBWpfApp1(DbContextOptions<cntxDBWpfApp1> options) : base(options)
        {
            
        }
    }
}
