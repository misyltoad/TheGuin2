using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using ImageProcessor;
using ImageProcessor.Plugins.Cair;

namespace TheGuin2.Commands
{
    [OnCommand("magik", "Squish an image.")]
    class MagikCommand : BaseImageCommand
    {
        public MagikCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory)
        {
			var image = imageFactory.Image;
			var size = new Size(image.Size.Width * 2, image.Size.Height * 2);
			var doubleSize = new Size(size.Width * 2, size.Height * 2);
			imageFactory.Resize(doubleSize);
			imageFactory.ContentAwareResize(size);
        }
    }
}
