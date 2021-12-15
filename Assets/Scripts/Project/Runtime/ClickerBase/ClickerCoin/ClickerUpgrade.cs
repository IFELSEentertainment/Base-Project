using System.Runtime.CompilerServices;
using Base;
using Base.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public enum ClickerUpgradeType { Click, Second }
[CreateAssetMenu(fileName = "NewClickerUpgrade", menuName = "Clicker/Clicker Upgrade", order = 2)]
public class ClickerUpgrade : ScriptableObject {
    [TabGroup("Upgrade")]
    public Enum_Menu_PlayerOverlayComponent ButtonEnum;
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

    private const string baseSaveLoc = "Upgrades";
    public void Setup() {
        ButtonText = $"Cost: {Cost.Value} /n Amount: {UpgradeAmount.Value}";
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
    void save() {
        SaveInfo.SaveScriptableObject();
    }
    [TabGroup("Save")]
    [Button("Load SO")]
    void load() {
        SaveInfo.LoadScriptableObject();
    }
    [TabGroup("Upgrade")]
    [Button]
    void ModifyValuesToMaster() {
        Cost.BaseValue = Cost.Value * this.Master().BaseModifier;
        UpgradeAmount.Value *= this.Master().BaseModifier;
    }

    public void UseUpgrade() {
        if (coin.MainMoney < Cost.Value) return;
        if (UseTimes >= Limit) return;
        UseTimes++;
        coin.IncreaseMainMoney(-Cost.Value * this.Master().BaseModifier);
        coin.UpgradeClickerValue(this);
        //Increase value with curve
        UpdateGuıText();
    }

    void UpdateGuıText() {

        IncreaseValues();

        ButtonText = $"Cost: {Cost.Value} /n Amount: {UpgradeAmount.Value}";
        ownedButtonTmpro.text = ButtonText;
    }

    void IncreaseValues() {
        float multi = Curve.Evaluate(UseTimes / Limit);
        AtModifier mod = new AtModifier(Cost.Value + (Cost.Value * multi), AT_AttributeModifierType.Flat);
        Cost.AddModifier(mod);
        UpgradeAmount.Value += UpgradeAmount.Value + (Cost.Value * multi);
    }


    
    
}