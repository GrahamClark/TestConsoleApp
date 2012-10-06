using System;
using System.Collections.Generic;

namespace TestConsoleApp.EnumFlags
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            User user1, user2, user3, user4;

            user1 = new User() { Name = "User 1", Option = Option.ShowEForm | Option.HideRefine };
            user2 = new User() { Name = "User 2", Option = Option.HideRefine };
            user3 = new User() { Name = "User 3", Option = (user1.Option & user2.Option) };
            user4 = new User() { Name = "User 4", Option = (Option)257 };

            if ((user2.Option & Option.HideRefine) == Option.HideRefine)
            {
                user3.Option |= Option.HideRefine;
            }

            List<User> users = new List<User>();
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);

            foreach (User u in users)
            {
                if (u.Option == Option.None)
                {
                    Console.WriteLine("{0} has None", u.Name);
                }
                
                if ((u.Option & Option.HideRefine) == Option.HideRefine)
                {
                    Console.WriteLine("{0} has HideRefine", u.Name);
                }
                
                if ((u.Option & Option.ShowExtranet) == Option.ShowExtranet)
                {
                    Console.WriteLine("{0} has ShowExtranet", u.Name);
                }
                
                if ((u.Option & Option.ShowEForm) == Option.ShowEForm)
                {
                    Console.WriteLine("{0} has ShowEForm", u.Name);
                }

                Console.WriteLine();
            }
        }
    }
}
