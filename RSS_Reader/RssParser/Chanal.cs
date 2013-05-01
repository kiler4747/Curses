using System.Collections.Generic;

namespace RssReader.RssParser
{
	internal class Chanal : Node
	{
		public string Source { get; set; }

		public List<Item> Items { get; set; }
	}
}