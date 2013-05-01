using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace RssReader
{
	partial class Chanal : EntityObject
	{
		public int NotReadedArticlsCount
		{
			get { return Articls.Count((x) => x.Readed == false); }
		}
	}
}
