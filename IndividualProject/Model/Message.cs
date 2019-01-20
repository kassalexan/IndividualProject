using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string MessageData { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
    }
}
