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
        /// https://cloudinary.com/documentation/dotnet_image_upload#rename_images
        /// You can rename images uploaded to Cloudinary. Renaming means changing 
        /// the public ID of already uploaded images. The following methods allows 
        /// renaming a public ID:
        /// </summary>
        [TestMethod]
        public void RenameImage()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TestUtilities.UploadImageWithName("renameSample", "https://c.pxhere.com/images/2c/4a/a103357d600dd68d0b28ca6b5135-1432179.jpg!d");
            /*By default, Cloudinary prevents renaming to an already taken public ID.
             You can set the overwrite parameter to true to delete the image that has 
             the target public ID and replace it with the image being renamed:*/
            RenameResult renameResult = cloudinary.Rename("renameSample", "bee", true);
            TestUtilities.LogAndWrite(renameResult, "RenameResult.txt");
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
            TestUtilities.UploadImageWithName();
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
            TestUtilities.UploadImageWithName();
            Debugger.Break();
            DelResResult delResResult = cloudinary.DeleteResources("zombie");
            TestUtilities.LogAndWrite(delResResult, "DeleteResources.txt");
        }


    }
}
