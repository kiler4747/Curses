﻿using System;
using System.Collections.Generic;
using System.Text;
using PluginInterface;

namespace Summator
{
    public class Addition : IPlugin
    {
	    public double Operation(double a, double b)
	    {
		    return a + b;
	    }
    }
}