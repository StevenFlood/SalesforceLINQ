using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToSalesforce;
using static System.Console;

namespace SalesforceLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = "3MVG9uudbyLbNPZNpLG0jrtxsMfYa8hNL8TGBRUDe8TmeEA.lm2.tyiKDXX9wrkII9P549lFNMw0oNkTFyMa.";
            string clientsecret = "1509117947598357864";
            string username = "Steven.John.Flood@jci.com";
            string password = "Bill-Hall04";
            string securityToken = "qCyEragCilFTObvE4FybjRBz";

            var impersonationParam = new Rest.OAuth.ImpersonationParam(clientId, clientsecret, securityToken, username, password);
            var context = new SalesforceDataContext("jcibe.my", impersonationParam);

            var accounts = (from a in context.Accounts
                            where !a.Name.StartsWith("Company")
                            select a).Take(10);
            foreach (var account in accounts)
            {
                WriteLine($"Account {account.Name} Industry: {account.Industry}");
                foreach (var contact in account.Contacts) // lazy load contacts
                {
                    WriteLine($"contact: {contact.Name} - {contact.Phone} - {contact.LeadSource}");

                    foreach (var @case in contact.Cases) // lazy load contact's cases
                        WriteLine($"case: {@case.Id}");
                }
            }
        }
    }
}




