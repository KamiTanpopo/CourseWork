using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class Network
    {
        private List<User> _users;

        public Network()
        {
            _users = new List<User>();
        }
        public List<User> Users => new List<User>(_users);
        public void CreateAcc(string name, int age, string pass)
        {
            if (age < 16)
                throw new ArgumentException("Too young to register");
            else if (age > 110)
                throw new ArgumentException("There is no such age");
            else
            {
                _users.Add(new User(name, age, pass));
            }
        }
        public User FindUser(string name, string pass)
        {
            return _users.Find(x => x.Name == name && x.Pass == pass);
        }
        public bool ExistUser(string name)
        {
            if (_users.Exists(x => x.Name == name))
            {
                return true;
            }
            return false;
        }
        public User FindUserByName(string name)
        {
            return _users.Find(x => x.Name == name);
        }
        public void SendRequest(User currentUser, string nameFriend)
        {
            if (ExistUser(nameFriend))
            {
                FindUserByName(nameFriend).AddRequest(currentUser.Name);
            }
            else
                throw new UserException();
        }
        public void RealAddFriend(User currentUser, string nameFriend)
        {
            if (ExistUser(nameFriend))
            {
                FindUserByName(nameFriend).AddFriend(currentUser.Name);
                currentUser.AddFriend(nameFriend);
                currentUser.DeleteRequest(nameFriend);
            }
            else
                throw new UserException();
        }
        public void RejectFriend(User currentUser, string nameFriend)
        {
            if (ExistUser(nameFriend))
                currentUser.DeleteRequest(nameFriend);
            else
                throw new UserException();
        }
        public void RealDeleteFriend(User currentUser, string nameFriend)
        {
            if (ExistUser(nameFriend))
            {
                currentUser.DeleteFriend(nameFriend);
                FindUserByName(nameFriend).DeleteFriend(currentUser.Name);
            }
            else
                throw new UserException();
        }
    }
}
