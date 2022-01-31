using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class BossLimbsMain : MonoBehaviour {
    public string LimbName = "Limb";
    public AtModifier Health;
    private BossMain Parent;

    private bool destroyed = false;

    public void SetupLimb(BossMain parent) {
        Parent = parent;
    }

    private void OnMouseDown() {
        TakeDamage(10f);
    }

    public void TakeDamage(float value) {
        if(destroyed) return;
        Health.Value -= value;
        if (Health.Value <= 0) Die();
        Parent.CalculateHealth();
    }

    void Die() {
        destroyed = true;
        Health.Value = 0;
    }

}
