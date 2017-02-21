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
    [OnCommand("vr", "I think I am seeing double.")]
    class VrCommand : BaseImageCommand
    {
        public VrCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory, ref Bitmap returnBitmap)
        {
			var image = imageFactory.Image;
			returnBitmap = new Bitmap(image);
			
            int halfWidth = (int)Math.Floor((float)returnBitmap.Width / 2.0f);
            for (int x = 0; x < halfWidth; x++)
            {
                for (int y = 0; y < returnBitmap.Height; y++)
                {
                    Color otherColor = returnBitmap.GetPixel(x, y);
                    returnBitmap.SetPixel(x + halfWidth, y, otherColor);
                }
            }
        }
    }
}
