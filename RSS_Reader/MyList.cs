using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace RSS_Reader
{
	[Serializable]
	class MyList<T> : List<T>, INotifyCollectionChanged
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public new void Add(T item)
		{
			base.Add(item);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
		}
	}
}
