using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Core
{
    public class UpdatePublicationOwner:IQueueCommand
    {
        public string ObjectId;
        public string Name;
    }
}
