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
    [OnCommand("hellodarkness", "My old friend...")]
    class HelloDarknessCommand : BaseAvatarCommand
    {
        public HelloDarknessCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory, ref Bitmap returnBitmap)
        {
			imageFactory.Saturation(-100);
			imageFactory.Filter(ImageProcessor.Imaging.Filters.Photo.MatrixFilters.Polaroid);
			imageFactory.GaussianBlur(4);
			imageFactory.Vignette();
			imageFactory.RoundedCorners(12);
			imageFactory.Rotate(-5);
			var image = imageFactory.Image;
			returnBitmap = new Bitmap(image);
			channel.SendMessage("My old friend...");
        }
    }
}
