using System;
using System.Collections.Generic;
using NetworkLibrary;

namespace NetworkApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Network myNetwork = new Network();
            bool isMainMenuWorking = true;
            while (isMainMenuWorking)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n1.Create account\t3.View registered users\n" +
                                    "2.Log into account\t0.Exit");
                Console.Write("\nEnter the command number: ");
                try
                {
                    int numberOfCommand = Convert.ToInt32(Console.ReadLine());
                    if (numberOfCommand == 1 || numberOfCommand == 2 || numberOfCommand == 3 || numberOfCommand == 0)
                    {
                        Console.WriteLine();
                        switch (numberOfCommand)
                        {
                            case 1:
                                CreateAccount(myNetwork);
                                break;
                            case 2:
                                LogIntoAccount(myNetwork);
                                break;
                            case 3:
                                WriteAllUsers(myNetwork);
                                break;
                            case 0:
                                isMainMenuWorking = false;
                                break;
                        }
                    }
                    else throw new ArgumentException("Wrong command number");
                }
                catch (NonExistenceException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void CreateAccount(Network myNetwork)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your password: ");
            string pass = Console.ReadLine();
            myNetwork.CreateAcc(name, age, pass);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRegistration successful");
        }
        private static void WriteAllUsers(Network myNetwork)
        {
            if (myNetwork.Users.Count != 0)
            {
                Console.WriteLine($"{"Name:",-15} {"Age:",-3}");
                foreach (User user in myNetwork.Users)
                    Console.WriteLine(user.ToString());
            }
            else throw new NonExistenceException("No registered users");
        }
        private static void LogIntoAccount(Network myNetwork)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your password: ");
            string pass = Console.ReadLine();
            User user = myNetwork.FindUser(name, pass);
            if (user != null)
                UserMenu(myNetwork, user);
            else throw new ArgumentException("Incorrect name or password");
        }
        private static void UserMenu(Network myNetwork, User user)
        {
            bool isUserMenuWorking = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWelcome, " + user.Name);
            while (isUserMenuWorking)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1.Send friend Request\t\t3.View Friends\n" +
                                    "2.Check friend requests\t\t4.Delete Friend\n\t\t" +
                                              "0.Exit to Main Menu");
                Console.Write("\nEnter the command number: ");
                try
                {
                    int numberOfCommand = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (numberOfCommand == 1 || numberOfCommand == 2 || numberOfCommand == 3 || numberOfCommand == 4 || numberOfCommand == 0)
                    {
                        switch (numberOfCommand)
                        {
                            case 1:
                                SendRequest(myNetwork, user);
                                break;
                            case 2:
                                CheckRequest(myNetwork, user);
                                break;
                            case 3:
                                ViewFrends(user);
                                break;
                            case 4:
                                DeleteFriend(myNetwork, user);
                                break;
                            case 0:
                                isUserMenuWorking = false;
                                break;
                        }
                    }
                    else throw new ArgumentException("Wrong command number");
                }
                catch (UserException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (NonExistenceException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void WriteNames(List<string> names)
        {
            Console.WriteLine($"{"Name:",-15}");
            foreach (string name in names)
            {
                Console.WriteLine($"{name,-15}");
            }
        }
        static void ViewFrends(User currentUser)
        {
            if (currentUser.Friends.Count != 0)
                WriteNames(currentUser.Friends);
            else throw new NonExistenceException("You don't have friends yet");
        }
        static void WriteUsers(Network myNetwork, User currentUser)
        {
            Console.WriteLine($"{"Name:",-15} {"Age:",-3}");
            foreach (User user in myNetwork.Users)
            {
                if (user.Name != currentUser.Name && !currentUser.Friends.Exists(x => x == user.Name))
                    Console.WriteLine(user.ToString());
            }
        }
        static void SendRequest(Network myNetwork, User currentUser)
        {
            if ((currentUser.Friends.Count + 1) < myNetwork.Users.Count)
            {
                WriteUsers(myNetwork, currentUser);
                Console.Write("\nEnter the name to send request: ");
                string nameFriend = Console.ReadLine();
                myNetwork.SendRequest(currentUser, nameFriend);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRequest has been sent");
            }
            else throw new NonExistenceException("No one to send a request");
        }
        static void CheckRequest(Network myNetwork, User currentUser)   //*************
        {
            if (currentUser.Requests.Count != 0)
            {
                Console.WriteLine("Want(s) to add you as a friend:");
                WriteNames(currentUser.Requests);
                Console.Write("\nDo you want to add a friend? (yes/no)\t");
                string choice = Console.ReadLine();
                if (choice == "yes")
                {
                    Console.Write("\nEnter the name of future friend: ");
                    string friendName = Console.ReadLine();
                    myNetwork.RealAddFriend(currentUser, friendName);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nFriend added");
                }
                else if (choice == "no")
                {
                    Console.Write("\nEnter the name to reject the request: ");
                    string friendName = Console.ReadLine();
                    myNetwork.RejectFriend(currentUser, friendName);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nRequest rejected");
                }
                else throw new ArgumentException("Wrong input");
            }
            else throw new NonExistenceException("You have no friend requests");
        }
        static void DeleteFriend(Network myNetwork, User currentUser)
        {
            if (currentUser.Friends.Count != 0)
            {
                WriteNames(currentUser.Friends);
                Console.Write("\nEnter the name to delete: ");
                string name = Console.ReadLine();
                myNetwork.RealDeleteFriend(currentUser, name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFriend deleted");
            }
            else throw new NonExistenceException("You have no friends to delete");
        }
    }
}
