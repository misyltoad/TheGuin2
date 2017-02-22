using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [AttributeUsage(AttributeTargets.All)]
    public class OnCommand : System.Attribute
    {
        public OnCommand(string commandName, string commandDescription)
        {
            name = commandName;
            description = commandDescription;
        }
        public string name;
        public string description;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class OnMessage : System.Attribute
    { }

    [AttributeUsage(AttributeTargets.All)]
    public class OnUserUnbanned : System.Attribute
    { }

    [AttributeUsage(AttributeTargets.All)]
    public class OnUserBanned : System.Attribute
    { }

    [AttributeUsage(AttributeTargets.All)]
    public class OnUserLeft : System.Attribute
    { }

    [AttributeUsage(AttributeTargets.All)]
    public class OnUserJoined : System.Attribute
    {}

    [AttributeUsage(AttributeTargets.All)]
    public class OnUserChange : System.Attribute
    { }
}
