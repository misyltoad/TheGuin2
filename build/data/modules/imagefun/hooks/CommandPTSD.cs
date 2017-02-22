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
    [OnCommand("ptsd", "Etho's at it again.")]
    class PTSDCommand : BaseImageCommand
    {
        public PTSDCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory)
        {
			imageFactory.Resize(new Size(512, 512));
			imageFactory.Pixelate(12);
			imageFactory.GaussianSharpen(20);
			imageFactory.Brightness(50);
			imageFactory.Contrast(50);
			imageFactory.Filter(ImageProcessor.Imaging.Filters.Photo.MatrixFilters.Invert);
			
			MemoryStream stream = new MemoryStream();
			imageFactory.Save(stream);
			
			imageFactory.Reset();
			imageFactory.Load(stream);
			imageFactory.Saturation(100);

			channel.SendMessage("PUNT");
        }
    }
}
