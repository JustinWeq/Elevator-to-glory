using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class BasicSpawner : MonoBehaviour
    {
        public int AmountToSpawn;
        public bool Infinite;
        public float Delay;
        public GameObject SpawnObject;
        protected float remaining_time = 0;
        protected bool can_spawn = true;

        public float MaxEnemys = 100;

        private void Start()
        {
            //add self to the level manager
            GlobalManager.GetGlobalManager().GetLevelManager().AddSpawner(this);
        }

        private void Update()
        {
            remaining_time -= Time.deltaTime;
            if(can_spawn && remaining_time <=0 && GlobalManager.GetGlobalManager().GetLevelManager().GetEnemys().Count < MaxEnemys)
            {
                //spawn the object
                Instantiate(SpawnObject, transform.position,Quaternion.identity);
                remaining_time = Delay;
            }
        }

        public void SetCanSpawn(bool can_spawn)
        {
            this.can_spawn = can_spawn;
        }

        public bool GetCanSpawn()
        {
            return can_spawn;
        }

        private void OnDestroy()
        {
            //remove self from thje spawner list
            GlobalManager.GetGlobalManager().GetLevelManager().RemoveSpawner(this);
        }

    }
}
