using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TheGuin2.Commands
{
    public abstract class BaseImageCommand : BaseCommand
    {
        public BaseImageCommand(CmdData data) : base(data)
        { }

        public override void Execute()
        {
            try
            {
                string imageUrl = args[0];
                var webClient = new System.Net.WebClient();
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                try
                {

                    Image image = Image.FromStream(new MemoryStream(imageBytes));
                    Bitmap bitmap = new Bitmap(image);

                    try
                    {
                        channel.SendMessage("Processing...");
                        ProcessImage(ref image, ref bitmap);
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
                        string fileId = StaticConfig.Paths.TempPath + System.Guid.NewGuid().ToString() + ".jpg";
                        bitmap.Save(fileId, System.Drawing.Imaging.ImageFormat.Jpeg);
                        try
                        {
                            channel.SendFile(fileId);
                        } catch
                        {
                            channel.SendMessage("Couldn't attach file.");
                        }
						try
						{
							File.Delete(fileId);
						}
						catch { }
                    } catch
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

        public abstract void ProcessImage(ref Image image, ref Bitmap bitmap);
    }
}
