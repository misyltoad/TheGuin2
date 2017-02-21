using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public abstract class BaseChannel
    {
        public abstract void SendMessage(string message);

        public abstract List<BaseUser> GetUsers();
        public abstract BaseServer GetServer();
        public abstract string GetName();
        public abstract void SendFile(string filename);
    }
}
