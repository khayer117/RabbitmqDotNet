using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitmqDotNetCore.Infrastructure;

namespace RabbitmqDotNetCore.Features
{
    public class UpdatePublicationOwnerCommand:IQueueCommand
    {
        public string ObjectId;
        public string Name;
    }
}
