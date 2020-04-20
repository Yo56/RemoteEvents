using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDIN_PROJ1.Common
{
    public class EventProxy : MarshalByRefObject
    {

        #region Event Declarations

        public event OnlineUsersChangedEvent OnlineUsersChanged;
        public event NewChatRequestEvent NewChatRequest;
        public event OpenAcceptedChatRequestEvent OpenAcceptedChatRequest;
        public event CloseOtherUserChatPageEvent CloseOtherUserChatPage;

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

        public void LocallyHandleCloseOtherUserChatPageRequest(string senderNickname, string receiverNickname)
        {
            if (CloseOtherUserChatPage != null)
                CloseOtherUserChatPage(senderNickname, receiverNickname);
        }
        #endregion

    }
}
