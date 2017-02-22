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
    class HelloDarknessCommand : BaseImageCommand
    {
        public HelloDarknessCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory)
        {
			imageFactory.Resize(new Size(512, 512));
			imageFactory.Saturation(-100);
			imageFactory.Filter(ImageProcessor.Imaging.Filters.Photo.MatrixFilters.Polaroid);
			imageFactory.GaussianBlur(15);
			imageFactory.Vignette();
			imageFactory.RoundedCorners(30);
			imageFactory.Rotate(-5);
			channel.SendMessage("My old friend...");
        }
    }
}
