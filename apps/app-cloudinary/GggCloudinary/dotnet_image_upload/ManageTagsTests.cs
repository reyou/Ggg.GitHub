using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggCloudinary.dotnet_image_upload
{
    /// <summary>
    /// https://cloudinary.com/documentation/dotnet_image_upload#manage_tags
    /// </summary>
    [TestClass]
    public class ManageTagsTests
    {
        /// <summary>
        /// template
        /// </summary>
        [TestMethod]
        public void Template()
        {

        }

        /// <summary>
        /// The following example clears the given tag from a list of images:
        /// </summary>
        [TestMethod]
        public void ClearAllTags()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TagParams tagParams = new TagParams()
            {
                Command = TagCommand.Replace,
                Tag = "another_tag"
            };
            tagParams.PublicIds.Add("UploadWithTags");
            tagParams.PublicIds.Add("de9wjix4hhnqpxixq6cw");
            TagResult tagResult = cloudinary.Tag(tagParams);
            TestUtilities.LogAndWrite(tagResult, "ClearAllTags.txt");
        }

        /// <summary>
        /// The following example clears the given tag from a list of images:
        /// </summary>
        [TestMethod]
        public void ClearGivenTag()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TagParams tagParams = new TagParams()
            {
                Command = TagCommand.Remove,
                Tag = "another_tag"
            };
            tagParams.PublicIds.Add("UploadWithTags");
            tagParams.PublicIds.Add("de9wjix4hhnqpxixq6cw");
            TagResult tagResult = cloudinary.Tag(tagParams);
            TestUtilities.LogAndWrite(tagResult, "ClearGivenTag.txt");
        }

        /// <summary>
        /// You can modify the assigned tags of an already uploaded image. 
        /// The following example assigns a tag to a list of images:
        /// </summary>
        [TestMethod]
        public void ModifyAssignedTags()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            TagParams tagParams = new TagParams()
            {
                Command = TagCommand.Add,
                Tag = "another_tag"
            };
            tagParams.PublicIds.Add("UploadWithTags");
            tagParams.PublicIds.Add("de9wjix4hhnqpxixq6cw");
            TagResult tagResult = cloudinary.Tag(tagParams);
            TestUtilities.LogAndWrite(tagResult, "ModifyAssignedTags.txt");
        }

        /// <summary>
        /// Cloudinary supports assigning one or more tags to uploaded images. 
        /// Tags allow you to better organize your media library.
        /// </summary>
        [TestMethod]
        public void UploadWithTags()
        {
            Cloudinary cloudinary = TestUtilities.GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(TestUtilities.GetImagesFolder() + @"\car.jpg"),
                PublicId = "UploadWithTags",
                Tags = "special, for_homepage, car, this is an amazing car"
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            TestUtilities.LogAndWrite(uploadResult, "UploadWithTags.txt");

        }
    }
}
