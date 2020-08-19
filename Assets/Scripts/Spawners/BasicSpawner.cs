using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    class BasicSpawner : MonoBehaviour
    {
        public int AmountToSpawn;
        public bool Infinite;
        public float Delay;
        public GameObject SpawnObject;
        protected float remaining_time = 0;

        public float MaxEnemys = 100;

        private void Update()
        {
            remaining_time -= Time.deltaTime;
            if(remaining_time <=0 && GlobalManager.GetGlobalManager().GetLevelManager().GetEnemys().Count < MaxEnemys)
            {
                //spawn the object
                Instantiate(SpawnObject, transform.position,Quaternion.identity);
                remaining_time = Delay;
            }
        }

    }
}
