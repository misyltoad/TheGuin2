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
    public abstract class BaseAvatarCommand : BaseCommand
    {
        public BaseAvatarCommand(CmdData data) : base(data)
        { }

        public override void Execute()
        {
            BaseUser user = null;

            if (argsString != "")
                user = server.FindUser(argsString);

            if (user == null)
                return;

            try
            {
                Bitmap bitmap = user.GetAvatar();

                try
                {
                    ImageFactory imageFactory = new ImageFactory(true, true);
                    imageFactory.Load(bitmap);

                    Bitmap returnBitmap = null;

                    try
                    {
                        channel.SendMessage("Processing...");
                        ProcessImage(ref imageFactory, ref returnBitmap);
                    }
                    catch
                    {
                        channel.SendMessage("Error processing image.");
                    }

                    try
                    {
                        try
                        {
                            Directory.CreateDirectory(StaticConfig.Paths.TempPath);
                        }
                        catch
                        { }

                        string fileId = StaticConfig.Paths.TempPath + System.Guid.NewGuid().ToString() + ".png";
                        returnBitmap.Save(fileId, System.Drawing.Imaging.ImageFormat.Png);

                        try
                        {
                            channel.SendFile(fileId);
                        }
                        catch
                        {
                            channel.SendMessage("Couldn't attach file.");
                        }
                        try
                        {
                            File.Delete(fileId);
                        }
                        catch { }
                    }
                    catch
                    {
                        channel.SendMessage("Internal error.");
                    }
                }
                catch
                {
                    channel.SendMessage("Invaid Image!");
                }
            }
            catch
            {
                channel.SendMessage("Invaid URL!");
            }
        }

        public abstract void ProcessImage(ref ImageFactory imageFactory, ref Bitmap returnBitmap);
    }
}
