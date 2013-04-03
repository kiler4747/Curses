using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS_Reader
{
	class Chanal : RSSNode
	{
		private List<Item> items = new List<Item>();

		public void AddItem(Item item)
		{
			items.Add(item);
		}
	}
}
