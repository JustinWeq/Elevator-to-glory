using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.utility
{
    class EliminationLevel : level_manager
    {

        public override string GetDescription()
        {
            if (!ObjectiveReached())
                return LevelObjective + "\n enemys left: " + enemys.Count;
            else
                return LevelObjective + "\n all enemys elimininated";
        }

        public override bool ObjectiveReached()
        {
            if (enemys.Count < 1)
                return true;
            return false;
        }

        public override void StartLevel()
        {
            //do nothing since elimination levels start by default
        }
    }
}
