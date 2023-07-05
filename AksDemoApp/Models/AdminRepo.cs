using AksDemoApp.Data;

namespace AksDemoApp.Models
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AppDbContext context;

        public AdminRepo(AppDbContext context)
        {
            this.context = context;
        }

        public Admin GetAdmin(Admin admin)
        {
           var user= context.Admins.FirstOrDefault(x => x.Name == admin.Name && x.Password == admin.Password);
            if(user!= null)
            {
                return user;
            }
            return null;
        }

        public Admin GetAdminPass(string pass)
        {
            return context.Admins.FirstOrDefault(x => x.Password == pass);
        }

        public Admin Update(Admin admin)
        {
           var pass= context.Admins.Attach(admin);
            pass.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return admin;
        }
    }
}
