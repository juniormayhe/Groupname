// *** Updated 5/28/2017 3:41 PM
using System;
using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices.AccountManagement;

namespace groupname
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalGroups = 0;
            try
            {
                
                bool hasArgs = args != null && args.Length > 0;
                if (hasArgs)
                    int.TryParse(args[0], out totalGroups); 

                if (!hasArgs || args[0] == "/?") {
                    System.Console.WriteLine("GROUPNAME   Copyright (c) 2016 Wanderley Mayhe Junior\n\n" +
                        "Displays a list of current computer user groups.\n");
                    System.Console.WriteLine(@"Usage: GROUPNAME [/? | amount]

  amount       Specifies the amount of group items to be shown.
  /?           Shows this help.");
                    Environment.Exit(0);
                    return;
                }

                var groups = UserPrincipal.Current.GetGroups();
                IEnumerable<string> groupNames = groups.Select(x => x.SamAccountName);

                int i = 1;
                foreach (string g in groupNames)
                {
                    System.Console.Write(g);
                    
                    if (hasArgs && i >= totalGroups)
                        break;
                    i++;
                    System.Console.WriteLine();
                }
            }
            catch (PrincipalServerDownException psdex)
            {
                System.Console.Write("Error: Active Directory was not found. " + psdex.Message);
                Environment.Exit(-2);
            }
            catch (Exception ex) {
                System.Console.Write("Error: " + ex.Message);
                Environment.Exit(-1);
            }

            
        }
    }
}
