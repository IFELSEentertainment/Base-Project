using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Base;
using Base.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
public class ClickerPlayer : MonoBehaviour {

    public static ClickerPlayer instance;

    public ClickerCoin Coin;

    private bool CanClick;
    private bool CanCount;

    private Coroutine MoneyPerSecondRoutine;

    private void Awake() {
        if (instance == null) instance = this; else Destroy(this.gameObject);
        B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(ActivatePlayer, false);
    }

    private void Start() {
        GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.PlayerMainMoney).ActivatePart();
        Coin.Setup();
    }

    private void Update() {
        if (!CanClick || !B_GM_GameManager.instance.IsGamePlaying()) return;
        if (Input.GetMouseButtonDown(0)) {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            Coin.IncreaseMainMoney(Coin.IncomePerClick.Value);
            UpdateCurrentMoneyUI();
        }
    }

    private void OnDestroy() {
        instance = null;
    }

    void ActivatePlayer() {
        CanClick = true;
        CanCount = true;
        UpdateIncomeUI();
        MoneyPerSecondRoutine = StartCoroutine(IncreasePlayerMoney());
    }

    public void ChangeClickState(bool value) => CanClick = value;

    IEnumerator IncreasePlayerMoney() {
        while (CanCount) {
            Coin.IncreaseMainMoney(Coin.IncomePerSecond.Value / 10);
            UpdateCurrentMoneyUI();
            yield return new WaitForSeconds(.1f);
        }
    }

    void UpdateIncomeUI() {
        GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.PlayerPerClick).ChangeText($"+{Coin.IncomePerClick.Value}pc");
        GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.PlayerPerSecond).ChangeText($"+{Coin.IncomePerSecond.Value}ps");
    }

    void UpdateCurrentMoneyUI() {
        GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.PlayerMainMoney).ChangeText($"{Coin.MainMoney.ToString("0")}");

    }

}