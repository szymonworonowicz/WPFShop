using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace WpfProject.ImagesHelpers
{
    public class ImageResizer
    {

        public byte[] resize(string uri)
        {
            byte[] buffer;
            using (var image = System.Drawing.Image.FromFile(uri))
            {
                var resized = new Bitmap(150, 150);
                using (var graphic = Graphics.FromImage(resized))
                {
                    graphic.DrawImage(image, 0, 0, 150, 150);


                    using (var stream = new MemoryStream())
                    {
                        resized.Save(stream, image.RawFormat);
                        buffer = stream.ToArray();

                    }

                }

            }

            return buffer;
        }
    }
}
