namespace DTO
{
    public static class ClassMapper
    {
        // public static K generic(T obj, K dto)
        // {
        //     MapperConfiguration config = new MapperConfiguration(conf => conf.CreateMap(typeof(T), typeof(K)));
        //     var mapper = new Mapper(config);
        // 
        //     return mapper.Map<K>(obj);
        // }

        public static UserDTO convertForDTO(GSI47543.DB.User user)
        {
            UserDTO mappedUser = new UserDTO();
            mappedUser.ID = user.ID;
            mappedUser.displayName = user.displayName;
            mappedUser.ou = user.ou;
            mappedUser.accountActive = user.accountActive;
            mappedUser.userAccountControl = user.userAccountControl;
            mappedUser.logonCount = user.logonCount;
            mappedUser.pwdExpires = user.pwdExpires.HasValue ? user.pwdExpires.Value.ToString("dd-MM-yyyy HH:mm:ss"): null;
            mappedUser.whenCreated = user.whenCreated.HasValue ? user.whenCreated.Value.ToString("dd-MM-yyyy HH:mm:ss") : null;
            mappedUser.pwdLastSet = user.pwdLastSet.HasValue ? user.pwdLastSet.Value.ToString("dd-MM-yyyy HH:mm:ss") : null;
            return mappedUser;
        }

        public static GSI47543.DB.User convertForUpdate(GSI47543.AD.User user)
        {
            GSI47543.DB.User use = new GSI47543.DB.User();

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

            return use;
        }
    }
}
