using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WeddingSite.Models.PhotoApi;

namespace WeddingSite.Controllers
{
    [RoutePrefix("api/Photos")]
    public class EngagementPicturesApiController : ApiController
    {
        public class MyStreamProvider : MultipartFormDataStreamProvider
        {
            public MyStreamProvider(string uploadPath)
                : base(uploadPath)
            {

            }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                string fileName = headers.ContentDisposition.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = Guid.NewGuid().ToString() + ".data";
                }
                return fileName.Replace("\"", string.Empty);
            }
        }

        private CloudStorageAccount _engagementCloudStore;
        private CloudBlobClient _engagementBlobClient;
        private CloudBlobContainer _engagementBlobContainer;

        public EngagementPicturesApiController()
        {
            _engagementCloudStore = CloudStorageAccount.Parse("");
            _engagementBlobClient = _engagementCloudStore.CreateCloudBlobClient();
            _engagementBlobContainer = _engagementBlobClient.GetContainerReference("engagement-pictures");
        }

        [Authorize(Roles = "Admin")]
        public async Task<List<string>> Post()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = Path.GetTempPath();

                MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                List<string> messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    using (Image originalImage = Image.FromFile(file.LocalFileName))
                    {
                        await UploadImage(originalImage, Path.GetFileName(file.LocalFileName), file.Headers.ContentType.ToString(), "XL", 600);
                        await UploadImage(originalImage, Path.GetFileName(file.LocalFileName), file.Headers.ContentType.ToString(), "L", 500);
                        await UploadImage(originalImage, Path.GetFileName(file.LocalFileName), file.Headers.ContentType.ToString(), "M", 400);
                        await UploadImage(originalImage, Path.GetFileName(file.LocalFileName), file.Headers.ContentType.ToString(), "S", 300);
                        await UploadImage(originalImage, Path.GetFileName(file.LocalFileName), file.Headers.ContentType.ToString(), "thumb", 200);
                    }
                }

                return messages;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
        }

        private async Task UploadImage(Image originalImage, string fileName, string contentType, string sizeSuffix, int width)
        {
            using (MemoryStream xlStream = new MemoryStream())
            using (Bitmap xl = ResizeImage(originalImage, width))
            {
                ImageFormat format;
                string extension = (Path.GetExtension(fileName) ?? ".png");

                switch (extension.ToUpper())
                {
                    case ".JPG":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".PNG":
                        format = ImageFormat.Png;
                        break;
                    default:
                        format = ImageFormat.Png;
                        break;
                }

                xl.Save(xlStream, format);
                xlStream.Position = 0;

                CloudBlockBlob blockBlob = _engagementBlobContainer.GetBlockBlobReference(string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fileName), sizeSuffix, extension));
                blockBlob.Properties.ContentType = contentType;
                await blockBlob.UploadFromStreamAsync(xlStream);
            }
        }

        public static Bitmap ResizeImage(Image image, int width)
        {
            int height = (int)((1.0 * width / image.Width) * image.Height);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        [Route("list/{library}/{size}")]
        public IEnumerable<string> GetImageNames(string library, string size)
        {
            string sizeSuffix = string.Format("_{0}.", size);
            var blobNames = _engagementBlobContainer.ListBlobs().Select(b => b.Uri.Segments.Last()).ToList();

            return blobNames.Where(n => n.Contains(sizeSuffix)).ToList();
        }

        [Route("package/{library}")]
        public IEnumerable<PhotoPackage> GetPhotoPackage(string library)
        {
            var blobNames = _engagementBlobContainer.ListBlobs().Select(b => b.Uri.Segments.Last())
                .Select(b => new
                {
                    BlobName = b,
                    BlobGrouping = b.Substring(0, b.LastIndexOf("_")),
                    SizeSuffix = Path.GetFileNameWithoutExtension(b)
                        .Replace(b.Substring(0, b.LastIndexOf("_") + 1), string.Empty)
                });

            var blobPackages = blobNames
                .GroupBy(b => b.BlobGrouping)
                .Select(bg => new PhotoPackage()
                {
                    LibraryName = library,
                    ThumbNail = bg.Where(b => b.SizeSuffix == "thumb")
                        .Select(b => b.BlobName)
                        .FirstOrDefault(),
                    Small = bg.Where(b => b.SizeSuffix == "S")
                        .Select(b => b.BlobName)
                        .FirstOrDefault(),
                    Medium = bg.Where(b => b.SizeSuffix == "M")
                        .Select(b => b.BlobName)
                        .FirstOrDefault(),
                    Large = bg.Where(b => b.SizeSuffix == "L")
                        .Select(b => b.BlobName)
                        .FirstOrDefault(),
                    ExtraLarge = bg.Where(b => b.SizeSuffix == "XL")
                        .Select(b => b.BlobName)
                        .FirstOrDefault()
                })
                .ToList();

            return blobPackages;
        }

        [Route("content/{library}/{imageName}")]
        // GET api/engagementpicturesapi
        public HttpResponseMessage Get(string imageName)
        {
            var blob = _engagementBlobContainer.GetBlockBlobReference(imageName);

            if (blob.Exists())
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                using (var imageMem = new MemoryStream())
                {
                    blob.DownloadToStream(imageMem);
                    //Image image = Image.FromStream(imageMem);
                    result.Content = new ByteArrayContent(imageMem.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                }

                //MemoryStream memoryStream = new MemoryStream();
                //image.Save(memoryStream, ImageFormat.Jpeg);


                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        // GET api/engagementpicturesapi/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/engagementpicturesapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/engagementpicturesapi/5
        public void Delete(int id)
        {
        }
    }
}
