using System.IO;
using System.Web.Configuration;
using System.Web;

namespace Helpers
{
    public static class PathResolver
    {
        public static string GetPathForSavePhoto(string photoName, ImageSize size)
        {
            int i=photoName.IndexOf('.');
            string name = photoName.Remove(i);
            string trimmedRelativePath ="~"+WebConfigurationManager.AppSettings["PhotoStorageRelativePath"].Remove(0,5);
            return Path.Combine(HttpContext.Current.Server.MapPath(trimmedRelativePath),name  + size.ToString() + ".jpg");
        }
        public static string GetPathForViewPhoto(string photoName, ImageSize size)
        {
            int i = photoName.IndexOf('.');
            string name = photoName.Remove(i);
            return Path.Combine(WebConfigurationManager.AppSettings["PhotoStorageRelativePath"], name + size.ToString() + ".jpg");
        }
    }
}
