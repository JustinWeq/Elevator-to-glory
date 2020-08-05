using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    protected unit_control_script active_unit;
    public RawImage hp_bar;
    public RawImage mana_bar;
    public Text health_text;
    public Text mana_text;
    public Text stat_text;
    public RawImage ability1_image;
    public RawImage ability2_image;
    public RawImage ability3_image;
    public RawImage ability4_image;
    private float mana_bar_length;
    private float health_bar_length;


    // Start is called before the first frame update
    void Start()
    {
        //get the ui elements

        mana_bar_length = mana_bar.rectTransform.sizeDelta.x;
        health_bar_length = hp_bar.rectTransform.sizeDelta.x;
    }

    public void SetActiveUnit(GameObject unit)
    {
        active_unit = unit.GetComponent<unit_control_script>();
        UpdateUiInfo();
    }



    protected void UpdateUiInfo()
    {
        //set the ability icons
        if (active_unit == null)
            return;
        ability1_image.texture = active_unit.GetAbility(0).GetIcon();
        ability2_image.texture = active_unit.GetAbility(1).GetIcon();
        ability3_image.texture = active_unit.GetAbility(2).GetIcon();
        ability4_image.texture = active_unit.GetAbility(3).GetIcon();
        //update the health bar
        health_text.text = active_unit.GetHp()+"/"+active_unit.GetMaxHp();
        //hp_bar.rectTransform.sizeDelta = new Vector2(health_bar_length * active_unit.GetHp() / active_unit.GetMaxHp(), hp_bar.rectTransform.sizeDelta.y);
        //update the mana bar
        mana_text.text = active_unit.GetMana() + "/" + active_unit.GetMaxMana();
        // mana_bar.rectTransform.sizeDelta = new Vector2(mana_bar_length * active_unit.GetMana() / active_unit.GetMaxMana(), mana_bar.rectTransform.sizeDelta.y);

        //get pther stats
        StringBuilder str = new StringBuilder();
        str.Append("Strength: " + active_unit.GetStrength() + "\n");
        str.Append("Agility: " + active_unit.GetAgility() + "\n");
        str.Append("Intelligence: " + active_unit.GetIntelligence() + "\n");
        stat_text.text = str.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUiInfo();
    }
}
