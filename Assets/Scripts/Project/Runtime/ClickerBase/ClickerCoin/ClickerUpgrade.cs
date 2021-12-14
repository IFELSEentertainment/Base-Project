using Base;
using Base.UI;
using TMPro;
using UnityEngine;
public enum ClickerUpgradeType {Click, Second}
[CreateAssetMenu(fileName = "NewClickerUpgrade", menuName = "Clicker/Clicker Upgrade", order = 2)]
public class ClickerUpgrade : ScriptableObject {
        public Enum_Menu_PlayerOverlayComponent ButtonEnum;
        public ClickerUpgradeType UpgradeType;
        public ATFloat Cost;
        public AtModifier UpgradeAmount;
        public AnimationCurve Curve;
        [Range(1, 1000)]
        public int Limit;
        public int UseTimes;

        [SerializeField] private ClickerCoin coin;
        
        private string ButtonText;

        private UI_CButtonTMProSubframe ownedButton;
        private TextMeshProUGUI ownedButtonTmpro;
        public void Setup() {
                ButtonText = $"Cost: {Cost.Value} /n Amount: {UpgradeAmount.Value}";
                ownedButton = GUIManager.GetButton(ButtonEnum);
                ownedButton.AddFunction(UseUpgrade);
                ownedButtonTmpro = ownedButton.GetComponentInChildren<TextMeshProUGUI>();
                ownedButtonTmpro.text = ButtonText;
        }
        
        public void UseUpgrade() {
                if(coin.MainMoney < Cost.Value) return;
                if(UseTimes >= Limit) return;
                UseTimes++;
                coin.IncreaseMainMoney(-Cost.Value);
                coin.UpgradeClickerValue(this);
                //Increase value with curve
                UpdateGuıText();
        }

        void UpdateGuıText() {
                float multi = Curve.Evaluate(UseTimes / Limit);
                AtModifier mod = new AtModifier(Cost.Value + (Cost.Value * multi), AT_AttributeModifierType.Flat);
                Cost.AddModifier(mod);
                // UpgradeAmount = new AtModifier(UpgradeAmount.Value + (UpgradeAmount.Value * multi) * 10, AT_AttributeModifierType.Flat);
                UpgradeAmount.Value += UpgradeAmount.Value + (Cost.Value * multi);
                ButtonText = $"Cost: {Cost.Value} /n Amount: {UpgradeAmount.Value}";
                ownedButtonTmpro.text = ButtonText;
        }
}
