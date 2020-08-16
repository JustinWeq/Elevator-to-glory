using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    protected unit_control_script active_unit;
    protected player_controller_script player_script;
    public RawImage hp_bar;
    public RawImage mana_bar;
    public Text health_text;
    public Text mana_text;
    public Text stat_text;
    public Text gold_text;
    public RawImage AbilityCardBack;
    public RawImage UIBack;
    public Text AbilityDescriptionText;
    public Text AbilityCostText;
    public RawImage[] AbilityIcons = new RawImage[4];
    public RawImage[] ItemImages = new RawImage[6];
    private float mana_bar_length;
    private float health_bar_length;


    // Start is called before the first frame update
    void Awake()
    {
        //get the ui elements

        mana_bar_length = mana_bar.rectTransform.sizeDelta.x;
        health_bar_length = hp_bar.rectTransform.sizeDelta.x;
        // move the ui down to the bottom of the screen
        // ui_back.rectTransform.anchoredPosition = new Vector2(Screen.width/2,ui_back.rectTransform.anchoredPosition.y);

        //get the player script
        player_script = GetComponentInParent<player_controller_script>();

        //instantly hide the ability description
        hideAbilityCard();

        //add an event to all of the ability icons
      //  AbilityIcons[0].gameObject.
    }

    public void SetActiveUnit(GameObject unit)
    {
        active_unit = unit.GetComponent<unit_control_script>();
        UpdateUiInfo();
    }

    private void hideAbilityCard()
    {
        AbilityDescriptionText.enabled = false;
        AbilityCostText.enabled = false;
        AbilityCardBack.enabled = false;
    }

    private void showAbilityCard(int index)
    {
        AbilityCardBack.enabled = true;
        AbilityDescriptionText.enabled = true;
        AbilityCostText.enabled = true;

        //set the text and cost of the ability
        AbilityCostText.text = "Mana: " + active_unit.GetAbility(index).GetCost() + " Cooldown: " + active_unit.GetAbility(index).GetCooldown();
        AbilityDescriptionText.text = active_unit.GetAbility(index).GetDescription();
        AbilityCardBack.rectTransform.position = new Vector3(AbilityIcons[index].rectTransform.position.x,AbilityCardBack.rectTransform.position.y,AbilityCardBack.transform.position.z);
    }



    protected void UpdateUiInfo()
    {
        //set the ability icons
        if (active_unit == null)
            return;
        AbilityIcons[0].texture = active_unit.GetAbility(0).GetIcon();
        AbilityIcons[1].texture = active_unit.GetAbility(1).GetIcon();
        AbilityIcons[2].texture = active_unit.GetAbility(2).GetIcon();
        AbilityIcons[3].texture = active_unit.GetAbility(3).GetIcon();
        //update the health bar
        health_text.text = active_unit.GetHp()+"/"+active_unit.GetMaxHp() + " + " + (active_unit.GetHpRegen() + active_unit.GetAddedHpRegen());
        hp_bar.rectTransform.sizeDelta = new Vector2(health_bar_length*active_unit.GetHp()/active_unit.GetMaxHp(), hp_bar.rectTransform.sizeDelta.y);
        //update the mana bar
        mana_text.text = active_unit.GetMana() + "/" + active_unit.GetMaxMana() + " + " + (active_unit.GetManaRegen() + active_unit.GetAddedManaRegen());
        mana_bar.rectTransform.sizeDelta = new Vector2(mana_bar_length * active_unit.GetMana() / active_unit.GetMaxMana(), mana_bar.rectTransform.sizeDelta.y);

        //update gold text
        gold_text.text = "Gold: " + player_script.GetGold();

        //get pther stats
        StringBuilder str = new StringBuilder();
        str.Append("Strength: " + active_unit.GetStrength() + "\n");
        str.Append("Agility: " + active_unit.GetAgility() + "\n");
        str.Append("Intelligence: " + active_unit.GetIntelligence() + "\n");
        str.Append("Attack damage: " + active_unit.GetBaseDamage() + " + " + active_unit.GetAddedDamage() + "\n");
        str.Append("Attack speed: " + active_unit.GetAttackSpeed() + " + " + active_unit.GetAddedAttackSpeed() + " Total Attack Time:  " + active_unit.GetAttackTime() + "\n");
        str.Append("Move speed: " + active_unit.GetMovespeed() + " + " + active_unit.GetAddedMovespeed() + "\n");
        str.Append("Ability amp: " + active_unit.GetSpellamp() + " + " + active_unit.GetAddedSpellAmp() + "\n");
        stat_text.text = str.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUiInfo();
        bool over_icon = false;
        //decide if the mouse is over an ability icon
        for(int i = 0;i < 4;i++)
        {
            if (AbilityIcons[i].GetComponent<MouseOverDetector>().MouseIsOver())
            {
                showAbilityCard(i);
                over_icon = true;
            }

        }

        if(!over_icon)
        {
            hideAbilityCard();
        }
    }
}
