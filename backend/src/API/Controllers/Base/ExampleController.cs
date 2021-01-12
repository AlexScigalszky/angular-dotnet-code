using Application.Models.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers.Base
{
    public class ExampleController : ControllerBase
    {
        protected User CurrentUser()
        {
            return (User)HttpContext.Items["User"];
        }

        protected UserAgentModel UserAgentData()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            string uaString = Convert.ToString(userAgent[0]);
            return new UserAgentModel()
            {
                Data = uaString
            };

        }
    }
}
