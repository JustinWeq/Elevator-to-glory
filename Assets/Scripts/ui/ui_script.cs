using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public Text LevelText;
    public Text SkillPointText;
    public Text LevelObjectiveText;
    public Text[] AbilityCooldownTexts = new Text[4];
    public Text ExtraStatsText;
    public RawImage AbilityCardBack;
    public RawImage UIBack;
    public Text AbilityDescriptionText;
    public Text AbilityCostText;
    public Text LivesText;
    public RawImage[] AbilityIcons = new RawImage[4];
    public RawImage[] AbilityIconTints = new RawImage[4];
    public RawImage[] ItemImages = new RawImage[6];
    public RawImage LevelIndicator;
    public RawImage ExtraStatsBack;
    public Texture LeveledTexture;
    public Texture UnLeveledTexture;
    public UnityEngine.UI.Button LevelUpButton;
    public InputField GoldAmountField;
    public UnityEngine.UI.Button GiveGoldButton;
    public UnityEngine.UI.Button ResetButton;
    public UnityEngine.UI.Button ToggleSpawners;
    public UnityEngine.UI.Button RespawnButton;
    public UnityEngine.UI.Button CanDieButton;
    public UnityEngine.UI.Button ExtraStatsToggleBtn;
    public UnityEngine.UI.Button AddLifeButton;
    private RawImage[][] level_indicators = new RawImage[4][];
    private float[] normal_tint_heights = new float[4];
    private float mana_bar_length;
    private float health_bar_length;
    private bool leveling_mode;


    // Start is called before the first frame update
    void Awake()
    {
        //get the ui elements
        mana_bar_length = mana_bar.rectTransform.sizeDelta.x;
        health_bar_length = hp_bar.rectTransform.sizeDelta.x;

        //get the player script
        player_script = GetComponentInParent<player_controller_script>();

        //instantly hide the ability description
        hideAbilityCard();


        //get the normal heights of all of the tints
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            normal_tint_heights[i] = AbilityIconTints[i].rectTransform.sizeDelta.y;
        }
        //set the leveling mode to fgalse
        leveling_mode = false;

        GiveGoldButton.onClick.AddListener(GiveGoldBtnClicked);
        LevelUpButton.onClick.AddListener(LevelUpBtnClicked);
        ResetButton.onClick.AddListener(ResetBtnClicked);
        RespawnButton.onClick.AddListener(RespawnBtnClicked);
        ToggleSpawners.onClick.AddListener(ToggleSpawnersBtnClicked);
        CanDieButton.onClick.AddListener(CanDieButtonClicked);
        AddLifeButton.onClick.AddListener(AddLifeBtnClicked);
        ExtraStatsToggleBtn.onClick.AddListener(ExtraStatsToggleBtnClicked);
        //toggle the extra stats
        ToggleExtraStats();
    }
    

    void ExtraStatsToggleBtnClicked()
    {
        ToggleExtraStats();
    }

    void AddLifeBtnClicked()
    {
        //add a life tpo the player controller
        GlobalManager.GetGlobalManager().GetPlayerController().AddLives(1);
    }

    void GiveGoldBtnClicked()
    {
        //give the player the gold amount in the field
        player_script.AddGold(int.Parse(GoldAmountField.text));
    }

    void RespawnBtnClicked()
    {
        active_unit.Respawn();
    }

    void ResetBtnClicked()
    {
        active_unit.ResetHpAndMana();
        active_unit.ResetSkills();
    }

    void CanDieButtonClicked()
    {
        active_unit.SetCanDie(!active_unit.GetCanDie());
    }

    void ToggleSpawnersBtnClicked()
    {
        //toggle the spawners in the leveel manager
        GlobalManager.GetGlobalManager().GetLevelManager().ToggleSpawners();
    }

    public void SetActiveUnit(unit_control_script unit)
    {
        active_unit = unit;
        //set up the level indicators
        setupLevelIndicators();
    }

    private void hideAbilityCard()
    {
        AbilityDescriptionText.enabled = false;
        AbilityCostText.enabled = false;
        AbilityCardBack.enabled = false;
    }

    protected void ToggleExtraStats()
    {
        ExtraStatsText.enabled = !ExtraStatsText.enabled;
        ExtraStatsBack.enabled = !ExtraStatsBack.enabled;
    }

    private void showAbilityCard(int index)
    {
        AbilityCardBack.enabled = true;
        AbilityDescriptionText.enabled = true;
        AbilityCostText.enabled = true;

        //set the text and cost of the ability
        AbilityCostText.text = "Mana: " + active_unit.GetAbility(index).GetCost() + " Cooldown: " + active_unit.GetAbility(index).GetCooldown();
        AbilityDescriptionText.text =active_unit.GetAbility(index).GetName() + "\n" +  active_unit.GetAbility(index).GetDescription();
        AbilityCardBack.rectTransform.position = new Vector3(AbilityIcons[index].rectTransform.position.x,AbilityCardBack.rectTransform.position.y,AbilityCardBack.transform.position.z);

        //set the level text
        LevelText.text = "Level: " + active_unit.GetLevel();
    }

    public void EnterLevelStatus()
    {
        leveling_mode = true;
        //go through all of the ability icons and check to see if they can be leveled
        for(int i =0;i < AbilityIcons.Length;i++)
        {
            if (active_unit.GetAbility(i).CanLevel())
            {
                AbilityIconTints[i].enabled = true;
                AbilityIconTints[i].color = Color.yellow*new Color(1,1,1,0.5f);
            }
            else
            {
                AbilityIconTints[i].enabled = true;
                AbilityIconTints[i].color = Color.black * new Color(1, 1, 1, 0.5f);
            }
        }
    }

    private void setupLevelIndicators()
    {
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            if(level_indicators[i] == null)
            level_indicators[i] = new RawImage[active_unit.GetAbility(i).GetMaxLevel()];
            for (int j = 0; j < active_unit.GetAbility(i).GetMaxLevel(); j++)
            {
                if (level_indicators[i][j] != null)
                    Destroy(level_indicators[i][j]);
                RawImage img = Instantiate(LevelIndicator,AbilityIcons[i].transform);
                int max_level = active_unit.GetAbility(i).GetMaxLevel();
                img.rectTransform.anchoredPosition += j * Vector2.up * (AbilityIcons[i].rectTransform.sizeDelta.y / max_level);
                img.texture = UnLeveledTexture;
                level_indicators[i][j] = img;
            }
        }
    }

    public bool IsInLevelingMode()
    {

        return leveling_mode;
    }

    public void ExitLevelStatus()
    {
        leveling_mode = false;
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            if (active_unit.GetAbility(i).GetLevel() < 1)
            {
                AbilityIconTints[i].color = Color.black * new Color(1, 1, 1, 0.5f);
            }
            else
            {
                AbilityIconTints[i].color = Color.black * new Color(1, 1, 1, 0.5f);
                AbilityIconTints[i].enabled = false;
            }
        }
    }



    public void DisableAbilitys()
    {
        for(int i = 0;i< AbilityIcons.Length;i++)
        {
            AbilityIconTints[i].enabled = true;
        }
    }

    public void EnableAbilitys()
    {
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            if (active_unit.GetAbility(i).GetLevel() > 0)
                AbilityIconTints[i].enabled = true;
        }
    }

    public void UpdateCooldowns()
    {
        for (int i = 0; i < AbilityIcons.Length;i++)
        {
            if(active_unit.GetAbility(i).OnCooldown())
            {
                AbilityIconTints[i].enabled = true;
                AbilityIconTints[i].rectTransform.sizeDelta = new Vector2(AbilityIconTints[i].rectTransform.sizeDelta.x,
                    normal_tint_heights[i] * (active_unit.GetAbility(i).GetRemainingCooldown() / active_unit.GetAbility(i).GetCooldown() > 1?1:
                    active_unit.GetAbility(i).GetRemainingCooldown() / active_unit.GetAbility(i).GetCooldown()));
                AbilityCooldownTexts[i].enabled = true;
                AbilityCooldownTexts[i].text = "" + active_unit.GetAbility(i).GetRemainingCooldown();
            }
            else
            {
                AbilityCooldownTexts[i].enabled = false;
                if (active_unit.GetAbility(i).GetLevel() > 0 && leveling_mode == false)
                    AbilityIconTints[i].enabled = false;
                else
                    AbilityIconTints[i].enabled = true;
            }
        }
    }

    public void LevelUpBtnClicked()
    {
        //add enough xp for the player to levelup
        active_unit.AddExperience(unit_control_script.XP_NEEDED_FOR_LEVEL);
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
        health_text.text = (int)active_unit.GetHp()+"/"+(int)active_unit.GetMaxHp() + " + " + (int)(active_unit.GetHpRegen() + active_unit.GetAddedHpRegen());
        hp_bar.rectTransform.sizeDelta = new Vector2(health_bar_length*active_unit.GetHp()/active_unit.GetMaxHp(), hp_bar.rectTransform.sizeDelta.y);
        //update the mana bar
        mana_text.text = (int)active_unit.GetMana() + "/" + (int)active_unit.GetMaxMana() + " + " + (int)(active_unit.GetManaRegen() + active_unit.GetAddedManaRegen());
        mana_bar.rectTransform.sizeDelta = new Vector2(mana_bar_length * active_unit.GetMana() / active_unit.GetMaxMana(), mana_bar.rectTransform.sizeDelta.y);

        //update gold text
        gold_text.text = "Gold: " + player_script.GetGold();

        //get pther stats
        StringBuilder str = new StringBuilder();
        str.Append("Strength: " + active_unit.GetStrength() + "\n");
        str.Append("Agility: " + active_unit.GetAgility() + "\n");
        str.Append("Intelligence: " + active_unit.GetIntelligence() + "\n");
        str.Append("Attack damage: " + (int)active_unit.GetBaseDamage() + " + " + (int)active_unit.GetAddedDamage() + "\n");
        str.Append("Attack speed: " + (int)active_unit.GetAttackSpeed() + " + " + (int)active_unit.GetAddedAttackSpeed() + " Total Attack Time:  " + active_unit.GetAttackTime() + "\n");
        str.Append("Move speed: " + (int)active_unit.GetMovespeed() + " + " + (int)active_unit.GetAddedMovespeed() + "\n");
        str.Append("Ability amp: " + active_unit.GetSpellamp() + " + " + active_unit.GetAddedSpellAmp() + "\n");
        stat_text.text = str.ToString();
        //display the amount of skill points
        SkillPointText.text = "Skill points: " + active_unit.GetSkillPoints();
        //set the level text
        LevelText.text = "Level: " + active_unit.GetLevel();

        //set the lives text
        LivesText.text = "X" + player_script.GetLives();

        //update the cooldowns
        UpdateCooldowns();

        //update the extra stats text
        str.Clear();
        str.Append("Armor: " + active_unit.GetBaseArmor() + "+" + active_unit.GetAddedArmor());
        str.Append("\nMagic resistance: " + active_unit.GetMagicResist());
        str.Append("\nCastspeed reduction: " + active_unit.GetCastSpeedReduction());
        str.Append("\nCooldown reduction: " + active_unit.GetCooldownReduction());
        str.Append("\nCast range: " + active_unit.GetCastRange());
        str.Append("\nSpell lifesteal: " + active_unit.GetSpellLifesteal());
        str.Append("\nLifesteal: " + active_unit.GetLifesteal());
        str.Append("\n Attack range: " + active_unit.GetAttackRange() );
        str.Append("\n Pure damage: " + active_unit.GetPureDamage());
        str.Append("\n Splash damage: " + active_unit.GetSplash());
        str.Append("\n Cleave damage: " + active_unit.GetCleave());
        str.Append("\n Crit damage: " + active_unit.GetCriticalDamage());
        str.Append("\n Crit chance: " + active_unit.GetCriticalChance());
        str.Append("\n Status resist: " + active_unit.GetStatusResist());

        ExtraStatsText.text = str.ToString();


        //check to see if the player has clicked on a level
        if (Input.GetMouseButtonDown(0) && leveling_mode)
        {
            //check to see if the ability can be leveled
            for (int i = 0; i < AbilityIcons.Length; i++)
            {
                if(AbilityIcons[i].GetComponent<MouseOverDetector>().MouseIsOver() && active_unit.GetAbility(i).CanLevel())
                {
                    active_unit.GetAbility(i).Level();
                    level_indicators[i][active_unit.GetAbility(i).GetLevel() - 1].texture = LeveledTexture;
                    //reduce the ability points
                    active_unit.AddSkillPoints(-1);
                    ExitLevelStatus();
                }
            }
        }

        //update the level objective text
        LevelObjectiveText.text = level_manager.GetLevelManager().GetDescription();

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
