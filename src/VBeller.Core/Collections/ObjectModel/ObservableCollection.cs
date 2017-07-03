using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace VBeller.Collections.ObjectModel
{
    /// <summary>
    /// Provides an extended version of the <see cref="System.Collections.ObjectModel.ObservableCollection{T}"/> class with support to
    /// add and remove item ranges without firing events for each of them.
    /// </summary>
    /// <typeparam name="T">The item type, the collection holds.</typeparam>
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        /// <summary>
        /// Adds a range of <paramref name="items"/> to the <see cref="ObservableCollection{T}"/> but only raises <see cref="ObservableCollection{T}.CollectionChanged"/> once.
        /// </summary>
        /// <param name="items">The items that will be added to the <see cref="ObservableCollection{T}"/>.</param>
        public void AddRange(IEnumerable<T> items)
        {
            CheckReentrancy();
            foreach (var item in items)
                Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        }

        /// <summary>
        /// Removes a range of <paramref name="items"/> from the <see cref="ObservableCollection{T}"/> but only raises <see cref="ObservableCollection{T}.CollectionChanged"/> once.
        /// </summary>
        /// <param name="items">The items that will be removed from the <see cref="ObservableCollection{T}"/>s</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            CheckReentrancy();
            foreach (var item in items)
                Items.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        }

        /// <summary>
        /// Raises the <see cref="ObservableCollection{T}.CollectionChanged"/> event with the given <see cref="NotifyCollectionChangedEventArgs"/>./>
        /// </summary>
        /// <param name="e">The arguments passed to the <see cref="ObservableCollection{T}.CollectionChanged"/> event.</param>
        public void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            OnCollectionChanged(e);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ObservableCollection{T}"/> class with the given items.
        /// </summary>
        /// <param name="collection">The initial collection of items.</param>
        public ObservableCollection(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Creates an emtpy instance of the <see cref="ObservableCollection{T}"/> class.
        /// </summary>
        public ObservableCollection() { }
    }
}