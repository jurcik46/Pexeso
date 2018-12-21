using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library.Models;
using Pexeso.Server.Models;

namespace Pexeso.Server
{
    public class Entrance
    {

        public bool Registration(string userName, string password)
        {
            using (var PexesoContext = new PexesoContext())
            {
                try
                {
                    Console.WriteLine($@"Registration user: {userName}");

                    if (PexesoContext.CheckIfExistThisUser(userName))
                    {
                        Console.WriteLine($@"Dane meno je uz obsadene: {userName}");
                        return false;
                    }
                    PexesoContext.SaveUser(new Users() { UserName = userName, Password = password });
                }
                catch (Exception e)
                {
                    Console.WriteLine($@"Registration Failed User: {userName}  Error: {e.Message}");
                    return false;
                }
            }
            return true;
        }

        public User LogIn(string userName, string password)
        {
            using (var PexesoContext = new PexesoContext())
            {
                try
                {
                    Console.WriteLine($@"Login User: {userName}");

                    var a = PexesoContext.GetUserByName(userName);
                    if (a == null)
                    {
                        Console.WriteLine($@"User: {userName} neexistuje");
                        return null;
                    }

                    if (a.Password == password)
                    {
                        return new User() { UserName = a.UserName, Id = a.Id };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($@"Login Failed User: {userName}  Error: {e.Message}");
                    return null;
                }
            }

            return null;
        }
    }
}
