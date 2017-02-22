using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using ImageProcessor;
using ImageProcessor.Plugins.Effects;

namespace TheGuin2.Commands
{
    [OnCommand("ascii", "I think I am seeing double.")]
    class AsciiCommand : BaseImageCommand
    {
        public AsciiCommand(CmdData data) : base(data)
        { }

        public override void ProcessImage(ref ImageFactory imageFactory)
        {
			Ascii processor = new Ascii();
			processor.DynamicParameter = new AsciiParameters() { FontSize = 2, CharacterCount = 10, PixelPerCharacter = 1 };
			
			Bitmap result = new Bitmap(processor.ProcessImage(imageFactory));
			imageFactory.Reset();
			imageFactory.Load(result);
        }
    }
}
