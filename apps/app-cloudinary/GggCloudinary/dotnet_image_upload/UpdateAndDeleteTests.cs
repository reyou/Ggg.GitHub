using System.Collections.Generic;
using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_upload
{
    [TestClass]
    public class UpdateAndDeleteTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }


        /// <summary>
        /// generate transformed versions
        /// </summary>
        [TestMethod]
        public void GenerateTransformedVersions()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ExplicitParams exp = new ExplicitParams("EagerTransformations")
            {
                Type = "upload",
                EagerTransforms =
                    new List<Transformation>
                    {
                        new Transformation().Width(150).Height(230).Crop("fill")
                    }
            };
            ExplicitResult expResult = cloudinary.Explicit(exp);
            TestUtilities.LogAndWrite(expResult, "GenerateTransformedVersions.txt");

        }


        /// <summary>
        /// Forcing cache invalidation
        /// </summary>
        [TestMethod]
        public void ForcingCacheInvalidation()
        {
            UploadZombieImage();
            Debugger.Break();
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            DelResParams delParams = new DelResParams
            {
                PublicIds = new List<string> { "zombie" },
                Invalidate = true
            };
            DelResResult delResult = cloudinary.DeleteResources(delParams);
            TestUtilities.LogAndWrite(delResult, "ForcingCacheInvalidation.txt");
        }

        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void DeleteResources()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            UploadZombieImage();
            Debugger.Break();
            DelResResult delResResult = cloudinary.DeleteResources("zombie");
            TestUtilities.LogAndWrite(delResResult, "DeleteResources.txt");
        }

        private void UploadZombieImage()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription("https://c.pxhere.com/photos/90/4b/carnival_venice_eyes_mask_woman-947285.jpg!d"),
                PublicId = "zombie",
                Invalidate = true
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "UploadZombieImage.txt");
        }
    }
}
