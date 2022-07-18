using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystemManager : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private GameObject swordSkillObject;

    [SerializeField]
    private GameObject staffSkillObject;

    [SerializeField]
    private GameObject shieldSkillObject;

    [SerializeField]
    private GameObject daggerSkillObject;

    [SerializeField]
    private GameObject vitalitySkillObject;

    [SerializeField]
    private GameObject mindSkillObject;

    [SerializeField]
    private GameObject enduranceSkillObject;

    [SerializeField]
    private GameObject toughnessSkillObject;

    [SerializeField]
    private GameObject wisdomSkillObject;

    [SerializeField]
    private DefaultWeaponAbilities swordAbilities;

    [SerializeField]
    private DefaultWeaponAbilities staffAbilities;

    [SerializeField]
    private DefaultWeaponAbilities shieldAbilities;

    [SerializeField]
    private DefaultWeaponAbilities daggerAbilities;

    #endregion

    #region Fields

    private GameObject abilityTemplate;

    private GameObject weaponSkillPanel;

    private GameObject abilityInfoPanel;

    private GameObject selectedAbilityBorder;

    private CharacterSkills characterSkills;

    private float timeCounter;

    private const float updateTime = 0.5f;

    #endregion

    private void Start()
    {
        characterSkills = GlobalControl.instance.characterSkills;
        weaponSkillPanel = transform.Find("WeaponSkillPanel").gameObject;
        abilityInfoPanel = weaponSkillPanel.transform.Find("AbilityInfoPanel").gameObject;
        abilityTemplate = weaponSkillPanel.transform.Find("AbilityPanel").Find("Ability Template").gameObject;
        UpdateUI();
    }

    private void FixedUpdate()
    {
        timeCounter += Time.fixedDeltaTime;
        if (timeCounter >= updateTime)
        {
            timeCounter -= updateTime;
            UpdateSkillUI(mindSkillObject, characterSkills.mind);
            UpdateSkillUI(wisdomSkillObject, characterSkills.wisdom);
            UpdateSkillUI(staffSkillObject, characterSkills.staffSkill);
        }
    }

    private void UpdateUI()
    {
        UpdateSkillUI(swordSkillObject, characterSkills.swordSkill);
        UpdateSkillUI(staffSkillObject, characterSkills.staffSkill);
        UpdateSkillUI(shieldSkillObject, characterSkills.shieldSkill);
        UpdateSkillUI(daggerSkillObject, characterSkills.daggerSkill);

        UpdateSkillUI(vitalitySkillObject, characterSkills.vitality);
        UpdateSkillUI(mindSkillObject, characterSkills.mind);
        UpdateSkillUI(enduranceSkillObject, characterSkills.endurance);
        UpdateSkillUI(toughnessSkillObject, characterSkills.toughness);
        UpdateSkillUI(wisdomSkillObject, characterSkills.wisdom);
    }

    private void UpdateSkillUI(GameObject skillObject, Skill skill)
    {
        skillObject.transform.Find("Level").GetComponent<Text>().text = $"Level: {skill.Level}";
        skillObject.transform.Find("ExperienceBar").Find("bar").GetComponent<Image>().fillAmount = skill.Experience / (float) skill.RequiredExperience();
        skillObject.transform.Find("ExperienceText").GetComponent<Text>().text = skill.Level < skill.MaxLevel ? $"{skill.Experience} / {skill.RequiredExperience()} XP" : "MAX";
    }

    public void OnWeaponSkillButton(string weaponType)
    {
        if (weaponSkillPanel.activeSelf)
            weaponSkillPanel.SetActive(false);

        if (abilityInfoPanel.activeSelf)
            abilityInfoPanel.SetActive(false);

        weaponSkillPanel.transform.Find("Weapon Skill Title").GetComponent<Text>().text = $"{weaponType} Abilities";

        var defaultAbilities = FindDefaultAbilities(weaponType);
        if (defaultAbilities is null) return;

        FillAbilityPanel(defaultAbilities);

        weaponSkillPanel.SetActive(true);
    }

    private List<Ability> FindDefaultAbilities(string weaponType)
    {
        switch (weaponType)
        {
            case "Sword":
                return swordAbilities.abilities;
            case "Staff":
                return staffAbilities.abilities;
            case "Shield":
                return shieldAbilities.abilities;
            case "Dagger":
                return daggerAbilities.abilities;
            default:
                Debug.LogError("Null abilities list returned from FindDefaultAbilities in SkillSystemManager.");
                return null;
        }
    }

    private void FillAbilityPanel(List<Ability> abilities)
    {
        var content = weaponSkillPanel.transform.Find("AbilityPanel").Find("Content");
        ClearContentPanel(content);
        abilities.ForEach(ability =>
        {
            var abilityTransform = Instantiate(abilityTemplate, content).transform;
            abilityTransform.name = ability.name;
            abilityTransform.Find("AbilityName").GetComponent<Text>().text = ability.name;
            abilityTransform.Find("Level").GetComponent<Text>().text = $"LVL:{ability.RequiredLevel}";
            abilityTransform.gameObject.GetComponent<AbilityContainer>().Ability = ability;
            abilityTransform.GetComponent<Button>().onClick.AddListener(delegate { DisplayAbilityInfo(abilityTransform); });
            abilityTransform.gameObject.SetActive(true);
        });
    }

    private void DisplayAbilityInfo(Transform abilityTransform)
    {
        SetAbilityBorder(abilityTransform);

        if (abilityInfoPanel.activeSelf)
            abilityInfoPanel.SetActive(false);

        var abilityInfoTransform = abilityInfoPanel.transform;

        var ability = abilityTransform.GetComponent<AbilityContainer>().Ability;
        abilityInfoTransform.Find("AbilityName").GetComponent<Text>().text = ability.name;
        abilityInfoTransform.Find("LevelReq").GetComponent<Text>().text = $"Lvl Required: {ability.RequiredLevel}";
        abilityInfoTransform.Find("Cost").GetComponent<Text>().text = $"Stamina Cost: {ability.cost}";
        abilityInfoTransform.Find("Description").GetComponent<Text>().text = ability.description;
        if (ability is DamageAbility dmgAbility)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Damage Multiplier: x{dmgAbility.damageMultiplier}");
            sb.AppendLine($"Physical Penetration: {dmgAbility.physicalPen * 100}%");
            sb.AppendLine($"Magic Penetration: {dmgAbility.magicPen * 100}%");

            abilityInfoTransform.Find("AbilityStats").GetComponent<Text>().text = sb.ToString();
        }
        else if (ability is BuffAbility buffAbility)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Physical Resist Bonus: {buffAbility.BuffValue}");
            sb.AppendLine($"Magic Resist Bonus: {buffAbility.BuffValue}");

            abilityInfoTransform.Find("AbilityStats").GetComponent<Text>().text = sb.ToString();
        }

        abilityInfoPanel.gameObject.SetActive(true);
    }

    private void SetAbilityBorder(Transform abilityTransform)
    {
        if (selectedAbilityBorder)
            selectedAbilityBorder.SetActive(false);
        selectedAbilityBorder = abilityTransform.Find("toggleBorder").gameObject;
        selectedAbilityBorder.SetActive(true);
    }

    private void ClearContentPanel(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnSkillUIButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnExitWeaponSkillPanelButton()
    {
        abilityInfoPanel.SetActive(false);
        weaponSkillPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        OnExitWeaponSkillPanelButton();
        gameObject.SetActive(false);
    }
}
