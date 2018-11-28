using NexMed.Data;
using NexMed.Entities;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NexMed.Services
{
    public class UserService
    {
        private NexMedContext db;

        public UserService(NexMedContext context)
        {
            db = context;
        }

        public bool IsUserSet(string email)
        {
            return db.Users.Where(x => x.Email == email).Any();
        }

        public User GetUser(string email, string password)
        {
            return db.Users.Where(x => x.Email == email && x.Password == getHash(password)).FirstOrDefault();
        }

        public User SetUser(int cityId, string email, string name, string password, int role)
        {
            var city = db.Cities.Where(x => x.Id == cityId).First();
            var newUser = new User()
            {
                City = city,
                Email = email,
                Name = name,
                Password = getHash(password),
                Role = role
            };

            var createdUser = db.Users.Add(newUser);
            db.SaveChanges();
            return createdUser;
        }

        private string getHash(string value)
        {
            byte[] hash = Encoding.UTF8.GetBytes(value);
            MD5 md5 = new MD5CryptoServiceProvider();
            return Encoding.UTF8.GetString(md5.ComputeHash(hash));
        }
    }
}
