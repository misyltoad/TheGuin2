using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2.Commands
{
    [OnUserJoined]
    class JoinLog
    {
        public JoinLog(BaseUser user, BaseServer server)
		{
			var logChannel = server.FindChannelByName("theguin-log");
			if (logChannel == null)
				return;
			
			logChannel.SendMessage(user.GetDataString() + " has joined.");
		}
    }
	
	[OnUserUnbanned]
    class UnbannedLog
    {
        public UnbannedLog(BaseUser user, BaseServer server)
		{
			var logChannel = server.FindChannelByName("theguin-log");
			if (logChannel == null)
				return;
			
			logChannel.SendMessage(user.GetDataString() + " was unbanned.");
		}
    }
	
	[OnUserBanned]
    class BannedLog
    {
        public BannedLog(BaseUser user, BaseServer server)
		{
			var logChannel = server.FindChannelByName("theguin-log");
			if (logChannel == null)
				return;
			
			logChannel.SendMessage(user.GetDataString() + " was banned.");
		}
    }
	
	[OnUserLeft]
	class LeaveLog
	{
		public LeaveLog(BaseUser user, BaseServer server)
		{
			var logChannel = server.FindChannelByName("theguin-log");
			if (logChannel == null)
				return;
			
			logChannel.SendMessage(user.GetDataString() + " has left." ); 
		}
	}
	
	[OnMessage]
	class MessageLog
	{
		public MessageLog(BaseUser user, BaseServer server, BaseChannel channel, BaseMessage message)
		{
			var logChannel = server.FindChannelByName("theguin-chat-log");
			if (logChannel == null)
				return;
			
			logChannel.SendMessage(user.GetDataString() + " said: ``" + message.GetText() ); 
		}
	}
	
	[OnUserChange]
    class ChangeLog
    {
        public ChangeLog(BaseUser oldUser, BaseUser newUser, BaseServer server)
		{
			var logChannel = server.FindChannelByName("theguin-log");
			if (logChannel == null)
				return;
			
			if (oldUser.GetNickname() != newUser.GetNickname() || oldUser.GetUsername() != newUser.GetUsername())
				logChannel.SendMessage(oldUser.GetDataString() + " became: ``" + newUser.GetDataString() );
			
			var oldRoles = oldUser.GetRoles();
			var newRoles = newUser.GetRoles();
			
			var oldRoleNames = new List<string>();
			var newRoleNames = new List<string>();
			
			foreach (var role in oldRoles)
				oldRoleNames.Add(role.GetName());
				
			foreach (var role in newRoles)
				newRoleNames.Add(role.GetName());
				
			var differenceOldNew = oldRoleNames.Except(newRoleNames).ToList();
			var differenceNewOld = newRoleNames.Except(oldRoleNames).ToList();
			
			foreach (var role in differenceNewOld)
				logChannel.SendMessage(newUser.GetDataString() + " gained the role: ``" + role + "``");
				
			foreach (var role in differenceOldNew)
				logChannel.SendMessage(newUser.GetDataString() + " lost the role: ``" + role + "``" );
		}
    }
}
