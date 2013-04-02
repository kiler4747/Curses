namespace PluginInterface
{
	public interface IPlugin
	{
		double Operation(double a, double b);
		string Symbol { get; }
	}
}
