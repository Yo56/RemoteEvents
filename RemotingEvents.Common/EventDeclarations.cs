﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingEvents.Common
{

    public delegate void OnlineUsersChangedEvent(Dictionary<String, String> onlineUsers);
    public delegate void NewChatRequestEvent(string senderNickname, string receiverNickname);

}
