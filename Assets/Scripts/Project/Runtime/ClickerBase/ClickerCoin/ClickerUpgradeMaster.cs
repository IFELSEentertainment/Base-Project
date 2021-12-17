using System.Collections.Generic;
using Base;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
[CreateAssetMenu(fileName = "NewClickerMaster", menuName = "Clicker/Create Master", order = 0)]
public class ClickerUpgradeMaster : ScriptableObject {

    #region Modifiers

    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Values")]
    [Range(1, 100)]
    public float BaseCostMulti = 1;
    
    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Values")]
    [Range(1, 1250)]
    public float BaseCostValue = 250;

    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Values")]
    [Range(1, 100)]
    public float BaseUpgradeMulti = 1;
    
    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Values")]
    [Range(1, 100)]
    public float BaseUpgradeValue = 10;

    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Curves")]
    public AnimationCurve BaseCostCurve;
    
    [ShowIf("@IsListNullOrHasNull() == true")]
    [TabGroup("Master", "Modifiers")]
    [BoxGroup("Master/Modifiers/Curves")]
    public AnimationCurve BaseUpgradeCurve;

    #region Functions

    [HorizontalGroup("Master/Configs/A")]
    [BoxGroup("Master/Configs/A/Functions")]
    [ShowIf("@IsListNullOrHasNull() == true")]
    [Button("Setup Upgrades")]
    void SetupCurrentUpgrades() {
        float baseCost = BaseCostValue * BaseCostMulti;
        float baseAmount = BaseUpgradeValue * BaseUpgradeMulti;
        List<string> names = new List<string>();
        foreach (var upgrade in Upgrades) {
            baseCost *= (1 + (int)upgrade.Tier) * BaseCostCurve.Evaluate(((int)upgrade.Tier + 1));
            baseAmount *= (1 + (int)upgrade.Tier) * BaseUpgradeCurve.Evaluate(((int)upgrade.Tier + 1));
            upgrade.ModifyValuesToMaster(in baseCost, in baseAmount);
            names.Add(upgrade.name);
        }
        EnumCreator.CreateEnum("ClickerUpgrades", names.ToArray());
    }
    #endregion
    
    #endregion

    #region Configs
    
    [TabGroup("Master", "Configs")]
    [HorizontalGroup("Master/Configs/A")]
    [BoxGroup("Master/Configs/A/Upgrades")]
    public List<ClickerUpgrade> Upgrades;

    
    
    #region Functions

    [HorizontalGroup("Master/Configs/A")]
    [BoxGroup("Master/Configs/A/Functions")]
    [ShowIf("@IsListNullOrHasNull() == false")]
    [Button("Setup Master")]
    public void SetupMaster() {
        Upgrades = ExtentionFunctions.FindAssetsByType<ClickerUpgrade>();
    }
    
    [HorizontalGroup("Master/Configs/A")]
    [BoxGroup("Master/Configs/A/Functions")]
    [ShowIf("@IsListNullOrHasNull() == true")]
    [Button("Delete Saves")]
    public void DeleteSaves() {
        foreach (var VARIABLE in Upgrades) {
            VARIABLE.DeleteSaves();
        }
        SetupCurrentUpgrades();
    }

    #endregion
    
    #endregion

    #region Helpers

    bool IsListNullOrHasNull() {
        if (Upgrades.IsNullOrEmpty()) return false;
        foreach (var VARIABLE in Upgrades) {
            if (VARIABLE == null) return false;
        }
        return true;
    }

    #endregion
    
}