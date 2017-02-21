using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TheGuin2.Commands
{
    [OnCommand("doublewaaw", "Mirror an image 4 ways.")]
    class DoubleWaawCommand : BaseImageCommand
    {
        public DoubleWaawCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref Image image, ref Bitmap bitmap)
        {
            int halfWidth = (int)Math.Floor((float)bitmap.Width / 2.0f);
            int halfHeight = (int)Math.Floor((float)bitmap.Height / 2.0f);
            for (int x = 0; x < halfWidth; x++)
            {
                for (int y = 0; y < halfHeight; y++)
                {
                    Color otherColor = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(bitmap.Width - 1 - x, bitmap.Height - 1 - y, otherColor);
                    bitmap.SetPixel(bitmap.Width - 1 - x, y, otherColor);
                    bitmap.SetPixel(x, bitmap.Height - 1 - y, otherColor);
                }
            }
        }
    }
}
