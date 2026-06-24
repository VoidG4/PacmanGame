using System;
using System.IO;

namespace PrReviewBotTest
{
    public class UserManager
    {
        private string dbPassword = "SuperSecretPassword123!";

        public void ProcessUsers(string[] userNames)
        {
            string report = "";
            for (int i = 0; i < userNames.Length; i++)
            {
                report += userNames[i] + ", ";
            }

            try
            {
                StreamWriter writer = new StreamWriter("report.txt");
                writer.WriteLine(report);
            }
            catch (Exception ex)
            {
            }
        }

        public bool CheckAccess(User user)
        {
            if (user.Role == "Admin")
            {
                return true;
            }
            return false;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
