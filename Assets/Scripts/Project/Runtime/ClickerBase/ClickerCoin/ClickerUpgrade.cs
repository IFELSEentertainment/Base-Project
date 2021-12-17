using System.Runtime.CompilerServices;
using Base;
using Base.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public enum ClickerUpgradeType { Click, Second }
public enum ClickerUpgradeTier { Tier1, Tier2, Tier3, Tier4, Tier5, Tier6, Tier7, Tier8, Tier9, Tier10, Ultimate }
[CreateAssetMenu(fileName = "NewClickerUpgrade", menuName = "Clicker/Clicker Upgrade", order = 2)]
public class ClickerUpgrade : ScriptableObject {
    [TabGroup("Upgrade")]
    public Enum_Menu_PlayerOverlayComponent ButtonEnum;
    [TabGroup("Upgrade")]
    public ClickerUpgradeTier Tier;
    [TabGroup("Upgrade")]
    public ClickerUpgradeType UpgradeType;
    [TabGroup("Upgrade")]
    public ATFloat Cost;
    [TabGroup("Upgrade")]
    public AtModifier UpgradeAmount;
    [TabGroup("Upgrade")]
    public AnimationCurve Curve;
    [TabGroup("Upgrade")]
    [Range(1, 1000)]
    public int Limit;
    [TabGroup("Upgrade")]
    public int UseTimes;
    [TabGroup("Upgrade")]
    [SerializeField] private ClickerCoin coin;
    [TabGroup("Upgrade")]
    private string ButtonText;
    private UI_CButtonTMProSubframe ownedButton;
    private TextMeshProUGUI ownedButtonTmpro;

    [TabGroup("Save")]
    public ScriptableObjectSaveInfo SaveInfo;

    private const string baseSaveLoc = "ClickerUpgrades";
    public void Setup() {
        ButtonText = $"Cost: {Cost.Value} Amount: {UpgradeAmount.Value} Level : {UseTimes}";
        ownedButton = GUIManager.GetButton(ButtonEnum);
        ownedButton.AddFunction(UseUpgrade);
        ownedButtonTmpro = ownedButton.GetComponentInChildren<TextMeshProUGUI>();
        ownedButtonTmpro.text = ButtonText;
    }
    
    [TabGroup("Save")]
    [Button]
    public void SetSaveFile() {
        if (SaveInfo == null) {
            SaveInfo = new ScriptableObjectSaveInfo(this, baseSaveLoc,this.name);
        }
        else {
            SaveInfo.ModifyInfo(this, baseSaveLoc, this.name);
        }
    }
    [TabGroup("Save")]
    [Button("Save SO")]
    void Save() {
        SetSaveFile();
        SaveInfo.SaveScriptableObject();
    }
    [TabGroup("Save")]
    [Button("Load SO")]
    void Load() {
        SaveInfo.LoadScriptableObject();
    }
    [TabGroup("Save")]
    [Button("Delete SOS")]
    public void DeleteSaves() {
        SaveInfo.ClearScriptableObject();
    }
    
    public void ModifyValuesToMaster(in float cost, in float value) {
        UseTimes = 1;
        //Add a formula for it if you like 
        float newCost = cost;
        float newValue = value;
        //Modify this value however you like
        if (UpgradeType == ClickerUpgradeType.Second) {
            newCost *= 4;
            newValue /= 2;
        }
        Cost = new ATFloat(newCost);
        UpgradeAmount = new AtModifier(newValue, AT_AttributeModifierType.Flat, this);
        Save();
    }

    public void UseUpgrade() {
        if (coin.MainMoney < Cost.Value) return;
        if (UseTimes >= Limit) return;
        UseTimes++;
        coin.IncreaseMainMoney(-Cost.Value);
        coin.UpgradeClickerValue(this);
        //Increase value with curve
        UpdateGuıText();
    }

    void UpdateGuıText() {
        IncreaseValues();
        ButtonText = $"Cost: {Cost.Value} Amount: {UpgradeAmount.Value} Level : {UseTimes}";
        ownedButtonTmpro.text = ButtonText;
    }

    void IncreaseValues() {
        float multi = Curve.Evaluate(UseTimes / Limit);
        AtModifier mod = new AtModifier(Cost.Value + (Cost.Value * multi), AT_AttributeModifierType.Flat);
        Cost.AddModifier(mod);
        UpgradeAmount.Value += UpgradeAmount.Value + (Cost.Value * multi);
    }


    
    
}