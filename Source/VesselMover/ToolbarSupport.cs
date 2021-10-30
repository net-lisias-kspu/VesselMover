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
using UnityEngine;

using KSPe.Annotations;
using Toolbar = KSPe.UI.Toolbar;
using GUI = KSPe.UI.GUI;
using GUILayout = KSPe.UI.GUILayout;

namespace VesselMover
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class ToolbarController : MonoBehaviour
	{
		internal static KSPe.UI.Toolbar.Toolbar Instance => KSPe.UI.Toolbar.Controller.Instance.Get<ToolbarController>();

		[UsedImplicitly]
		private void Start()
		{
			KSPe.UI.Toolbar.Controller.Instance.Register<ToolbarController>(Version.FriendlyName);
		}
	}
}
