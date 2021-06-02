using System;
using System.Collections.Generic;

namespace NetworkLibrary
{
    public class User
    {
        private string _name;
        private int _age;
        private string _password;
        private List<string> _friends;
        private List<string> _requests;
        public User(string name, int age, string password)
        {
            _name = name;
            _age = age;
            _password = password;
            _friends = new List<string>();
            _requests = new List<string>();
        }
        public string Name => _name;
        public int Age => _age;
        public string Pass => _password;

        public List<string> Friends => new List<string>(_friends);
        public List<string> Requests => new List<string>(_requests);
        public override string ToString()
        {
            return $"{Name,-15} {Age,-3}";
        }
        public void AddFriend(string name)
        {
            _friends.Add(name);
        }
        public void DeleteFriend(string name)
        {
            _friends.Remove(name);
        }
        public void AddRequest(string name)
        {
            _requests.Add(name);
        }
        public void DeleteRequest(string name)
        {
            _requests.Remove(name);
        }

    }
}
