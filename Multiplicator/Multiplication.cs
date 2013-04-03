using PluginInterface;

namespace Multiplicator
{
	public class Multiplication : IPlugin
	{
		public double Operation(double a, double b)
		{
			return a * b;
		}

		public string Symbol { get { return "*"; } }
	}
}
