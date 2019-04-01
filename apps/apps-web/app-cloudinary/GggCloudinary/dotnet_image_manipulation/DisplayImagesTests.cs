using CloudinaryDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_manipulation
{
    [TestClass]
    public class DisplayImagesTests
    {

        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#multiple_cdn_sub_domains
        /// </summary>
        [TestMethod]
        public void MultipleCdnSubdomains()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            cloudinary.Api.CSubDomain = true;
            string tag = cloudinary.Api.UrlImgUp.CSubDomain(true)
                .BuildImageTag("sample.jpg");
            TestUtilities.LogAndWrite(tag, "MultipleCdnSubdomains.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#secure_https_urls
        /// You can force BuildImageTag to always use HTTPS URLs by setting the Secure 
        /// parameter to true, either globally or locally in each call to BuildImageTag 
        /// or to BuildUrl
        /// </summary>
        [TestMethod]
        public void SecureHttpsUrLs()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string tag = cloudinary.Api.UrlImgUp.Secure()
                .Transform(new Transformation().Width(100).Height(150).Crop("fill"))
                .BuildImageTag("sample.jpg");
            string url = cloudinary.Api.UrlImgUp.Secure()
                .Transform(new Transformation().Width(100).Height(150).Crop("fill"))
                .BuildUrl("sample.jpg");
            TestUtilities.LogAndWrite(tag, "SecureHttpsUrLs.txt");
            TestUtilities.LogAndWrite(url, "SecureHttpsUrLs2.txt");
        }


        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#direct_url_building
        /// The BuildImageTag method generates an HTML image tag. In certain occasions, 
        /// you might want to generate a transformation URL directly, without the containing 
        /// image tag. You can do that by using the BuildUrl method of the Url class.
        /// </summary>
        [TestMethod]
        public void BuildUrl()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string url = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("fill"))
                .BuildUrl("sample.jpg");
            string url2 = cloudinary.Api.UrlImgUp.ResourceType("raw")
                .BuildUrl("sample_spreadsheet.xls");
            TestUtilities.LogAndWrite(url, "BuildUrl.txt");
            TestUtilities.LogAndWrite(url2, "BuildUrl2.txt");
        }



        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#display_images
        /// To force the CDN to display the latest uploaded image, you should add a version 
        /// component to Cloudinary's URLs. The version value is returned by Cloudinary 
        /// as part of the response of the upload API call, and is unique per upload. 
        /// Adding the version component to URLs can be done by setting the Version parameter, 
        /// for example:
        /// </summary>
        [TestMethod]
        public void BuildImageTagVersion()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp.Version("1315746344").BuildImageTag("sample.jpg");
            TestUtilities.LogAndWrite(buildImageTag, "BuildImageTagVersion.txt");
        }


        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#display_images
        /// You can use BuildImageTag to show transformed versions of your uploaded 
        /// images by adding transformation instructions. For example, displaying 
        /// the 'sample' image resized to fill a 100x150 area 
        /// </summary>
        [TestMethod]
        public void BuildImageTagWidthHeightCrop()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp.Transform(
                new Transformation().Width(100).Height(150).Crop("fill")).BuildImageTag("sample.jpg");
            TestUtilities.LogAndWrite(buildImageTag, "BuildImageTagWidthHeightCrop.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#display_images
        /// displaying the uploaded image with the sample public ID, while 
        /// providing an alternate text
        /// </summary>
        [TestMethod]
        public void BuildImageTag()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp.BuildImageTag("sample.jpg", new StringDictionary("alt=Sample Image"));
            TestUtilities.LogAndWrite(buildImageTag, "BuildImageTag.txt");
        }
    }
}
