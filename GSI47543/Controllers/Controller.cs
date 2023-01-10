using GSI47543.AD;
using GSI47543.Service;
using Microsoft.AspNetCore.Mvc;

namespace GSI47543.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller
    {
        [HttpPost]
        [Route("~/user/db/updateall")]
        public List<User> updateUsers()
        {
            return Service.Service.updateUsers();
        }

        [HttpGet]
        [Route("~/user/db/getusers")]
        public List<DTO.GetUserName> getUsers()
        {
            return Service.Service.getUsers();
        }

        [HttpGet]
        [Route("~/user/db/getusersou")]
        public List<DTO.GetUserName> getUsers(String ou)
        {
            return Service.Service.getUsers(ou);
        }

        [HttpGet]
        [Route("~/user/db/getusersbyaccountstatus")]
        public List<DTO.GetUserName> getUsersByAccountStatus(bool accountStatus)
        {
            return Service.Service.getUsersByAccountStatus(accountStatus);
        }

        [HttpGet]
        [Route("~/user/db/getuserswhosepasswordiswithinoneweektoexpire")]
        public List<DTO.GetUserName> getUsersWhosePasswordIsWithinOneWeekToExpire()
        {
            return Service.Service.getUsersWhosePasswordIsWithinOneWeekToExpire();
        }
    }

}
