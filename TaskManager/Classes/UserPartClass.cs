using System.IO;
using Avalonia.Media.Imaging;

namespace TaskManager.Model;

public partial class User
{
    public Bitmap GetPhoto
    {
        get
        {
            if (Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(Photo))
                {
                    var bitmap = new Bitmap(stream);
                    return bitmap;
                }
            }
            else
            {
                var photo = File.ReadAllBytes("/home/astra/Desktop/Projects/TaskManager/TaskManager/Resources/User.jpg");
                using (MemoryStream stream = new MemoryStream(photo))
                {
                    var bitmap = new Bitmap(stream);
                    return bitmap;
                }
            }
        }
    }
}