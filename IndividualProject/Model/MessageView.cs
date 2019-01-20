using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class MessageView
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string MessageData { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
    }
}
