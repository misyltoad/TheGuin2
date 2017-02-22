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
                    ImageFactory imageFactory = new ImageFactory(true, true);
                    imageFactory.Load(imageBytes);

                    try
                    {
                        channel.SendMessage("Processing...");
                        ProcessImage(ref imageFactory);
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

                        string fileId = StaticConfig.Paths.TempPath + "/" + System.Guid.NewGuid().ToString() + imageUrl.Substring(imageUrl.LastIndexOf('.'));
                        imageFactory.Save(fileId);

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
                BaseUser user = null;

                if (argsString != "")
                    user = server.FindUser(argsString);

                if (user == null && (argsString != null && argsString != "" && argsString != " " && argsString != "  "))
                {
                    channel.SendMessage("Couldn't get user or URL.");
                    return;
                }

                if (user == null)
                    user = this.user;

                ImageFactory imageFactory = new ImageFactory(true, true);
                imageFactory.Load(user.GetAvatar());

                try
                {
                    channel.SendMessage("Processing...");
                    ProcessImage(ref imageFactory);
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

                    string fileId = StaticConfig.Paths.TempPath + "/" + System.Guid.NewGuid().ToString() + ".png";
                    imageFactory.Save(fileId);

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
        }

        public abstract void ProcessImage(ref ImageFactory imageFactory);
    }
}
