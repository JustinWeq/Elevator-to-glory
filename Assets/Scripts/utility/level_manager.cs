using Assets.Scripts.utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class level_manager : MonoBehaviour
{
    public string LevelObjective;
    protected List<enemy_controller> enemys;
    protected List<unit_control_script> player_units;
    // Start is called before the first frame update
    void Awake()
    {
        //init the enemy list
        enemys = new List<enemy_controller>();
        //init the player list
        player_units = new List<unit_control_script>();
        GlobalManager.GetGlobalManager().SetLevelManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<enemy_controller> GetEnemys()
    {
        return enemys;
    }

    public void AddEnemy(enemy_controller enemy)
    {
        enemys.Add(enemy);
    }

    public void RemoveEnemy(enemy_controller enemy)
    {
        enemys.Remove(enemy);
    }

    public void AddPlayerUnit(unit_control_script unit)
    {
        player_units.Add(unit);
    }

    public void RemovePlayerUnit(unit_control_script unit)
    {
        player_units.Remove(unit);
    }

    public static level_manager GetLevelManager()
    {
        return GlobalManager.GetGlobalManager().GetLevelManager();
    }

    public List<unit_control_script> GetPlayerUnitsInRange(Vector3 point, float distance)
    {
        List<unit_control_script> units_in_range = new List<unit_control_script>();
        //go through all of the units and see if there are in range
        foreach (unit_control_script unit in player_units)
        {
            if (UtilityHelper.InRange(unit.GetPosition(), point, distance))
                units_in_range.Add(unit);
        }

        return units_in_range;
    }

    public abstract void StartLevel();

    public abstract bool ObjectiveReached();

    public abstract string GetDescription();
}
