using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
[Serializable]
public class ClickerEditorFunctions {
    
    private List<ClickerUpgrade> allUpgrades;

    private List<string> Upgrades;
    [OnValueChanged("ChangeEdit")]
    [ValueDropdown("allUpgrades")]
    public ClickerUpgrade Upgrade;
    [ShowIf("@Upgrade != null")]
    [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
    public ClickerUpgrade Upgrade1;
    public ClickerEditorFunctions(OdinMenuTree tree) {
        allUpgrades = new List<ClickerUpgrade>();
        allUpgrades = ExtentionFunctions.FindAssetsByType<ClickerUpgrade>();
        tree.AddAssetAtPath("Master", ExtentionFunctions.FindAssetPath<ClickerUpgradeMaster>());
        if(allUpgrades.IsNullOrEmpty()) return;
    }

    void ChangeEdit() {
        Upgrade1 = Upgrade;
    }
}

