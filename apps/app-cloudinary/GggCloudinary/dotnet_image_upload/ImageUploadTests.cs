using System.Collections.Generic;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_upload
{
    [TestClass]
    public class ImageUploadTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// Sometimes you don't know whether your users would upload image files or raw files. 
        /// It is supported automatically when you use RawUploadParams, however you can turn 
        /// off the automatic mode by passing the second parameter to the Upload function.
        /// https://cloudinary.com/documentation/dotnet_image_upload#raw_file_uploading
        /// </summary>
        [TestMethod]
        public void RawUploadParams()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            RawUploadParams uploadParams = new RawUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\sample_spreadsheet2.xls"),
                PublicId = "sample_spreadsheet2"
            };
            RawUploadResult uploadResult = cloudinary.Upload(uploadParams, "raw");
            string buildUrl = cloudinary.Api.UrlImgUp.ResourceType("raw").BuildUrl("sample_spreadsheet2.xls");
            TestUtilities.LogAndWrite(uploadResult, "RawUploadParams.txt");
            TestUtilities.LogAndWrite(buildUrl, "RawUploadParams-buildUrl.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#raw_file_uploading
        /// </summary>
        [TestMethod]
        public void RawFileUploading()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            RawUploadParams uploadParams = new RawUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\sample_spreadsheet.xls"),
                PublicId = "sample_spreadsheet"
            };
            RawUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "RawFileUploading.txt");
        }


        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#semantic_data_extraction
        /// </summary>
        [TestMethod]
        public void CoordinatesOfAllDetectedFaces()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(@"https://res.cloudinary.com/demo/image/upload/couple.jpg"),
                Faces = true,
                Colors = true,
                Exif = true,
                Metadata = true
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "CoordinatesOfAllDetectedFaces.txt");
        }

        /// <summary>
        /// chained transformation
        /// https://cloudinary.com/documentation/dotnet_image_upload#eager_transformations
        /// You can apply an incoming transformation and generate derived images eagerly 
        /// using the same upload call. The following example does that while also applying 
        /// a chained transformation:
        /// </summary>
        [TestMethod]
        public void ChainedTransformation()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\fifa.jpg"),
                Transformation = new Transformation()
                    .Width(100).Height(120).Crop("limit")
                    .Chain().Crop("crop").X(5).Y(10).Width(40).Height(10),
                EagerTransforms = new List<Transformation>
                {
                    new Transformation().Width(0.2).Crop("scale"),
                    new Transformation().Effect("hue:30")
                }
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "ChainedTransformation.txt");

        }

        /// <summary>
        /// two transformed versions eagerly
        /// https://cloudinary.com/documentation/dotnet_image_upload#eager_transformations
        /// </summary>
        [TestMethod]
        public void TwoTransformedVersionsEagerly()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\last_of_us.jpg"),
                EagerTransforms = new List<Transformation>
                {
                    new Transformation().Width(100).Height(150).Crop("fit").FetchFormat("png"),
                    new Transformation().Named("jpg_with_quality_30")
                }
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "TwoTransformedVersionsEagerly.txt");
        }


        /// <summary>
        /// embeds the derived image
        /// </summary>
        [TestMethod]
        public void EmbedDerivedImage()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string buildUrl = cloudinary.Api.UrlImgUp.Transform(
                    new Transformation().Width(150).Height(100).Crop("thumb").Gravity("face"))
                .BuildUrl("EagerTransformations");
            TestUtilities.LogAndWrite(buildUrl, "EmbedDerivedImage.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#eager_transformations
        /// </summary>
        [TestMethod]
        public void EagerTransformations()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\face.jpg"),
                PublicId = "EagerTransformations",
                EagerTransforms = new List<Transformation>
                {
                    new Transformation().Width(150).Height(100).Crop("thumb").Gravity("face"),
                    new Transformation().Width(200).Height(200),
                    new Transformation().Width(50).Height(50)
                }
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "EagerTransformations.txt");

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#incoming_transformations
        /// Named transformations as well as multiple chained transformations can be applied 
        /// using the Transformation property. The following example first limits the dimensions 
        /// of an uploaded image and then adds a watermark as an overlay.
        /// </summary>
        [TestMethod]
        public void IncomingTransformationsMultiple()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\train.jpg"),
                Transformation = new Transformation()
                    .Width(400).Height(300).X(50).Y(80).Crop("crop").FetchFormat("png")
                    .Chain().Overlay("my_watermark").Flags("relative").Width(0.5)
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "IncomingTransformationsMultiple.txt");

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#incoming_transformations
        /// Another example, this time performing custom coordinates cropping and 
        /// forcing format conversion to PNG:
        /// </summary>
        [TestMethod]
        public void IncomingTransformationsCrop()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\bus.jpg"),
                Transformation =
                    new Transformation().Width(400).Height(300).X(50).Y(80).Crop("crop").FetchFormat("png")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "IncomingTransformationsCrop.txt");

        }
        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#incoming_transformations
        /// </summary>
        [TestMethod]
        public void IncomingTransformations()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\apartment.jpg"),
                Transformation = new Transformation().Width(640).Height(480).Crop("limit")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "IncomingTransformations.txt");

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#data_uploading_options
        /// </summary>
        [TestMethod]
        public void RemoteHttpUrLs()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(@"https://image.freepik.com/free-icon/vimeo-logo_318-32560.jpg")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "RemoteHttpUrLs.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#public_id
        /// </summary>
        [TestMethod]
        public void UseFilename()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\microphone.jpg"),
                UseFilename = true
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "UseFilename.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#public_id
        /// </summary>
        [TestMethod]
        public void PublicIdFolders()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\airplane.jpg"),
                PublicId = "my_folder/my_sub_folder/my_name"
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "PublicIdFolders.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#public_id
        /// </summary>
        [TestMethod]
        public void PublicId()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\chicken.jpg"),
                PublicId = "sample_id"
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "PublicId.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_image_upload#server_side_upload
        /// </summary>
        [TestMethod]
        public void ServerSideUpload()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\lego.jpg")
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "ServerSideUpload.txt");
        }

    }
}
