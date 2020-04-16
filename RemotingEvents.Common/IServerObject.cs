using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingEvents.Common
{
    public interface IServerObject
    {

        #region Events

        event OnlineUsersChangedEvent OnlineUsersChanged;

        #endregion

        #region Methods
        /////////////////////////////// METHOD PROTOTYPES /////////////////////////////
        string HelloWorld();
        Boolean DoesThisUserExist(String nickname);
        User GetUser(String nickname);
        string GetRealNameFromUser(String username);
        Boolean Authentication(String nickname, String hashedPasswordInput, String ipAddress);
        void Registration(User user, String ipAddress);
        void LogOut(string username);
        void addUserToOnlineUsers(String username, String ipAddress);
        string GetAddressFromOnlineUser(String username);
        int AllocatePort(String nickname);
        int GetFreePort(String nickname);
        bool IsPortFree(String userAddress, int port);
        Dictionary<string, string> getOnlineUsers();

        ////////////////////////////////////////////////////////////////////////////////////////
        #endregion

    }
}
