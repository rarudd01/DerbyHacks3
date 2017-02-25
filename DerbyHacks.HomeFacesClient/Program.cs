using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using DerbyHacks.HomeFacesClient;

namespace DerbyHacks.HomeFacesClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Capture capture = new Capture();

            capture.Grab();
            Mat frame = capture.QueryFrame();
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            //image.Save("c:\\button.bmp");

            Save(image, "test.jpeg", long.MaxValue);
        }

        public static void Save(Emgu.CV.Image<Bgr, Byte> img, string filename, double quality)
        {
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                (long)quality
                );

            var jpegCodec = (from codec in ImageCodecInfo.GetImageEncoders()
                             where codec.MimeType == "image/jpeg"
                             select codec).Single();

            img.Bitmap.Save(filename, jpegCodec, encoderParams);
        }
    }
}

