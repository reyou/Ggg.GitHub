using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_video_manipulation
{
    /// <summary>
    /// https://cloudinary.com/documentation/dotnet_video_manipulation
    /// </summary>
    [TestClass]
    public class VideoTransformationTests
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
        /// The following example resizes the dog video to 40% of it's original size and rotates it by 20 degrees
        /// </summary>
        [TestMethod]
        public void VideoResizeAndRotate()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            VideoUploadResult videoUploadResult = TestUtilities.UploadVideo();
            Transformation transformation = new Transformation()
                .Width(0.4).Angle(20).Chain()
                .Overlay("cloudinary_icon").Opacity(50).Width(60).Gravity("south_east").Y(15).X(60);
            string buildVideoTag = cloudinary.Api.UrlVideoUp.Transform(transformation)
                .BuildVideoTag("dog");
            TestUtilities.LogAndWrite(videoUploadResult, "UploadVideo.txt");
            TestUtilities.LogAndWrite(buildVideoTag, "VideoResizeAndRotate.txt");
        }
    }
}
