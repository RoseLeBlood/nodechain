using System;
using System.Text;
using ASF.Node.List;

namespace nodechain {
    [Serializable]
    public enum CircleEntryType {
        Text, //
        Image,
        Audio,
        Sensor, 
        Link,
        Video,
    }
    
    [Serializable]
    public class CircleEntry {
        public Guid CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public CircleEntryType Type { get; set; }
        public Object Value { get; set;}

        public ListNode<CircleEntry> Comments { get; set; }

        protected CircleEntry(Guid creationUser, DateTime date, CircleEntryType type, object data) {
            CreationUser = creationUser;
            CreationDate = date;
            Type = type;
            Value = data;
            Comments = new ListNode<CircleEntry>(String.Format("S:{0}:{1}", creationUser, date));
        }
        public bool addComment(Guid creationUser, DateTime date, CircleEntryType type, Object data) {
            CircleEntry entry = null;

            switch(type) {
                case CircleEntryType.Text: 
                    entry = new CircleEntryText(creationUser, date, (string)data); 
                    break;
                case CircleEntryType.Image: break;
                case CircleEntryType.Audio: break;
                case CircleEntryType.Sensor: break;
                case CircleEntryType.Link: break;
                case CircleEntryType.Video: break;
                default: break;
            }
            if(entry != null)
                Comments.setNode (new ListNode<CircleEntry> (String.Format("C:{0}:{1}",
                    creationUser, date), entry));

            return true;
        }
        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("[CircleEntry] {0}:{1}:{3}:{2}:{4}", CreationUser, CreationDate, Type,
                Comments.Count, Value);

            return builder.ToString();
        }
        public ListNode<CircleEntry> getComment(Guid user, DateTime date) {
            return Comments.getNode(String.Format("C:{0}:{1}", user, date));
        }
    }
}