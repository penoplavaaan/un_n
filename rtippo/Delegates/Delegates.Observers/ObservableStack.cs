using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
    public class StackOperationsLogger
    {
        private readonly Observer observer = new Observer();
        public void SubscribeOn<T>(ObservableStack<T> stack)
        {
            stack.ObjectAdded += (sender, data) => observer.HandleEvent(data.ToString());
            stack.ObjectRemoved += (sender, data) => observer.HandleEvent(data.ToString());
        }

        public string GetLog()
        {
            return observer.Log.ToString();
        }
    }

    public interface IObserver
    {
        void HandleEvent(string message);
    }

    public class Observer : IObserver
    {
        public StringBuilder Log = new StringBuilder();

        public void HandleEvent(string message)
        {
            Log.Append(message);
        }
    }


    public class ObservableStack<TStack>
    {
        public event EventHandler<StackEventData<TStack>> ObjectAdded;
        public event EventHandler<StackEventData<TStack>> ObjectRemoved;

        List<TStack> observers = new List<TStack>();

        public void Add(TStack observer)
        {
            observers.Add(observer);
            ObjectAdded?.Invoke(this, new StackEventData<TStack> { IsPushed = true, Value = observer });
        }

        public void Remove(TStack observer)
        {
            observers.Remove(observer);
            ObjectRemoved?.Invoke(this, new StackEventData<TStack> { IsPushed = false, Value = observer });
        }

        List<TStack> data = new List<TStack>();

        public void Push(TStack stackEl)
        {
            data.Add(stackEl);
            Add(stackEl);
        }

        public TStack Pop()
        {
            if (data.Count.Equals(0))
                throw new InvalidOperationException();
            int removeindex = data.Count - 1;
            TStack result = data[removeindex];
            Remove(data[removeindex]);
            return result;
        }
    }
}