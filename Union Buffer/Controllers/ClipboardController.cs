using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Union_Buffer;

namespace Server.Controllers
{
    public class ClipboardController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return ClipboardModel.currentClipboardValue;
        }

        [HttpGet]
        public void Set(string value)
        {
            lock (this)
            {
                ClipboardModel.currentClipboardValue = value;
            }
        }
    }
}