using AutoMapper;
using DTO;
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
                    DB.User use = ClassMapper.convertForUpdate(user);                
                    db.Add(use);
                    db.SaveChanges();
                }
            }

                return users;
        }

        public static List<DTO.UserDTO> getUsers()
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.UserDTO)));
            var mapper = new Mapper(config);
            
            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.ToList();
                List<DTO.UserDTO> usersDto = new List<DTO.UserDTO>();
                DTO.UserDTO userDto = new DTO.UserDTO();

                
                foreach (DB.User user in users)
                {
                    usersDto.Add(ClassMapper.convertForDTO(user));
                }
                return usersDto;
            }
        }

        public static List<DTO.UserDTO> getUsers(String ou)
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.UserDTO)));
            var mapper = new Mapper(config);

            using(var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.ou.Equals(ou)).ToList();
                List<DTO.UserDTO> usersDto = new List<DTO.UserDTO>();
                DTO.UserDTO userDto = new DTO.UserDTO();

                foreach ( DB.User user in users)
                {
                    usersDto.Add(ClassMapper.convertForDTO(user));
                }
                return usersDto;
            }
        }

        // difference between bool and Boolean / string and String?????

        public static List<DTO.UserDTO> getUsersByAccountStatus(bool accountStatus)
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.UserDTO)));
            var mapper = new Mapper(config);

            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.accountActive.Equals(accountStatus)).ToList();
                List<DTO.UserDTO> usersDto = new List<DTO.UserDTO>();
                DTO.UserDTO userDto = new DTO.UserDTO();

                foreach (DB.User user in users)
                {
                    usersDto.Add(ClassMapper.convertForDTO(user));
                }
                return usersDto;
            }
        }

        public static List<DTO.UserDTO> getUsersWhosePasswordIsWithinOneWeekToExpire()
        {
            MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(DB.User), typeof(DTO.UserDTO)));
            var mapper = new Mapper(config);

            using (var db = new DataContext())
            {
                List<DB.User> users = db.users.Where(u => u.pwdExpires <= (DateTime.Now.AddDays(7))).ToList();
                List<DTO.UserDTO> usersDto = new List<DTO.UserDTO>();
                DTO.UserDTO userDto = new DTO.UserDTO();

                foreach (DB.User user in users)
                {
                    usersDto.Add(ClassMapper.convertForDTO(user));
                }
                return usersDto;
            }
        }

    }
}