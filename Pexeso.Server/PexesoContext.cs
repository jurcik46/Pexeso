using System.Data.Entity;
using System.Linq;
using Pexeso.Server.Models;

namespace Pexeso.Server
{
    public partial class PexesoContext : DbContext
    {

        public DbSet<Users> Users { get; set; }

        public PexesoContext()
            : base("name=Pexeso")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PexesoContext>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public Users GetUserByName(string name)
        {
            return Users.First(u => u.UserName == name);
        }
        public bool CheckIfExistThisUser(string userName)
        {
            return Users.Any(u => u.UserName == userName);
        }

        public void SaveUser(Users user)
        {
            Users.Add(user);
            SaveChanges();
        }


    }
}
