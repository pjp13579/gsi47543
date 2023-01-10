using AutoMapper;
using GSI47543.AD;
using GSI47543.DB;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace GSI47543.Service
{
    public class Service
    {
        public static List<AD.User> updateUsers()
        {
            List<AD.User> users = ADManager.GetUsers();

            using (var db = new DataContext())
            {
                // delete every record and commit the changes in database
                db.Database.ExecuteSqlRaw("TRUNCATE TABLE users");
                db.SaveChanges();

                foreach (AD.User user in users)
                {
                    DB.User use = new DB.User();                    

                    use.ID = user.objectGUID.ToString();
                    use.pwdLastSet = user.pwdLastSet;
                    use.logonCount = user.logonCount;
                    use.displayName = user.displayName;

                    // distinguishedName is composed of several fields, we only want the OU
                    string ou = user.distinguishedName.Split(",").ToList().FirstOrDefault(x => x.StartsWith("OU="));
                    //some users do not belong to OU, if OU are on the 2º field. If º field is not OU, user does not belong to OU, therefore not adding to database(db saves null value for OU)
                    use.ou = string.IsNullOrEmpty(ou) ? null : ou.Split("=")[1];
                    
                    use.whenCreated = user.whenCreated;
                    use.pwdExpires = user.pwdExpires;
                    use.userAccountControl = user.userAccountControl;
                    
                    // check if flag 0x0002 (account disabled) is in userAccountControl
                    if (Convert.ToBoolean(user.userAccountControl & 0x0002))
                        use.accountActive = false;
                    else
                        use.accountActive = true;
                    
                    // add to db
                    db.Add(use);
                    //commit change
                    db.SaveChanges();
                }
            }

                return users;
        }

        public static List<DTO.GetUserName> getUsers()
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.GetUserName)));
            var mapper = new Mapper(config);

            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.ToList();
                List<DTO.GetUserName> usersDto = new List<DTO.GetUserName>();
                DTO.GetUserName userDto = new DTO.GetUserName();

                foreach (DB.User user in users)
                {
                    usersDto.Add(mapper.Map<DTO.GetUserName>(user));
                }
                return usersDto;
            }
        }

        public static List<DTO.GetUserName> getUsers(String ou)
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.GetUserName)));
            var mapper = new Mapper(config);

            using(var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.ou.Equals(ou)).ToList();
                List<DTO.GetUserName> usersDto = new List<DTO.GetUserName>();
                DTO.GetUserName userDto = new DTO.GetUserName();

                foreach ( DB.User user in users)
                {
                    usersDto.Add(mapper.Map<DTO.GetUserName>(user));
                }
                return usersDto;
            }
        }

        // difference between bool and Boolean / string and String?????

        public static List<DTO.GetUserName> getUsersByAccountStatus(bool accountStatus)
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.GetUserName)));
            var mapper = new Mapper(config);

            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.accountActive.Equals(accountStatus)).ToList();
                List<DTO.GetUserName> usersDto = new List<DTO.GetUserName>();
                DTO.GetUserName userDto = new DTO.GetUserName();

                foreach (DB.User user in users)
                {
                    usersDto.Add(mapper.Map<DTO.GetUserName>(user));
                }
                return usersDto;
            }
        }

        public static List<DTO.GetUserName> getUsersWhosePasswordIsWithinOneWeekToExpire()
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.GetUserName)));
            var mapper = new Mapper(config);

            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.pwdExpires <= (DateTime.Now.AddDays(7))).ToList();
                List<DTO.GetUserName> usersDto = new List<DTO.GetUserName>();
                DTO.GetUserName userDto = new DTO.GetUserName();

                foreach (DB.User user in users)
                {
                    usersDto.Add(mapper.Map<DTO.GetUserName>(user));
                }
                return usersDto;
            }
        }

    }
}