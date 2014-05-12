using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.UI.Helper
{
    public class ObservableSet<T> : ISet<T>, INotifyCollectionChanged
    {
        private readonly ISet<T> m_underlyingSet;

        public ObservableSet()
        {
            m_underlyingSet = new HashSet<T>();
        }

        #region ISet<T> Methods

        public bool Add(T item)
        {
            if (!m_underlyingSet.Add(item)) return false;
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            return true;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (var elem in other)
                Remove(elem);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            foreach (var notPresent in m_underlyingSet.Except(other).ToList())
                Remove(notPresent);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach (var elem in other)
                if (!Remove(elem))
                    Add(elem);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach (var elem in other) Add(elem);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return m_underlyingSet.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return m_underlyingSet.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return m_underlyingSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return m_underlyingSet.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return m_underlyingSet.SetEquals(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return m_underlyingSet.SetEquals(other);
        }

        #endregion

        #region ICollection<T> Methods

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            m_underlyingSet.Clear();
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return m_underlyingSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_underlyingSet.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_underlyingSet.Count; }
        }

        public bool IsReadOnly
        {
            get { return m_underlyingSet.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            if (!m_underlyingSet.Remove(item)) return false;
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return true;
        }

        #endregion

        #region IEnumerable<T> Methods

        public IEnumerator<T> GetEnumerator()
        {
            return m_underlyingSet.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Methods

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
