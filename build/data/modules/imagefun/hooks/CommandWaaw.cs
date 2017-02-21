using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TheGuin2.Commands
{
    [OnCommand("waaw", "Mirror an image horizontally.")]
    class WaawCommand : BaseImageCommand
    {
        public WaawCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref Image image, ref Bitmap bitmap)
        {
            int halfWidth = (int)Math.Floor((float)bitmap.Width / 2.0f);
            for (int x = 0; x < halfWidth; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color otherColor = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(bitmap.Width - 1 - x, y, otherColor);
                }
            }
        }
    }
}
