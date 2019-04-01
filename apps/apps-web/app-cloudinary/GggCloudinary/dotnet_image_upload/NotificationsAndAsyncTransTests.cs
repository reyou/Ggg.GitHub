using System.Collections.Generic;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_upload
{
    /// <summary>
    /// By default, Cloudinary's upload API works synchronously. Uploaded images 
    /// processed and eager transformations are generated synchronously during the 
    /// upload API call.
    /// https://cloudinary.com/documentation/dotnet_image_upload#notifications_and_async_transformations
    /// https://requestbin.fullcontact.com/
    /// </summary>
    [TestClass]
    public class NotificationsAndAsyncTransTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// Cloudinary also supports webhooks. With webhooks enabled, you can 
        /// get a notification to your server when an upload is completed.
        /// </summary>
        [TestMethod]
        public void WebHooks()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\umbrella.jpg"),
                PublicId = "umbrella",
                NotificationUrl = "https://requestbin.fullcontact.com/1knfony1"
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "WebHooks.txt");
        }

        /// <summary>
        /// template
        /// https://requestbin.fullcontact.com/tqgxvttq?inspect
        /// </summary>
        [TestMethod]
        public void UploadEagerAsyncTrue()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            cloudinary.DeleteResources("vosvos");
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\vosvos.jpg"),
                PublicId = "vosvos",
                EagerTransforms = new List<Transformation>() {
                    new Transformation().Width(150).Height(100).Crop("thumb").Gravity("face")
                },
                EagerAsync = true,
                EagerNotificationUrl = "http://requestbin.fullcontact.com/tqgxvttq"
            };
            ImageUploadResult imageUploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(imageUploadResult, "UploadEagerAsyncTrue.txt");
        }
    }
}
