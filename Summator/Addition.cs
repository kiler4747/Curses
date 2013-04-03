using PluginInterface;

namespace Summator
{
	public class Addition : IPlugin
	{
		public double Operation(double a, double b)
		{
			return a + b;
		}

		public string Symbol { get { return "+"; } }
	}
}
