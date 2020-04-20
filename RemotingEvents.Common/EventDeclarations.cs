using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDIN_PROJ1.Common
{

    public delegate void OnlineUsersChangedEvent(Dictionary<String, String> onlineUsers);
    public delegate void NewChatRequestEvent(string senderNickname, string receiverNickname);
    public delegate void OpenAcceptedChatRequestEvent(string senderNickname, string receiverNickname);
    public delegate void CloseOtherUserChatPageEvent(string senderNickname, string receiverNickname);

}
