using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return ClipboardModel.CurrentValue;
        }

        // PUT api/values/string
        [HttpPut("{value}")]
        public void Put(string value)
        {
            if (value != null)
            {
                lock (this)
                {
                    ClipboardModel.CurrentValue = value;
                }
            }
        }
    }
}
