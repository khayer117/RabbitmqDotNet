using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqDotNetCore.Infrastructure
{
    public class UpdatePublicationOwnerCommand:IQueueCommand
    {
        public string ObjectId;
        public string Name;
    }
}
