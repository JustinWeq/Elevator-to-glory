﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface OnAbilityHit
    {
        void OnAbilityHit(ref float damage,GameObject target);
    }
}
