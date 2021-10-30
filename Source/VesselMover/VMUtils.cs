/*
	This file is part of VesselMover /L Unleashed
		© 2020-2021 LisiasT
		© 2019-2020 jrodriguez
		© 2016-2018 Papa_Joe
		© 2015-2016 BahamutoD

	VesselMover /L is double licensed, as follows:

		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	VesselMover /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with VesselMover /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with VesselMover /L If not, see <https://www.gnu.org/licenses/>.
*/
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

