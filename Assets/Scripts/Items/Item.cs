using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items 
{

    public enum AttributeType
    {
        Strength,
        Agility,
        Int,
        Hp,
        Hp_Regen,
        Mana,
        Mana_Regen,
        Damage,
        Attack_Speed,
        Attack_Range,
        Armor,
        Magice_Resistance,
        SpellAmp,
        Crit_Damage,
        Crit_Chance,
        Pure_Damage,
        Splash,
        Cleave,
        Status_Resist,
        CastSpeed_Reduction,
        Cooldown_Reduction,
        Cast_Range,
        LifeSteal,
        SpellLifeSteal
    }
    class Item : MonoBehaviour
    {
        public AttributeType[] types;
        public float[] values;

        public void OnAttach(unit_control_script unit)
        {
            for (int i = 0; i < types.Length; i++)
            {
                //add each attribute to the player
                switch(types[i])
                {
                    case AttributeType.Agility:
                        unit.AddAgility(values[i]);
                        break;
                    case AttributeType.Strength:
                        unit.AddStrength(values[i]);
                        break;
                    case AttributeType.Int:
                        unit.AddInt(values[i]);
                        break;
                    case AttributeType.Damage:
                        unit.AddDamage(values[i]);
                        break;
                    case AttributeType.Armor:
                        unit.AddArmor(values[i]);
                        break;
                    case AttributeType.Attack_Range:
                        unit.AddAttackRange(values[i]);
                        break;
                    case AttributeType.Attack_Speed:
                        unit.AddAttackSpeed(values[i]);
                        break;
                    case AttributeType.CastSpeed_Reduction:
                        unit.AddCastSpeedReduction(values[i]);
                        break;
                    case AttributeType.Cast_Range:
                        unit.AddCastRange(values[i]);
                        break;
                    case AttributeType.Cleave:
                        unit.AddCleave(values[i]);
                        break;
                    case AttributeType.Cooldown_Reduction:
                        unit.AddCooldownReduction(values[i]);
                        break;
                    case AttributeType.Crit_Chance:
                        unit.AddCritChance(values[i]);
                        break;
                    case AttributeType.Crit_Damage:
                        unit.AddCritDamage(values[i]);
                        break;
                    case AttributeType.Hp:
                        unit.AddMaxHp(values[i]);
                        break;
                    case AttributeType.Hp_Regen:
                        unit.AddHpRegen(values[i]);
                        break;
                    case AttributeType.Magice_Resistance:
                        unit.AddMagicResistance(values[i]);
                        break;
                    case AttributeType.Mana:
                        unit.AddMaxMana(values[i]);
                        break;
                    case AttributeType.Mana_Regen:
                        unit.AddManaRegen(values[i]);
                        break;
                    case AttributeType.Pure_Damage:
                        unit.AddPureDamage(values[i]);
                        break;
                    case AttributeType.SpellAmp:
                        unit.AddSpellAmp(values[i]);
                        break;
                    case AttributeType.LifeSteal:
                        unit.AddLifeSteal(values[i]);
                        break;
                    case AttributeType.SpellLifeSteal:
                        unit.AddSpellLifeSteal(values[i]);
                        break;
                    case AttributeType.Splash:
                        unit.AddSplash(values[i]);
                        break;
                    case AttributeType.Status_Resist:
                        unit.AddStatusResist(values[i]);
                        break;


                }
            }
        }

        public void OnRemove(unit_control_script unit)
        {
            for (int i = 0; i < types.Length; i++)
            {
                //add each attribute to the player
                switch (types[i])
                {
                    case AttributeType.Agility:
                        unit.AddAgility(-values[i]);
                        break;
                    case AttributeType.Strength:
                        unit.AddStrength(-values[i]);
                        break;
                    case AttributeType.Int:
                        unit.AddInt(-values[i]);
                        break;
                    case AttributeType.Damage:
                        unit.AddDamage(-values[i]);
                        break;
                    case AttributeType.Armor:
                        unit.AddArmor(-values[i]);
                        break;
                    case AttributeType.Attack_Range:
                        unit.AddAttackRange(-values[i]);
                        break;
                    case AttributeType.Attack_Speed:
                        unit.AddAttackSpeed(-values[i]);
                        break;
                    case AttributeType.CastSpeed_Reduction:
                        unit.AddCastSpeedReduction(-values[i]);
                        break;
                    case AttributeType.Cast_Range:
                        unit.AddCastRange(-values[i]);
                        break;
                    case AttributeType.Cleave:
                        unit.AddCleave(-values[i]);
                        break;
                    case AttributeType.Cooldown_Reduction:
                        unit.AddCooldownReduction(-values[i]);
                        break;
                    case AttributeType.Crit_Chance:
                        unit.AddCritChance(-values[i]);
                        break;
                    case AttributeType.Crit_Damage:
                        unit.AddCritDamage(-values[i]);
                        break;
                    case AttributeType.Hp:
                        unit.AddMaxHp(-values[i]);
                        break;
                    case AttributeType.Hp_Regen:
                        unit.AddHpRegen(-values[i]);
                        break;
                    case AttributeType.Magice_Resistance:
                        unit.AddMagicResistance(-values[i]);
                        break;
                    case AttributeType.Mana:
                        unit.AddMaxMana(-values[i]);
                        break;
                    case AttributeType.Mana_Regen:
                        unit.AddManaRegen(-values[i]);
                        break;
                    case AttributeType.Pure_Damage:
                        unit.AddPureDamage(-values[i]);
                        break;
                    case AttributeType.SpellAmp:
                        unit.AddSpellAmp(-values[i]);
                        break;
                    case AttributeType.LifeSteal:
                        unit.AddLifeSteal(-values[i]);
                        break;
                    case AttributeType.SpellLifeSteal:
                        unit.AddSpellLifeSteal(-values[i]);
                        break;
                    case AttributeType.Splash:
                        unit.AddSplash(-values[i]);
                        break;
                    case AttributeType.Status_Resist:
                        unit.AddStatusResist(-values[i]);
                        break;


                }
            }
        }
    }
}
