using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;

namespace GggImageSharp.GettingStarted
{
    /// <summary>
    /// https://sixlabors.github.io/docs/articles/ImageSharp/GettingStarted.html
    /// </summary>
    [TestClass]
    public class GettingStartedSamples
    {
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// Metadata-only decoding
        /// https://sixlabors.github.io/docs/articles/ImageSharp/ImageFormats.html
        /// </summary>
        [TestMethod]
        public void MetaDecoding()
        {
            WebClient client = new WebClient();
            string address = "https://raw.githubusercontent.com/reyou/Ggg.Csharp/master/apps/app-ImageSharp/GggImageSharp/images/metaData.jpg";
            byte[] bytes = client.DownloadData(address);
            Stream inputStream = new MemoryStream(bytes);
            IImageInfo imageInfo = Image.Identify(inputStream);
            Trace.WriteLine($"{imageInfo.Width}x{imageInfo.Height} | BPP: {imageInfo.PixelType.BitsPerPixel}");
        }

        /// <summary>
        /// How do I create a blank image for drawing on?
        /// </summary>
        [TestMethod]
        public void CreateBlankImage()
        {
            int width = 640;
            int height = 480;
            // creates a new image with all the pixels set as transparent 
            using (Image<Rgba32> image = new Image<Rgba32>(width, height))
            {
                string location = TestUtilities.GetImagesFolder();
                string filePath = location + "\\createBlankImage.jpg";
                image.Save(filePath);
                // do your drawing in here...
            } // dispose - releasing memory into a memory pool ready for the next image you wish to process

        }
        /// <summary>
        /// Scaling a jpeg by half and save it again as a jpg
        /// </summary>
        [TestMethod]
        public void Scale()
        {

            string location = TestUtilities.GetImagesFolder();
            //open the file and detect the file type and decode it
            using (Image<Rgba32> image = Image.Load(location + "\\foo.jpg"))
            {
                // image is now in a file format agnostic structure in memory as a series of Rgba32 pixels
                // resize the image in place and return it for chaining
                image.Mutate(ctx => ctx.Resize(image.Width / 2, image.Height / 2));
                // based on the file extension pick an encoder then encode and write the data to disk
                image.Save(location + "\\bar.jpg");
            }
            // dispose - releasing memory into a memory pool ready for the next image you wish to process
        }
    }
}
