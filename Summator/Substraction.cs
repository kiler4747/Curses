using PluginInterface;

namespace Summator
{
	class Substraction : IPlugin
	{
		public double Operation(double a, double b)
		{
			return a - b;
		}

		public string Symbol { get { return "-"; } }
	}
}
