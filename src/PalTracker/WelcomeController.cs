using System;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {
        public readonly WelcomeMessage _message;

        [HttpGet]
        public string SayHello() => _message.Message;

        public WelcomeController(WelcomeMessage message)
        {
            _message = message;
        }
    }

    
}
