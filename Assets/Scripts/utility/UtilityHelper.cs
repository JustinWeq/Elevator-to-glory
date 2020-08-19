using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.utility
{
    class UtilityHelper
    {

        public static bool InRange(Vector3 point1,Vector3 point2,float distance)
        {
            return Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2) + Math.Pow(point1.z - point2.z, 2) < Math.Pow(distance, 2);
        }

        public static bool InRange(Vector2 point1,Vector2 point2,float distance)
        {
            return Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.x - point2.x, 2) < Math.Pow(distance, 2);
        }
    }
}
