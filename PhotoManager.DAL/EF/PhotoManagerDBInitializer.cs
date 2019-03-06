using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.DAL.EF
{
    class PhotoManagerDBInitializer : CreateDatabaseIfNotExists<PhotoManagerContext>
    {
        protected override void Seed(PhotoManagerContext context)
        {
            //Add admin user
        }
    }
}
