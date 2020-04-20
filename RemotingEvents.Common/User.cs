using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDIN_PROJ1.Common
{
    [Serializable]
    public class User
    {
        //attributes
        private string name;
        private string nickname;
        private string password;


        //Constructor
        public User(string name, string nickname, string password)
        {
            this.name = name;
            this.nickname = nickname;
            this.password = password;
        }

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ToString()
        {
            return "Name = " + Name + ", Nickname = " + Nickname + ", Password = " + Password;
        }
    }
}
