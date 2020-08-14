using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_bar_controller : MonoBehaviour
{
    protected float hp;
    protected float max_hp;
    protected Camera cam;
    protected Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        max_hp = 100;
    }

    private void Awake()
    {

        //get the camera
        cam = GlobalManager.GetGlobalManager().GetCamera();
        canvas = GlobalManager.GetGlobalManager().GetCanvas();
        //hp_bar_back.transform.SetParent(canvas.transform,false);
        //get the hp bar front and back
        //hp_bar_back = Instantiate(HpBar,Vector3.zero,Quaternion.identity,canvas.transform);
        //hp_bar_front = hp_bar_back.GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update()
    {
       // hp_bar_back.transform.position = transform.position;//cam.WorldToScreenPoint(hp_bar_back.transform.position);
       transform.LookAt( new Vector3(transform.position.x,cam.transform.position.y,cam.transform.position.z), -Vector3.up);
        //scale the hp bar based on max hp
        transform.localScale = new Vector3(hp / max_hp, 1, 1);
    }

    public void SetHpAndMaxHp(float hp,float max_hp)
    {
        this.hp = hp;
        this.max_hp = max_hp;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }


}
