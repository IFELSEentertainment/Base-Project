using System.Linq;
using Base;
public static class ClickerExtentions {
    public static ClickerUpgradeMaster Master(this ClickerUpgrade upgrade) {
        ClickerUpgradeMaster master = ExtentionFunctions.FindAssetsByType<ClickerUpgradeMaster>().ToList()[0];
        return master;
    }
}