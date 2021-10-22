/*
	This file is part of VesselMover /L
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VesselMover
{
    public class MoveLaunchMassModifier : PartModule
    {
        public bool modify = true;
        private float defaultMass = 0;

        public override void OnStart(StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                part.force_activate();
                defaultMass = this.part.mass;
                this.part.mass = 0;
            }
            base.OnStart(state);
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (modify)
                {
                    this.part.mass = 0;
                }
                else
                {
                    StartCoroutine(Drop());
                }
            }
        }

        IEnumerator Drop()
        {
            yield return new WaitForEndOfFrame();
            this.part.mass = defaultMass / 6;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            this.part.mass = defaultMass / 4;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            this.part.mass = defaultMass / 2;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            this.part.mass = defaultMass;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Destroy(this);
        }
    }
}
