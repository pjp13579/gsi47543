using GSI47543.AD;
using GSI47543.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GSI47543.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller
    {
        /// <summary>
        /// Update database with Active Directory user records
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("~/user/db/updateall")]
        [Produces("application/json")]
        public List<User> updateUsers()
        {
            return Service.Service.updateUsers();
        }

        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/user/db/getusers")]
        [Produces("application/json")]
        public List<DTO.UserDTO> getUsers()
        {
            return Service.Service.getUsers();
        }

        /// <summary>
        /// List all user for the specified organization
        /// </summary>
        /// <param name="ou"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/user/db/getusersou")]
        [Produces("application/json")]
        public List<DTO.UserDTO> getUsers(String ou)
        {
            return Service.Service.getUsers(ou);
        }

        /// <summary>
        /// List users for the specified account status
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/user/db/getusersbyaccountstatus")]
        [Produces("application/json")]
        public List<DTO.UserDTO> getUsersByAccountStatus([Required][DefaultValue(false)] bool accountStatus)
        {
            return Service.Service.getUsersByAccountStatus(accountStatus);
        }

        /// <summary>
        /// List users whose passowrd expire withing one week
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/user/db/getuserswhosepasswordiswithinoneweektoexpire")]
        [Produces("application/json")]
        public List<DTO.UserDTO> getUsersWhosePasswordIsWithinOneWeekToExpire()
        {
            return Service.Service.getUsersWhosePasswordIsWithinOneWeekToExpire();
        }
    }

}
