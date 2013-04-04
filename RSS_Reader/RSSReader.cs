using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS_Reader
{
	[Serializable]
	class RSSReader
	{
		private List<Chanal> chanals = new List<Chanal>();

		public List<Chanal> Chanals
		{
			get { return chanals; }
			set { chanals = value; }
		}
	}
}
