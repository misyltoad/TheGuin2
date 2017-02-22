using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using ImageProcessor;

namespace TheGuin2.Commands
{
    [OnCommand("doublewaaw", "Mirror an image 4 ways.")]
    class DoubleWaawCommand : BaseImageCommand
    {
        public DoubleWaawCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory)
        {
			var image = imageFactory.Image;
			var returnBitmap = new Bitmap(image);
			
            int halfWidth = (int)Math.Floor((float)returnBitmap.Width / 2.0f);
            int halfHeight = (int)Math.Floor((float)returnBitmap.Height / 2.0f);
            for (int x = 0; x < halfWidth; x++)
            {
                for (int y = 0; y < halfHeight; y++)
                {
                    Color otherColor = returnBitmap.GetPixel(x, y);
                    returnBitmap.SetPixel(returnBitmap.Width - 1 - x, returnBitmap.Height - 1 - y, otherColor);
                    returnBitmap.SetPixel(returnBitmap.Width - 1 - x, y, otherColor);
                    returnBitmap.SetPixel(x, returnBitmap.Height - 1 - y, otherColor);
                }
            }
			
			imageFactory.Reset();
			imageFactory.Load(returnBitmap);
        }
    }
}
