using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        private string type;
        public string Type { get { return this.type; } set { this.type = value; } }

        private MessageType messageType;
        public MessageType MessageType { get { return this.messageType; } set { this.messageType = value; } }

        private MessageTopic messageTopic;
        public MessageTopic MessageTopic { get { return this.messageTopic; } set { this.messageTopic = value; } }

        public Category(string type, MessageType messageType, MessageTopic messageTopic)
        {
            (this.type, this.messageType, this.messageTopic) = (type, messageType, messageTopic);
        }

        override public string ToString()
        {
            return this.type.ToString() + '.' + this.messageType.ToString() + '.' + this.messageTopic.ToString();
        }

        public int CompareTo(object obj)
        {
            try
            {
                Category a = obj as Category;
                // Сначала сравниваем по Message
                if (this.type.CompareTo(a.type) != 0) return this.type.CompareTo(a.type);

                if (this.messageType.CompareTo(a.messageType) != 0) return this.messageType.CompareTo(a.messageType);

                return this.messageTopic.CompareTo(a.messageTopic);
            }
            catch (NullReferenceException)
            {
                return -1;
            }

            /*
            if (this.type.CompareTo(a.type) != 0) return this.type.CompareTo(a.type);

            if (this.messageType.CompareTo(a.messageType) != 0) return this.messageType.CompareTo(a.messageType);

            return this.messageTopic.CompareTo(a.messageTopic);
            */
        }
        public override bool Equals(object obj)
        {
            return obj is Category && Equals(obj as Category); 
        }

        public bool Equals(Category a)
        {
            return this == a;
        }




        public static bool operator >(Category a, Category b)
        {
            return Math.Sign(a.CompareTo(b)) == 1;
        }

        public static bool operator <(Category a, Category b)
        {
            return Math.Sign(a.CompareTo(b)) == -1;
        }

        public static bool operator >=(Category a, Category b)
        {
            return a > b || a == b;
        }

        public static bool operator <=(Category a, Category b)
        {
            return a < b || a == b;
        }

        public static bool operator ==(Category a, Category b)
        {
            return Math.Sign(a.CompareTo(b)) == 0;
        }

        public static bool operator !=(Category a, Category b)
        {
            return Math.Sign(a.CompareTo(b)) != 0;
        }

    }

    interface IComparable
    {
        int CompareTo(object obj);
        bool Equals(object obj);

    }
}
