using System;

namespace nodechain {
    [Serializable]
    public class CircleEntryText : CircleEntry {
        public const short MAX_LENGHT = 512;

        public String Text { get { return (string)Value; } set { Value = value; } }
        public int Lenght { get; set; }

        public CircleEntryText(Guid creationUser, DateTime date, string text) 
            : base(creationUser, date, CircleEntryType.Text, text)  {  }
    }
}