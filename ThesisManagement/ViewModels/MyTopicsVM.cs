using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisManagement.Models;

namespace ThesisManagement.ViewModels
{
    internal class MyTopicsVM
    {
        public IEnumerable<Topic> waitingTopics;
        public IEnumerable<Topic> approveledTopics;
        public IEnumerable<Topic> rejectedTopics;

    }
}
