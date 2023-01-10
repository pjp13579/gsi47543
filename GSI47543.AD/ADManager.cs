using System.Data.SqlTypes;
using System.DirectoryServices;

namespace GSI47543.AD
{
    public class ADManager
    {
        public static List<User> GetUsers()
        {
            using (var root = new System.DirectoryServices.DirectoryEntry($"LDAP://192.168.50.240"))
            {
                root.Username = "Administrator";
                root.Password = "Tpsitpsi1";

                using (var searcher = new DirectorySearcher(root))
                {
                    searcher.Filter = $"(&(objectCategory=person)(objectClass=user))";

                    //searcher.PropertiesToLoad.Add("memberOf");

                    SearchResultCollection results = searcher.FindAll();

                    List<User> users = new List<User>();

                    foreach (SearchResult result in results)
                    {
                        User user = new User();                        
                        user.objectGUID = new Guid((byte[])result.Properties["objectGUID"][0]).ToString();
                        user.displayName = result.Properties["name"][0].ToString();
                        user.distinguishedName = result.Properties["distinguishedName"][0].ToString();
                        user.logonCount = (int)result.Properties["logonCount"][0];
                        user.userAccountControl = (int)result.Properties["userAccountControl"][0];          
                        user.pwdLastSet = GetDate((long)result.Properties["pwdLastSet"][0]);
                        user.whenCreated = (DateTime?)result.Properties["whenCreated"][0];
                        user.pwdExpires = GetDate((long)result.Properties["accountExpires"][0]);
                        
                        users.Add(user);
                    }
                    return users;
                }
            }
        }

        private static DateTime? GetDate(long value)
        {
            try
            {
                DateTime dt = DateTime.FromFileTime(value);
                return dt < SqlDateTime.MinValue ? null : dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}