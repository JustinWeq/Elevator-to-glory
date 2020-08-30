using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.unit
{
    class GraveStone : MonoBehaviour
    {
        protected unit_control_script owner;

        private void OnMouseDown()
        {
            //see if the player can respawn
            if(GlobalManager.GetGlobalManager().GetPlayerController().GetLives()>0)
            {
                //respawn the player
                GlobalManager.GetGlobalManager().GetPlayerController().AddLives(-1);
                owner.Respawn();
                //destroy self
                Destroy(gameObject);
            }
        }

        public void SetOwner(unit_control_script owner)
        {
            this.owner = owner;
        }
    }
}
