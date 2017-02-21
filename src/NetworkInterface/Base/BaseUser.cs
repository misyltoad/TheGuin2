using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public abstract class BaseUser
    {
        public abstract string GetUsername();
        public abstract string GetNickname();
        public abstract string GetTag();
        public abstract string GetId();

        public abstract void GiveRole(BaseRole role);

		public abstract void SendMessage(string message);
        public abstract bool IsBotOwner();
        public abstract bool IsOwner();
        public abstract bool IsAdmin();
    }
}
