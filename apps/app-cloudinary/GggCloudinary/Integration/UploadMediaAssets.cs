using System;
using System.Collections.Generic;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.Integration
{
    /// <summary>
    /// https://cloudinary.com/documentation/dotnet_integration#upload_media_assets
    /// </summary>
    [TestClass]
    public class UploadMediaAssets
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }


        /// <summary>
        /// As you can see in the following example, with a single call you can define a custom 
        /// Public ID, apply an incoming transformation before storing the resource in the cloud, 
        /// generate derived resources eagerly and assign tags to uploaded resources
        /// </summary>
        [TestMethod]
        public void MultipleActions()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            Transformation transformation1 = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face").
                Radius(20).Effect("sepia");
            Transformation transformation2 = new Transformation().Width(100).Height(150).Crop("fit").FetchFormat("png");
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\cat.jpg"),
                PublicId = "sample_id",
                Transformation = new Transformation().Crop("limit").Width(40).Height(40),
                EagerTransforms = new List<Transformation>
                {
                    transformation1,
                    transformation2
                },
                Tags = "special, for_homepage"
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "MultipleActions.txt");

        }

        /// <summary>
        /// Alternatively, you can a specify a local path, a public HTTP URL, an S3 URL or an actual media file's data
        /// </summary>
        [TestMethod]
        public void RemoteUrl()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(@"https://images.pexels.com/photos/906982/pexels-photo-906982.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "RemoteUrl.txt");
        }

        /// <summary>
        /// The following snippet uploads a local file to Cloudinary:
        /// </summary>
        [TestMethod]
        public void UploadLocalFile()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\mypicture.jpg")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "UploadLocalFile.txt");

        }
    }
}
