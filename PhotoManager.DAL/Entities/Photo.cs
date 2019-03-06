using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.DAL.Entities
{
    public class Photo
    {
        public Photo()
        {
            Albums=new List<Album>();
        }

        public int PhotoId { set; get; }
        public virtual ICollection<Album> Albums { set; get; }
        public DateTime UpdateDateTime { set; get; }
        public string OriginalName { set; get; }
        public string Description { set; get; }
        public string PhotoTakingPlace { set; get; }
        public string CameraModel { set; get; }
        public int LensFocalLength { set; get; }//mm
        public int ISO { set; get; }
        public float Diaphragm { set; get; }//diametr of the circle
        public bool Flash { set; get; }
        public int ShutterSpeedNumerator { set; get; }// Shutter speed is measured in fractions of a second
        public int ShutterSpeedDenominator { set; get; }// Shutter speed is measured in fractions of a second
        public string Name { set; get; }
    }
}
