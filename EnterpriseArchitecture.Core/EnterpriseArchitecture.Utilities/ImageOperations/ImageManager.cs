using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace EnterpriseArchitecture.Utilities.ImageOperations
{
    /// <summary>
    /// Operations on image files are performed by methods provided by the ImageManager static class.
    /// </summary>
    public static class ImageManager
    {
        static string[] ImageFormatTexts = { "bmp", "emf", "exif", "gif", "icon", "jpeg", "memorybmp", "png", "tiff", "wmf" };
        static ImageFormat[] ImageFormats = { ImageFormat.Bmp, ImageFormat.Emf, ImageFormat.Exif, ImageFormat.Gif, ImageFormat.Icon, ImageFormat.Jpeg, ImageFormat.MemoryBmp, ImageFormat.Png, ImageFormat.Tiff, ImageFormat.Wmf };

        public static byte[] ImageToByteArray(System.Drawing.Image inputImage, string contentType)
        {
            MemoryStream memoryStream = new MemoryStream();
            inputImage.Save(memoryStream, ContentTypeToImageFormat(contentType));
            return memoryStream.ToArray();
        }

        public static Image ByteArrayToImage(byte[] inputByteArray)
        {
            MemoryStream memoryStream = new MemoryStream(inputByteArray);
            Image outputImage = Image.FromStream(memoryStream);
            return outputImage;
        }

        public static ImageFormat ContentTypeToImageFormat(string contentType)
        {
            for (int i = 0; i < ImageFormatTexts.Length; i++)
            {
                if (ImageFormatTexts[i] == contentType.Split("/".ToCharArray())[1].ToString())
                {
                    return ImageFormats.ToList()[i];
                }
            }
            return null;
        }

        public static byte[] ConvertToSize(byte[] image, int? width, int? height)
        {
            bool noCrop = false;
            width = (width == 0) ? null : width;
            height = (height == 0) ? null : height;

            if ((width != null) || (height != null))
            {
                System.Drawing.Image originalImage = ByteArrayToImage(image);

                if (height == null)
                {
                    height = Convert.ToInt32(((float)width / (float)originalImage.Width) * originalImage.Height);
                    noCrop = true;
                }

                if (width == null)
                {
                    width = Convert.ToInt32(((float)height / (float)originalImage.Height) * originalImage.Width);
                    noCrop = true;
                }

                if (noCrop)
                {
                    Bitmap bitmapPhoto = new Bitmap(originalImage, width.Value, height.Value);
                    image = ImageToByteArray((System.Drawing.Image)bitmapPhoto, "image/png");
                }
                else
                {
                    Rectangle cropArea = new Rectangle();
                    float rate = (float)width.Value / (float)height.Value;
                    float originalRate = (float)originalImage.Width / (float)originalImage.Height;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                    int x = 0, y = 0;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

                    if (rate < originalRate)
                    {
                        cropArea.X = (int)((originalImage.Width - rate * originalImage.Height) / 2);
                        cropArea.Y = 0;
                        cropArea.Height = originalImage.Height;
                        cropArea.Width = (int)(rate * originalImage.Height);
                    }
                    else
                    {
                        cropArea.X = 0;
                        cropArea.Y = (int)((originalImage.Height - originalImage.Width / rate) / 2);
                        cropArea.Height = (int)(originalImage.Width / rate);
                        cropArea.Width = originalImage.Width;
                    }
                    var bmpImage = new Bitmap(originalImage);
                    var cbmpImage = new Bitmap(originalImage, width.Value, height.Value);

                    //System.Drawing.Image croppedImage = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
                    //Bitmap bmCropped = new Bitmap(croppedImage, w.Value, h.Value);
                    //image = imageToByteArray((System.Drawing.Image)bmCropped, "image/png"); 

                    Graphics grp = Graphics.FromImage(cbmpImage);
                    grp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    /* draw the image into the target bitmap */
                    grp.Clear(Color.Transparent);
                    grp.DrawImage(bmpImage, new Rectangle(0, 0, width.Value, height.Value), new Rectangle(cropArea.X, cropArea.Y, cropArea.Width, cropArea.Height), GraphicsUnit.Pixel);
                    grp.Dispose();

                    //grp.DrawImage(bmpImage,w.Value,)
                    //Bitmap bmCropped = new Bitmap(bmpImage, w.Value, h.Value);
                    image = ImageToByteArray((System.Drawing.Image)cbmpImage, "image/png");
                }
            }

            return image;
        }
    }
}