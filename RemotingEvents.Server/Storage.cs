using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TDIN_PROJ1.Common;

namespace TDIN_PROJ1.Server
{
    class Storage
    {
        const string PATH = "../../registeredUsers.ser";


        //static method wich return the list of registered users saved on local storage.
        // File => Program
        public static Dictionary<String, User> LoadRegisteredUsersFromFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(PATH, FileMode.Open, FileAccess.Read);
            Dictionary<String, User> registeredUsers = (Dictionary<String, User>)formatter.Deserialize(stream);
            stream.Close();
            Console.WriteLine("Users loaded from " + PATH);
            return registeredUsers;
        }


        //static method wich save the list on local storage
        // Program => File
        public static void SaveRegisteredUsersToFile(Dictionary<String, User> users)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(PATH, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, users);
            stream.Close();
            Console.WriteLine("Users saved at " + PATH);
        }

        public static void DeleteAllUsers()
        {
            Console.WriteLine("User list cleared");
            SaveRegisteredUsersToFile(new Dictionary<String, User>());
            
        }
    }
}

