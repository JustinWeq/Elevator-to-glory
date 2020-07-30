using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_controller_script : MonoBehaviour
{
    public List<GameObject> ManuallyAddedUnits;
    public GameObject DebugSphere;
    List<GameObject> controlled_units;
    private GameObject main_unit;
    // Start is called before the first frame update
    void Start()
    {
        controlled_units = new List<GameObject>();
        //add all of the manually added units to the controlled units group
        foreach(GameObject unit in ManuallyAddedUnits)
        {
            controlled_units.Add(unit);
            main_unit = unit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if the map was clicked
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            RaycastHit hit;
            if (Physics.Raycast(GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
            {
                foreach(GameObject unit in controlled_units)
                {
                    unit.GetComponent<unit_move_script>().MoveTo(hit.point);
                }
                Instantiate(DebugSphere, hit.point, Quaternion.identity);
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

    public void RemoveFromControlled(GameObject unit)
    {
        controlled_units.Remove(unit);
    }
}
