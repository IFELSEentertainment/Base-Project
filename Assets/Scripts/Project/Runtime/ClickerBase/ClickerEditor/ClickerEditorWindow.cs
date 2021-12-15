using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class ClickerEditorWindow : OdinMenuEditorWindow
{
    [MenuItem("Tools/Clicker/Clicker Editor %F3")]
    private static void OpenWindow() {
        GetWindow<ClickerEditorWindow>().Show();
    }
    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree();
        tree.Add("Clicker Editor", new ClickerEditorFunctions(tree));
        return tree;
    }
}
