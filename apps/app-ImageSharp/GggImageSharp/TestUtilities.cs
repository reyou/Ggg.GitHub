using System.IO;

namespace GggImageSharp.GettingStarted
{
    public class TestUtilities
    {
        public static string GetImagesFolder()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fileInfo = new FileInfo(location);
            if (fileInfo.Directory != null)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo.Directory.FullName);
                if (directoryInfo.Parent?.Parent?.Parent != null)
                {
                    string parentFullName = directoryInfo.Parent.Parent.Parent.FullName;
                    string imagesFolder = parentFullName + "\\images";
                    return imagesFolder;
                }
            }

            throw new FileNotFoundException("images path could not be found.");
        }
    }
}