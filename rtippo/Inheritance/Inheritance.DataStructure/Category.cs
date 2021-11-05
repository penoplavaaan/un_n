using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category: IComparable
    {
        private string type;
        public string Type { get { return this.type; } set { this.type = value; } }

        private MessageType messageType;
        public MessageType MessageType { get { return this.messageType; } set {this.messageType = value; } }

        private MessageTopic messageTopic;
        public MessageTopic MessageTopic { get { return this.messageTopic; } set { this.messageTopic = value; } }

        public Category(string type, MessageType messageType, MessageTopic messageTopic)
        {
            (this.type,this.messageType, this.messageTopic) = (type, messageType, messageTopic);
        }

        override public string ToString()
        {
            return this.type.ToString() + '.'+ this.messageType.ToString() + '.' + this.messageTopic.ToString();
        }

        public int CompareTo(Category a)
        {
            Console.WriteLine("Called on: " + this.ToString());
            Console.WriteLine("With parameter: " + a.ToString());

            if (this < a) return -1;
            if (this == a) return 0;
            return 1;
            
             
        }

        public bool CompareStrings(string firts, string second)
        {
            for (int i = 0; i < firts.Length; i++)
            {
                if (firts[i] > second[i]) return true;
            }
            return false;
        }

         


        public static bool operator >(Category a, Category b) {
            return true;
        }

        public static bool operator <(Category a, Category b) {
            return true;
        }

        public static bool operator ==(Category a, Category b)
        {
            return true;
        }

        public static bool operator !=(Category a, Category b)
        {
            return true;
        }

    }

    interface IComparable
    {
        int CompareTo(Category a);


    }
}
