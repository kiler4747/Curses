using System;
using System.Collections.Generic;
using System.Text;
using PluginInterface;

namespace Multiplicator
{
    public class Multiplication : IPlugin
    {
	    public double Operation(double a, double b)
	    {
		    return a * b;
	    }
    }
}
