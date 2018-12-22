using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;

namespace ch22yaml.Data
{
    [Obsolete]
    public class ObservableList<T> : IList<T>, INotifyCollectionChanged
    {
        private IList<T> _List;

        public ObservableList()
        {
            _List = new List<T>();
        }

        public ObservableList(IEnumerable<T> list)
        {
            _List = new List<T>(list);
        }

        public T this[int index] {
            get => _List[index];
            set
            {
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, _List[index]);
                _List[index] = value;
                RaiseCollectionChanged(e);
            }
        }

        public int Count => _List.Count;

        public bool IsReadOnly => false;


        public void Add(T item)
        {
            _List.Add(item);
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _List);
            RaiseCollectionChanged(e);
        }

        public void Clear()
        {
            _List.Clear();
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, _List);
            RaiseCollectionChanged(e);
        }

        public bool Contains(T item)=> _List.Contains(item);
        

        public void CopyTo(T[] array, int arrayIndex)=> _List.CopyTo(array, arrayIndex);


        public IEnumerator<T> GetEnumerator() => _List.GetEnumerator();

        public int IndexOf(T item) => _List.IndexOf(item);

        public void Insert(int index, T item)
        {
            _List.Insert(index, item);
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _List);
            RaiseCollectionChanged(e);
        }

        public bool Remove(T item)
        {
            var result = _List.Remove(item);
            if (result) { 
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, _List);
                RaiseCollectionChanged(e);
            }
            return result;
        }

        public void RemoveAt(int index)
        {
            _List.RemoveAt(index);
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, _List);
            RaiseCollectionChanged(e);

        }

        IEnumerator IEnumerable.GetEnumerator() => _List.GetEnumerator();


        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)=>CollectionChanged?.Invoke(this, e);
        
    }
}
