using System.Collections;
using System.Collections.Generic;
using Base;
using Base.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClickerCoin", menuName = "Clicker/Create New Coin", order = 1)]
public class ClickerCoin : SerializedScriptableObject {

    [SerializeField] Enum_Menu_PlayerOverlayComponent MainMoneyEnum;
    [SerializeField] Enum_Menu_PlayerOverlayComponent PerClickEnum;
    [SerializeField] Enum_Menu_PlayerOverlayComponent PerSecondEnum;

    public float MainMoney;
    public ATFloat IncomePerClick;
    public ATFloat IncomePerSecond;

    TextMeshProUGUI mainMoneyText;
    TextMeshProUGUI perClickText;
    TextMeshProUGUI perSecondText;

    public ScriptableObjectSaveInfo info;

    public void Setup() {
        mainMoneyText = GUIManager.GetText(MainMoneyEnum).TextComponent;
        perClickText = GUIManager.GetText(PerClickEnum).TextComponent;
        perSecondText = GUIManager.GetText(PerSecondEnum).TextComponent;
    }

    public void IncreaseMainMoney(float value) {
        MainMoney += value;
        UpdateMainMoney();
    }

    public void UpgradeClickerValue(ClickerUpgrade upgrade) {
        switch (upgrade.UpgradeType) {
            case ClickerUpgradeType.Click:
                IncomePerClick.AddModifier(upgrade.UpgradeAmount);
                break;
            case ClickerUpgradeType.Second:
                IncomePerSecond.AddModifier(upgrade.UpgradeAmount);
                break;
        }
        UpdateIncome();
    }

    void UpdateMainMoney() {
        mainMoneyText.text = $"{MainMoney.ToString("0")}";
    }

    void UpdateIncome() {
        perClickText.text = $"+{IncomePerClick.Value.ToString("0")}PC";
        perSecondText.text = $"+{IncomePerSecond.Value.ToString("0")}PS";
    }

    public void SaveSO() {
        if (info == null) {
            info = new ScriptableObjectSaveInfo(this, "Coins", "MainCoin");
        }
        else {
            info.ModifyInfo(this, "Coins","MainCoin");
        }
        info.SaveScriptableObject();
    }

    public void LoadSO() {
        info.LoadScriptableObject();
    }

    [Button]
    public void DeleteSaves() {
        MainMoney = 0;
        IncomePerClick = new ATFloat(1);
        IncomePerSecond = new ATFloat(0);
        info.ClearScriptableObject();
        info.SaveScriptableObject();
    }
    
}