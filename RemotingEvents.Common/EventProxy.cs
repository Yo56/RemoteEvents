using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingEvents.Common
{
    public class EventProxy : MarshalByRefObject
    {

        #region Event Declarations

        public event OnlineUsersChangedEvent OnlineUsersChanged;
        public event NewChatRequestEvent NewChatRequest;
        public event OpenAcceptedChatRequestEvent OpenAcceptedChatRequest;

        #endregion

        #region Lifetime Services

        public override object InitializeLifetimeService()
        {
            return null;            //Returning null holds the object alive until it is explicitly destroyed
        }

        #endregion

        #region Local Handlers

        public void LocallyHandleOnlineUsersChanged(Dictionary<string,string> onlineUsers)
        {
            if (OnlineUsersChanged != null)
                OnlineUsersChanged(onlineUsers);
        }

        public void LocallyHandleNewChatRequest(string senderNickname, string receiverNickname)
        {
            if (NewChatRequest != null)
                NewChatRequest(senderNickname, receiverNickname);
        }

        public void LocallyHandleOpenAcceptedChatRequest(string senderNickname, string receiverNickname)
        {
            if (OpenAcceptedChatRequest != null)
                OpenAcceptedChatRequest(senderNickname, receiverNickname);
        }
        #endregion

    }
}
