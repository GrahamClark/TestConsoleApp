using System;
using System.Collections.Generic;
using System.Linq;

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

            foreach (Option option in GetFlags(user1.Option))
            {
                Console.WriteLine((int)option);
            }

            if ((user2.Option & Option.HideRefine) == Option.HideRefine)
            {
                user3.Option |= Option.HideRefine;
            }

            var users = new List<User> { user1, user2, user3, user4 };

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

                if (u.Option.HasFlag(Option.ShowEForm))
                {
                    Console.WriteLine("{0} has ShowEForm via HasFlag", u.Name);
                }

                Console.WriteLine();
            }
        }

        private static IEnumerable<Enum> GetFlags(Enum input)
        {
            var flags = Enum.GetValues(input.GetType()).Cast<Enum>().Where(value => input.HasFlag(value) && Convert.ToInt32(value) != 0);

            return flags;
        }
    }
}
