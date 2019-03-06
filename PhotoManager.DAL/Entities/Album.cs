using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.DAL.Entities
{
    public class Album
    {
        public Album()
        {
            Photos = new List<Photo>();
        }
        public int AlbumId { set; get; }
        public virtual ICollection<Photo> Photos { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Genres Genre { set; get; }
        public virtual int GenresId
        {
            get
            {
                return (int)this.Genre;
            }
            set
            {
                Genre = (Genres)value;
            }
        }
        public int UserId { set; get; }
        public DateTime UpdateDateTime { set; get; }
        public string Username { set; get; }
    }
}
