using System;
using UnityEngine;

namespace VesselMover
{
	public static class VMUtils
	{
		public static bool SphereRayIntersect(Ray ray, Vector3 sphereCenter, double sphereRadius, out double distance)
		{
			Vector3 o = ray.origin;
			Vector3 l = ray.direction;
			Vector3d c = sphereCenter;
			double r = sphereRadius;

			double d;

			d = -(Vector3.Dot(l, o - c) + Math.Sqrt(Mathf.Pow(Vector3.Dot(l, o - c), 2) - (o - c).sqrMagnitude + (r * r))); 

			if(double.IsNaN(d))
			{
				distance = 0;
				return false;
			}
			else
			{
				distance = d;
				return true;
			}
		}
	  internal static void RepositionWindow(ref Rect windowPosition)
	  {
	    // This method uses Gui point system.
	    if (windowPosition.x < 0) windowPosition.x = 0;
	    if (windowPosition.y < 0) windowPosition.y = 0;

	    if (windowPosition.xMax > Screen.width)
	      windowPosition.x = Screen.width - windowPosition.width;
	    if (windowPosition.yMax > Screen.height)
	      windowPosition.y = Screen.height - windowPosition.height;
	  }
	}
}

