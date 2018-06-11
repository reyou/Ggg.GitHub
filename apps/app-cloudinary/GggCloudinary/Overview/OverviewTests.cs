using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.Overview
{
    [TestClass]
    public class OverviewTests
    {
        private Cloudinary Cloudinary { get; set; }
        public OverviewTests()
        {
            Cloudinary = TestUtilities.GetCloudinary();
        }

        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_integration#quick_example_file_upload
        /// </summary>
        [TestMethod]
        public void FileUpload()
        {
            VideoUploadParams uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(@"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\videos\dog.mp4"),
                PublicId = "my_folder/my_sub_folder/my_dog",
                Overwrite = true,
                NotificationUrl = "http://mysite/my_notification_endpoint"
            };
            VideoUploadResult uploadResult = Cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "FileUpload.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_integration
        /// </summary>
        [TestMethod]
        public void ImageTransformation()
        {

            Transformation transformation = new Transformation().Width(150).Height(150).Gravity("face")
                .Radius(20).Effect("sepia")
                .Chain().Overlay("cloudinary_icon").Gravity("south_east").X(5).Y(5).Opacity(60)
                .Effect("brightness:200")
                .Chain().Angle(10);
            string buildImageTag = Cloudinary.Api.UrlImgUp
                .Transform(transformation)
                .Secure()
                .BuildImageTag("front_face.png");
            TestUtilities.LogAndWrite(buildImageTag, "ImageTransformation.txt");
        }
    }
}
