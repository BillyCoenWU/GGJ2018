using UnityEngine;

public static class Easings
{
	private const float PI = Mathf.PI;
	private const float HALFPI = PI / 2.0f;

	public enum Functions
	{
		Linear,

		QuadraticEaseIn,
		QuadraticEaseOut,
		QuadraticEaseInOut,

		CubicEaseIn,
		CubicEaseOut,
		CubicEaseInOut,

		QuarticEaseIn,
		QuarticEaseOut,
		QuarticEaseInOut,

		QuinticEaseIn,
		QuinticEaseOut,
		QuinticEaseInOut,

		SineEaseIn,
		SineEaseOut,
		SineEaseInOut,

		CircularEaseIn,
		CircularEaseOut,
		CircularEaseInOut,

		ExponentialEaseIn,
		ExponentialEaseOut,
		ExponentialEaseInOut,

		ElasticEaseIn,
		ElasticEaseOut,
		ElasticEaseInOut,

		BackEaseIn,
		BackEaseOut,
		BackEaseInOut,

		BounceEaseIn,
		BounceEaseOut,
		BounceEaseInOut
	}

	public static string[] sFunctions = new string[]
	{
		"Linear",
		"QuadraticEaseIn",
		"QuadraticEaseOut",
		"QuadraticEaseInOut",
		"CubicEaseIn",
		"CubicEaseOut",
		"CubicEaseInOut",
		"QuarticEaseIn",
		"QuarticEaseOut",
		"QuarticEaseInOut",
		"QuinticEaseIn",
		"QuinticEaseOut",
		"QuinticEaseInOut",
		"SineEaseIn",
		"SineEaseOut",
		"SineEaseInOut",
		"CircularEaseIn",
		"CircularEaseOut",
		"CircularEaseInOut",
		"ExponentialEaseIn",
		"ExponentialEaseOut",
		"ExponentialEaseInOut",
		"ElasticEaseIn",
		"ElasticEaseOut",
		"ElasticEaseInOut",
		"BackEaseIn",
		"BackEaseOut",
		"BackEaseInOut",
		"BounceEaseIn",
		"BounceEaseOut",
		"BounceEaseInOut"
	};

	public static float Interpolate (float t, Functions function)
	{
		switch(function)
		{
			default:
			case Functions.Linear: 					return Linear(t);
			case Functions.QuadraticEaseOut:		return QuadraticEaseOut(t);
			case Functions.QuadraticEaseIn:			return QuadraticEaseIn(t);
			case Functions.QuadraticEaseInOut:		return QuadraticEaseInOut(t);
			case Functions.CubicEaseIn:				return CubicEaseIn(t);
			case Functions.CubicEaseOut:			return CubicEaseOut(t);
			case Functions.CubicEaseInOut:			return CubicEaseInOut(t);
			case Functions.QuarticEaseIn:			return QuarticEaseIn(t);
			case Functions.QuarticEaseOut:			return QuarticEaseOut(t);
			case Functions.QuarticEaseInOut:		return QuarticEaseInOut(t);
			case Functions.QuinticEaseIn:			return QuinticEaseIn(t);
			case Functions.QuinticEaseOut:			return QuinticEaseOut(t);
			case Functions.QuinticEaseInOut:		return QuinticEaseInOut(t);
			case Functions.SineEaseIn:				return SineEaseIn(t);
			case Functions.SineEaseOut:				return SineEaseOut(t);
			case Functions.SineEaseInOut:			return SineEaseInOut(t);
			case Functions.CircularEaseIn:			return CircularEaseIn(t);
			case Functions.CircularEaseOut:			return CircularEaseOut(t);
			case Functions.CircularEaseInOut:		return CircularEaseInOut(t);
			case Functions.ExponentialEaseIn:		return ExponentialEaseIn(t);
			case Functions.ExponentialEaseOut:		return ExponentialEaseOut(t);
			case Functions.ExponentialEaseInOut:	return ExponentialEaseInOut(t);
			case Functions.ElasticEaseIn:			return ElasticEaseIn(t);
			case Functions.ElasticEaseOut:			return ElasticEaseOut(t);
			case Functions.ElasticEaseInOut:		return ElasticEaseInOut(t);
			case Functions.BackEaseIn:				return BackEaseIn(t);
			case Functions.BackEaseOut:				return BackEaseOut(t);
			case Functions.BackEaseInOut:			return BackEaseInOut(t);
			case Functions.BounceEaseIn:			return BounceEaseIn(t);
			case Functions.BounceEaseOut:			return BounceEaseOut(t);
			case Functions.BounceEaseInOut:			return BounceEaseInOut(t);
		}
	}

	public static float Linear (float t)
	{
		return t;
	}

	public static float QuadraticEaseIn (float t)
	{
		return t * t;
	}

	public static float QuadraticEaseOut (float t)
	{
		return -(t * (t - 2.0f));
	}
	
