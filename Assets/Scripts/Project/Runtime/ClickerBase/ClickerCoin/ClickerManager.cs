using System;
using System.Collections.Generic;
using Base;
using Base.UI;
using UnityEngine;
public class ClickerManager : MonoBehaviour {

    public static ClickerManager instance;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    void OnDisable() {
        instance = null;
    }

    List<ClickerUpgrade> Upgrades;
    private Dictionary<Enum_ClickerUpgrades, ClickerUpgrade> upgradesDic;
    private void Start() {
        Upgrades = ExtentionFunctions.FindAssetsByType<ClickerUpgrade>();
        Upgrades.ForEach(t => t.Setup());
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).DeactivatePart();
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OpenUpgradesPanel).AddFunction(OpenPanel);
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.ClosePanelButton).AddFunction(ClosePanel);
    }

    void OpenPanel() {
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).ActivatePart();
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OpenUpgradesPanel).DeactivatePart();
    }

    void ClosePanel() {
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).DeactivatePart();
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OpenUpgradesPanel).ActivatePart();
    }
    
    public ClickerUpgrade GetUpgrade(in Enum_ClickerUpgrades toPullEnum) {
        return upgradesDic[toPullEnum];
    }
}