using Assets.Scripts.utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UIElements;

public class player_controller_script : MonoBehaviour
{
    public List<GameObject> ManuallyAddedUnits;
    public GameObject AttackIndicator;
    public GameObject MoveIndicator;
    List<unit_control_script> controlled_units;
    private unit_control_script main_unit;
    private ui_script ui;
    private Camera_script camera;
    private Canvas canvas;
    private int gold;
    private int lives;
    private int player_id;
    // Start is called before the first frame update
    void Start()
    {
        //Get the ui script
        ui = GetComponent<ui_script>();
        //get the camera script
        camera = GetComponent<Camera_script>();
        controlled_units = new List<unit_control_script>();
        canvas = GetComponent<Canvas>();
        //add all of the manually added units to the controlled units group
        foreach(GameObject unit in ManuallyAddedUnits)
        {
            controlled_units.Add(unit.GetComponent<unit_control_script>());
            main_unit = unit.GetComponent<unit_control_script>();
            //set the unit control script unit
            
        }
        ui.SetActiveUnit(main_unit);
        camera.SetActiveUnit(main_unit);
        //register player with the game manager
        game_manager.GetGameManager().RegisterPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("q"))
        {
            attemptAbilityFire(0);
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("w"))
        {
            attemptAbilityFire(1);
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("e"))
        {
            attemptAbilityFire(2);
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("r"))
        {
            attemptAbilityFire(3);
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("f"))
        {
            attemptAbilityFire(4);
        }
        //check to see if any ability keys are being pressed
        if (Input.GetKeyDown("d"))
        {
            attemptAbilityFire(5);
        }
        if(Input.GetKeyDown("u"))
        {
            if(main_unit.GetSkillPoints() > 0 && !ui.IsInLevelingMode())
            {
                ui.EnterLevelStatus();
            }
            else
            {
                ui.ExitLevelStatus();
            }
        }
        //check to see if the map was clicked
        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            RaycastHit hit;
            if (Physics.Raycast(GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.tag.Contains("Traversable"))
                {
                    foreach (unit_control_script unit in controlled_units)
                    {
                        if(unit.GetCanOrder() && unit.GetCanMove())
                            unit.GetComponent<unit_move_script>().MoveTo(hit.point);
                    }
                    Instantiate(MoveIndicator, hit.point, AttackIndicator.transform.rotation);
                }
                else if(hit.transform.tag.Contains("Enemy"))
                {
                    foreach(unit_control_script unit in controlled_units)
                    {
                        if (unit.GetCanOrder() && unit.GetCanMove())
                              unit.GetComponent<unit_control_script>().SetAttackOrder(hit.transform.gameObject);
                    }
                    Instantiate(AttackIndicator, hit.point,AttackIndicator.transform.rotation);
                }
            }

        }
    }
    
    public void AddToControlledUnits(unit_control_script unit)
    {
        if(controlled_units.Contains(unit))
        {
            return;
        }
        controlled_units.Add(unit);
    }

    private void attemptAbilityFire(int index)
    {
        
        Ability ability = main_unit.GetComponent<unit_control_script>().GetAbility(index);

        if (ability.GetLevel() < 1)
            return;
        if(main_unit.GetMana()< ability.GetCost() || ability.OnCooldown())
        {
            return;
        }
        //if the ability is a point target spell see if there is a target under neath the player and that the enemy is in range
        if(ability.ActivationType == AbilityActivationType.Cast)
        {
            if (ability._TargetType == TargetType.PointTarget)
            {
                //see if there is an enemy under the player cursor
                RaycastHit hit;
                if (Physics.Raycast(GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
                {
                    enemy_controller enemy = hit.transform.gameObject.GetComponent<enemy_controller>();
                    if (hit.transform.CompareTag("Enemy") && UtilityHelper.InRange(main_unit.GetPosition(), enemy.GetPosition(), (ability.GetCastRange() + main_unit.GetCastRange()) / 100.0f))
                    {
                        ability.ActivateAbility(hit.transform.gameObject);
                        //reduce the players mana
                        main_unit.AddMana(-ability.GetCost());
                    }
                }
            }

            if(ability._TargetType == TargetType.Self)
            {
                //check to see if ability is on cooldown
                if(!ability.OnCooldown())
                {
                    ability.ActivateAbility();
                }
            }
        }
    }

    public void RemoveFromControlled(unit_control_script unit)
    {
        controlled_units.Remove(unit);
    }

    public int GetGold()
    {
        return gold;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public float GetLives()
    {
        return lives;
    }

    public void AddLives(int amount)
    {
        lives += amount;
    }

    public void SetPlayerId(int id)
    {
        player_id = id;
    }

    public int GetPlayerId()
    {
        return player_id;
    }

}
