using System;
using System.Collections.Generic;
using Base.UI;
using UnityEngine;
public class ClickerButtonManager : MonoBehaviour {

    public List<ClickerUpgrade> Upgrades;

    private void Start() {
        Upgrades.ForEach(t => t.Setup());
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).DeactivatePart();
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OpenUpgradesPanel).AddFunction(OpenPanel);
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.ClosePanelButton).AddFunction(ClosePanel);
    }

    void OpenPanel() {
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).ActivatePart();
    }

    void ClosePanel() {
        GUIManager.GetPanel(Enum_Menu_PlayerOverlayComponent.UpgradesPanel).DeactivatePart();
    }
}