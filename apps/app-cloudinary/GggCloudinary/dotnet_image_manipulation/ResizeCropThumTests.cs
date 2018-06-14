using System;
using CloudinaryDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_manipulation
{
    [TestClass]
    public class ResizeCropThumTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TestUtilities.LogAndWrite(null, "Template.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#fetch_images
        /// Fetch images
        /// </summary>
        [TestMethod]
        public void FetchImages()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp.Action("fetch").BuildImageTag(
                "http://upload.wikimedia.org/wikipedia/commons/4/46/Jennifer_Lawrence_at_the_83rd_Academy_Awards.jpg");

            /*The same remote image is available in the following example after turning it into a
             150x150 face detection based thumbnail with rounded corners:*/
            string imageTag = cloudinary.Api.UrlImgUp.Action("fetch")
                .Transform(new Transformation()
                    .Width(150)
                    .Height(150)
                    .Crop("thumb")
                    .Gravity("face")
                    .Radius(20))
                .BuildImageTag(
                    "http://upload.wikimedia.org/wikipedia/commons/4/46/Jennifer_Lawrence_at_the_83rd_Academy_Awards.jpg");
            /*You can convert the format of fetched images by setting the FetchFormat parameter
             to your preferred image type. The following example converted a remote PNG image 
             to the JPG format:*/
            string transformedTag = cloudinary.Api.UrlImgUp
                .Action("fetch")
                .Transform(new Transformation()
                    .Width(150).Height(150).Crop("fill").FetchFormat("jpg"))
                .BuildImageTag("http://upload.wikimedia.org/wikipedia/commons/e/e4/Globe.png");

            TestUtilities.LogAndWrite(buildImageTag, "FetchImages.txt");
            TestUtilities.LogAndWrite(imageTag, "FetchImagesRoundedCorners.txt");
            TestUtilities.LogAndWrite(transformedTag, "FetchImagesTransformed.txt");
        }


        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#filters_and_effects
        /// Text layers
        /// </summary>
        [TestMethod]
        public void TextLayers()
        {
            /*You can use any image as an overlay or underlay of another image.
             This includes uploaded images, social profile pictures and also text layers. 
             In order to append text layers, first you need to define your custom 
             text style using our API. See Text creation for more details.*/
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#filters_and_effects
        /// Overlays, underlays and watermarks
        /// </summary>
        [TestMethod]
        public void OverlaysUnderlaysAndWatermarks()
        {
            /*Cloudinary supports generating new images by layer multiple
             images one on top of the other.*/
            /*Add an overlay to an image by setting the Overlay parameter to the
             public ID of a previously uploaded image. All additional transformation 
             parameters will be applied to the overlay images instead of the original one.*/

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#filters_and_effects
        /// Filters and effects
        /// </summary>
        [TestMethod]
        public void FiltersAndEffects()
        {
            /*Cloudinary supports applying various filters and effects on your pictures.
             A full list of filters and effects is available here. You may also want 
             to look at the following blog posts:*/
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#named_transformations
        /// Named transformations
        /// </summary>
        [TestMethod]
        public void NamedTransformations()
        {
            /*Cloudinary's URLs include your full transformation instructions.
             If you use Cloudinary to apply complex transformations on your images, 
             you may end up with somewhat long URLs. */
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#chained_transformations
        /// Chained transformations
        /// </summary>
        [TestMethod]
        public void ChainedTransformations()
        {
            /*Cloudinary supports powerful transformations. You can even combine multiple
             transformations together as part of a single transformation request, e.g. 
             crop an image and add a border. In certain cases you may want to perform 
             additional transformations on the result of a single transformation request. 
             In order to do that, you can use Cloudinary's chained transformations. */
        }

        /// <summary>
        /// Alter shape
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#alter_shape
        /// </summary>
        [TestMethod]
        public void AlterShape()
        {
            /*Cloudinary supports various shape and content image manipulation features.
 You can tell Cloudinary to round your images corners by setting the Radius parameter. 
 For example, using a radius of 20 pixels: */
        }


        /// <summary>
        /// Facebook and Twitter profile pictures
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#facebook_and_twitter_profile_pictures
        /// </summary>
        [TestMethod]
        public void FacebookAndTwitter()
        {
            /*The facebook, twitter_profile and twitter_name actions can be used to
             embed the social profile pictures of your users inside your web or mobile apps.*/
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp
                .Action("facebook")
                .Transform(new Transformation().Width(90).Height(98).Crop("fill").Gravity("face"))
                .BuildImageTag("billclinton.jpg");
            TestUtilities.LogAndWrite(buildImageTag, "FacebookAndTwitter.txt");

        }

        /// <summary>
        /// Face detection
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#face_detection
        /// </summary>
        [TestMethod]
        public void FaceDetection()
        {
            /*Cloudinary can detect one or more faces in a picture. This allows it to
             smartly crop photos or to manipulate images based on the automatically 
             detected faces in them.
             You can transform an image to fill given dimensions while keeping 
             the photographed subject's 
             face visible, by setting the Gravity parameter to face together 
             with the fill crop mode:*/
            throw new NotImplementedException();
        }

        /// <summary>
        /// Format conversion
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#format_conversion
        /// </summary>
        [TestMethod]
        public void FormatConversion()
        {
            // The supported image formats are: jpg, png, gif, bmp, tiff, 
            // ico, pdf, eps, psd, webp, svg, wdp
            throw new NotImplementedException();
        }

        /// <summary>
        /// You can scale an image to an exact width and height by setting the Crop parameter to scale
        /// </summary>
        [TestMethod]
        public void ScaleMode()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string scaleTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("scale"))
                .BuildImageTag("sample.jpg");
            /*Cloudinary's fit cropping mode (set Crop to fit) transforms the image
             to fit in a given rectangle while retaining its original proportions.*/
            string fitTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("fit"))
                .BuildImageTag("sample.jpg");
            /*Transform an image to fill specific dimensions completely while retaining its original
             proportions by setting Crop to fill. Only part of the original image might be visible 
             if the required proportions are different than the original ones.*/
            string fillTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("fill"))
                .BuildImageTag("sample.jpg");
            /*By default, fill keeps the center of the image centered while cropping
             the image to fill the given dimensions. You can control this behavior by 
             specifying the Gravity parameter. In the following example, gravity is set to 
             South East.*/
            string gravityTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("fill").Gravity("south_east"))
                .BuildImageTag("sample.jpg");
            // more examples
            // https://cloudinary.com/documentation/dotnet_image_manipulation#limit_mode
            // https://cloudinary.com/documentation/dotnet_image_manipulation#crop_mode
            // https://cloudinary.com/documentation/dotnet_image_manipulation#percentage_based_resizing
            TestUtilities.LogAndWrite(scaleTag, "ScaleMode.txt");
            TestUtilities.LogAndWrite(fitTag, "FitMode.txt");
            TestUtilities.LogAndWrite(fillTag, "FillMode.txt");
            TestUtilities.LogAndWrite(gravityTag, "GravityMode.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#resize_crop_and_thumbnails
        /// If you want to transform an uploaded image to a certain dimension and display 
        /// it within your page in a different dimension, you can use SetHtmlWidth 
        /// and SetHtmlHeight methods. Here's an example of the Cloudinary code and 
        /// its equivalent image tag:
        /// </summary>
        [TestMethod]
        public void SetHtmlWidthSetHtmlHeight()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            Transformation trans = new Transformation().Width(100).Height(150).Crop("fill").SetHtmlWidth(50).SetHtmlHeight(75);
            string tag = cloudinary.Api.UrlImgUp.Transform(trans).BuildImageTag("sample.jpg");
            TestUtilities.LogAndWrite(tag, "SetHtmlWidthSetHtmlHeight.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_manipulation#resize_crop_and_thumbnails
        /// </summary>
        [TestMethod]
        public void BuildImageTagResizeCrop()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildImageTag = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(100).Height(150).Crop("fill"))
                .BuildImageTag("sample.jpg");
            TestUtilities.LogAndWrite(buildImageTag, "BuildImageTagResizeCrop.txt");
        }
    }
}
