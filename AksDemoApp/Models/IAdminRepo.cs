namespace AksDemoApp.Models
{
    public interface IAdminRepo
    {
        public Admin GetAdmin(Admin admin);
        public Admin GetAdminPass(string pass);
        public Admin Update(Admin admin);
    }
}
