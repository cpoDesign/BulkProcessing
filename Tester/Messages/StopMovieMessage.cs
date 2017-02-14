using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Tester.Messages
{
    public class StopMovieMessage
    {
        public StopMovieMessage(int userId)
        {
            this.UserId = userId;
        }
        public int UserId { get; private set; }
    }
}
