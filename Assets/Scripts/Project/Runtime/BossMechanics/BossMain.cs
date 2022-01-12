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
    public ATFloat BossMainHealth;
    AtModifier DamageTaken;
    public List<BossLimbsMain> Limbs;
    private List<BossLimbsMain> activeLimbs;
    private void Start() {
        DamageTaken = new AtModifier(0, AT_AttributeModifierType.Flat);
        BossMainHealth.AddModifier(DamageTaken);
        Limbs.ForEach(t => BossMainHealth.AddModifier(t.Health));
        B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(ActivateTest, false);
        Debug.Log($"Boss Current Health is {BossMainHealth.Value}");
    }

    void ActivateTest() {
        StartCoroutine(TakeDamageUntilDeath());
    }

    public void TakeLimbDamage(float value) {
        activeLimbs = Limbs.Where(t => t.Health.Value > 0).ToList();
        if (activeLimbs.Count <= 0) { TakeMainHealthDamage(value); return; }
        activeLimbs[Random.Range(0, activeLimbs.Count - 1)].TakeDamage(value);
        CalculateHealth();
    }

    void TakeMainHealthDamage(float value) {
        DamageTaken.Value -= value;
        CalculateHealth();
    }

    IEnumerator TakeDamageUntilDeath() {
        while (BossMainHealth.Value > 0) {
            yield return new WaitForSeconds(1f);
            TakeLimbDamage(50);
        }
    }

    public void CalculateHealth() {
        if (BossMainHealth.Value <= 0) {
            Base_GameManager.instance.ActivateEndgame(true, 1f);
        }
        Debug.Log(BossMainHealth.Value);
    }
    
    private void OnDisable() {
        BossMainHealth.RemoveAllModifiers();
    }



}
