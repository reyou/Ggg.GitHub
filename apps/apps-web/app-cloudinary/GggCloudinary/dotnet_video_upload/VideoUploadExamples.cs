using System.Collections.Generic;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_video_upload
{
    [TestClass]
    public class VideoUploadExamples
    {

        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_video_upload#example_3_client_side_upload_from_the_browser_unsigned_upload
        /// </summary>
        [TestMethod]
        public void BuildUnsignedUploadForm()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string input = cloudinary.Api.BuildUnsignedUploadForm("video_id", "my_upload_preset", "video", new SortedDictionary<string, object>() {
                    { "public_id", "my_video" },
                    { "tags", "user_218,screencast" }
                }
            );
            TestUtilities.LogAndWrite(input, "BuildUnsignedUploadForm.txt");
        }

        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_video_upload#example_2_client_side_upload_from_the_browser_signed_upload
        /// </summary>
        [TestMethod]
        public void BuildUploadForm()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            string input = cloudinary.Api.BuildUploadForm("video_id", "video",
                new SortedDictionary<string, object>() {
                    { "eager", "sp_full_hd/83u8"},
                    { "eager_async", true },
                    { "eager_notification_url", "http://mysite/notify_endpoint" }
                },
                new Dictionary<string, string>() {
                    { "id", "my_upload_tag" }
                }
            );
            TestUtilities.LogAndWrite(input, "BuildUploadForm.txt");
        }
        /// <summary>
        /// https://cloudinary.com/documentation/dotnet_video_upload#example_1_server_side_upload
        /// </summary>
        [TestMethod]
        public void ServerSideUpload()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            VideoUploadParams uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(TestUtilities.GetVideosFolder() + @"\dog.mp4"),
                PublicId = "my_folder/my_sub_folder/dog_closeup",
                EagerTransforms = new List<Transformation>()
                {
                    new Transformation().Width(300).Height(300).Crop("pad").AudioCodec("none"),
                    new Transformation().Width(160).Height(100).Crop("crop").Gravity("south").AudioCodec("none"),
                },
                EagerAsync = true,
                EagerNotificationUrl = "https://requestbin.fullcontact.com/1964k7d1"
            };
            VideoUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "ServerSideUpload.txt");
        }
    }
}
