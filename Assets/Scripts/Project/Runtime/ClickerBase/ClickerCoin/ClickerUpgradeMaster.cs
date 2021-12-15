using UnityEngine;
[CreateAssetMenu(fileName = "NewClickerMaster", menuName = "Clicker/Create Master", order = 0)]
public class ClickerUpgradeMaster : ScriptableObject {
    [Range(1, 100)]
    public float BaseModifier = 1;
}