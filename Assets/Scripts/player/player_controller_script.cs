using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_controller_script : MonoBehaviour
{
    public List<GameObject> ManuallyAddedUnits;
    public GameObject AttackIndicator;
    public GameObject MoveIndicator;
    List<GameObject> controlled_units;
    private GameObject main_unit;
    private ui_script ui;
    private Camera_script camera;
    private Canvas canvas;
    private int gold;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the ui script
        ui = GetComponent<ui_script>();
        //get the camera script
        camera = GetComponent<Camera_script>();
        controlled_units = new List<GameObject>();
        canvas = GetComponent<Canvas>();
        //add all of the manually added units to the controlled units group
        foreach(GameObject unit in ManuallyAddedUnits)
        {
            controlled_units.Add(unit);
            main_unit = unit;
            //set the unit control script unit
            
        }
        ui.SetActiveUnit(main_unit);
        camera.SetActiveUnit(main_unit);
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
        //check to see if the map was clicked
        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            RaycastHit hit;
            if (Physics.Raycast(GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.tag == "Traversable")
                {
                    foreach (GameObject unit in controlled_units)
                    {
                        unit.GetComponent<unit_move_script>().MoveTo(hit.point);
                    }
                    Instantiate(MoveIndicator, hit.point, AttackIndicator.transform.rotation);
                }
                else if(hit.transform.tag == "Enemy")
                {
                    foreach(GameObject unit in controlled_units)
                    {
                        unit.GetComponent<unit_control_script>().SetAttackOrder(hit.transform.gameObject);
                    }
                    Instantiate(AttackIndicator, hit.point,AttackIndicator.transform.rotation);
                }
            }

        }
    }
    
    public void AddToControlledUnits(GameObject unit)
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
    }

    public void RemoveFromControlled(GameObject unit)
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

}
