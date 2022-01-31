using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Base;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class BossMain : MonoBehaviour {

    private Action OnBossDied;

    public ATFloat BossMainHealth;
    AtModifier DamageTaken;
    public List<BossLimbsMain> Limbs;
    private bool destroyed = false;
    // private List<BossLimbsMain> activeLimbs;
    private void Start() {
        DamageTaken = new AtModifier(0, AT_AttributeModifierType.Flat);
        BossMainHealth.AddModifier(DamageTaken);
        Limbs.ForEach(t => t.SetupLimb(this));
        Limbs.ForEach(t => BossMainHealth.AddModifier(t.Health));
        B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(ActivateTest, false);
        Debug.Log($"Boss Current Health is {BossMainHealth.Value}");
    }

    void ActivateTest() {
        
    }

    void TakeMainHealthDamage(float value) {
        if(destroyed) return;
        DamageTaken.Value -= value;
        CalculateHealth();
    }



    public void CalculateHealth() {
        if (BossMainHealth.Value <= 0) {
            destroyed = true;
            Base_GameManager.instance.ActivateEndgame(true, 1f);
            OnBossDied?.Invoke();
        }
        Debug.Log(BossMainHealth.Value);
    }

    private void OnDisable() {
        BossMainHealth.RemoveAllModifiers();
    }
    
    
    
    //Testing part

    // public void TakeLimbDamage(float value) {
    //     activeLimbs = Limbs.Where(t => t.Health.Value > 0).ToList();
    //     if (activeLimbs.Count <= 0) { TakeMainHealthDamage(value); return; }
    //     activeLimbs[Random.Range(0, activeLimbs.Count - 1)].TakeDamage(value);
    //     CalculateHealth();
    // }

    private void OnMouseDown() {
        TakeMainHealthDamage(10);
    }

}