	public static float QuadraticEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			return 2.0f * t * t;
		}
		else
		{
			return (-2.0f * t * t) + (4.0f * t) - 1.0f;
		}
	}
	
	public static float CubicEaseIn (float t)
	{
		return t * t * t;
	}
	
	public static float CubicEaseOut (float t)
	{
		float f = (t - 1.0f);
		return f * f * f + 1.0f;
	}
	
	public static float CubicEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			return 4.0f * t * t * t;
		}
		else
		{
			float f = ((2.0f * t) - 2.0f);
			return 0.5f * f * f * f + 1.0f;
		}
	}
	
	public static float QuarticEaseIn (float t)
	{
		return t * t * t * t;
	}

	public static float QuarticEaseOut (float t)
	{
		float f = (t - 1.0f);
		return f * f * f * (1.0f - t) + 1.0f;
	}
	
	public static float QuarticEaseInOut (float t) 
	{
		if(t < 0.5f)
		{
			return 8.0f * t * t * t * t;
		}
		else
		{
			float f = (t - 1.0f);
			return -8.0f * f * f * f * f + 1.0f;
		}
	}
	
	public static float QuinticEaseIn (float t) 
	{
		return t * t * t * t * t;
	}
	
	public static float QuinticEaseOut (float t) 
	{
		float f = (t - 1.0f);
		return f * f * f * f * f + 1.0f;
	}
	
	public static float QuinticEaseInOut (float t) 
	{
		if(t < 0.5f)
		{
			return 16.0f * t * t * t * t * t;
		}
		else
		{
			float f = ((2.0f * t) - 2.0f);
			return 0.5f * f * f * f * f * f + 1.0f;
		}
	}
	
	public static float SineEaseIn (float t)
	{
		return Mathf.Sin((t - 1.0f) * HALFPI) + 1.0f;
	}
	
	public static float SineEaseOut (float t)
	{
		return Mathf.Sin(t * HALFPI);
	}
	
	public static float SineEaseInOut (float t)
	{
		return 0.5f * (1.0f - Mathf.Cos(t * PI));
	}
	
	public static float CircularEaseIn (float t)
	{
		return 1.0f - Mathf.Sqrt(1.0f - (t * t));
	}
	
	public static float CircularEaseOut(float t)
	{
		return Mathf.Sqrt((2.0f - t) * t);
	}
	
	public static float CircularEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			return 0.5f * (1.0f - Mathf.Sqrt(1.0f - 4.0f * (t * t)));
		}
		else
		{
			return 0.5f * (Mathf.Sqrt(-((2.0f * t) - 3.0f) * ((2.0f * t) - 1.0f)) + 1.0f);
		}
	}
	
	public static float ExponentialEaseIn (float t)
	{
		return Mathf.Approximately(t, 0.0f) ? t : Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
	}
	
	public static float ExponentialEaseOut (float t)
	{
		return Mathf.Approximately(t, 1.0f) ? t : 1.0f - Mathf.Pow(2.0f, -10.0f * t);
	}
	
	public static float ExponentialEaseInOut (float t)
	{
		if(Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f)) return t;
		
		if(t < 0.5f)
		{
			return 0.5f * Mathf.Pow(2.0f, (20.0f * t) - 10.0f);
		}
		else
		{
			return -0.5f * Mathf.Pow(2, (-20.0f * t) + 10.0f) + 1.0f;
		}
	}
	
	public static float ElasticEaseIn (float t)
	{
		return Mathf.Sin(13.0f * HALFPI * t) * Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
	}
	
	public static float ElasticEaseOut (float t)
	{
		return Mathf.Sin(-13.0f * HALFPI * (t + 1.0f)) * Mathf.Pow(2.0f, -10.0f * t) + 1.0f;
	}

	public static float ElasticEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			return 0.5f * Mathf.Sin(13.0f * HALFPI * (2.0f * t)) * Mathf.Pow(2.0f, 10.0f * ((2.0f * t) - 1.0f));
		}
		else
		{
			return 0.5f * (Mathf.Sin(-13.0f * HALFPI * ((2.0f * t - 1.0f) + 1.0f)) * Mathf.Pow(2.0f, -10.0f * (2.0f * t - 1.0f)) + 2.0f);
		}
	}

	public static float BackEaseIn (float t)
	{
		return t * t * t - t * Mathf.Sin(t * PI);
	}

	public static float BackEaseOut (float t)
	{
		float f = (1.0f - t);
		return 1.0f - (f * f * f - f * Mathf.Sin(t * PI));
	}

	public static float BackEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			float f = 2.0f * t;
			return 0.5f * (f * f * f - f * Mathf.Sin(f * PI));
		}
		else
		{
			float f = (1.0f - (2.0f * t - 1.0f));
			return 0.5f * (1.0f - (f * f * f - f * Mathf.Sin(f * PI))) + 0.5f;
		}
	}

	public static float BounceEaseIn (float t)
	{
		return 1.0f - BounceEaseOut(1.0f - t);
	}

	public static float BounceEaseOut (float t)
	{
		if(t < 0.36f)
		{
			return (121.0f * t * t) / 16.0f;
		}
		else if(t < 0.72f)
		{
			return (9.075f * t * t) - (9.9f * t) + 3.4f;
		}
		else if(t < 0.9f)
		{
			return (12.066f * t * t) - (19.635f * t) + 8.898f;
		}
		else
		{
			return (10.8f * t * t) - (20.52f * t) + 10.72f;
		}
	}

	public static float BounceEaseInOut (float t)
	{
		if(t < 0.5f)
		{
			return 0.5f * BounceEaseIn(t * 2.0f);
		}
		else
		{
			return 0.5f * BounceEaseOut(t * 2.0f - 1.0f) + 0.5f;
		}
	}
}
