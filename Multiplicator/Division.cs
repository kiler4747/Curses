using PluginInterface;

namespace Multiplicator
{
	class Division : IPlugin
	{
		public double Operation(double a, double b)
		{
			return a / b;
		}
	}
}
