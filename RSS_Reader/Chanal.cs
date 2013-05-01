using System.Data.Objects.DataClasses;
using System.Linq;

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