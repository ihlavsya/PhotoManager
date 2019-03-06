using System.Collections.Generic;

namespace PhotoManager.DAL.Entities
{
    public class User
    {
        public User()
        {
           Albums=new List<Album>();
        }
        public int UserId { get; set; }
        public string Username { set; get; }
        public string Password { set; get; }
        public virtual ICollection<Album> Albums { set; get; }
    }
}
