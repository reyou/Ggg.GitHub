using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_upload
{
    /// <summary>
    /// Cloudinary allows generating dynamic text overlays with your custom text. 
    /// First you need to create a text style - font family, size, color, etc. 
    /// The name of the text style is its public ID and it behaves like an 
    /// image of the text type.
    /// </summary>
    [TestClass]
    public class TextCreationTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }


        /// <summary>
        /// The following image tag adds a text overlay using the created dark_name text style
        /// http://res.cloudinary.com/foodception/image/upload/g_south_east,l_text:dark_name:Hello+World,x_5,y_5/v1528853705/AddATextOverlay.jpg
        /// http://res.cloudinary.com/foodception/image/upload/g_south_east,l_text:dark_name:Hello+World,x_2,y_5/v1528853705/AddATextOverlay.jpg
        /// </summary>
        [TestMethod]
        public void AddATextOverlay()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TestUtilities.UploadImageWithName("AddATextOverlay", "https://c.pxhere.com/images/98/20/4e64c736bb23441baf5bcde4cf87-1432045.jpg!d");
            string buildImageTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation()
                        .X(5)
                        .Y(5)
                        .Gravity("south_east")
                        .Overlay("text:dark_name:Hello+World"))
                .BuildImageTag("AddATextOverlay");
            TestUtilities.LogAndWrite(buildImageTag, "AddATextOverlay.txt");

        }

        /// <summary>
        /// The following command creates a text style named dark_name of 
        /// a certain font, color and style:
        /// </summary>
        [TestMethod]
        public void CreateATextStyle()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TestUtilities.UploadImageWithName("dark_name", "https://c.pxhere.com/images/2d/29/6b36334fd2508d2b8f40600b8fa4-1418539.jpg!d");
            TextParams textParams = new TextParams("Sample Name")
            {
                PublicId = "dark_name",
                FontFamily = "Arial",
                FontSize = 50,
                FontColor = "black",
                Opacity = "90"
            };
            TextResult textResult = cloudinary.Text(textParams);
            TestUtilities.LogAndWrite(textResult, "CreateATextStyle.txt");
        }
    }
}
