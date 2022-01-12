using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class BossLimbsMain : MonoBehaviour {
    public string LimbName = "Limb";
    public AtModifier Health;
    private BossMain Parent;

    public void SetupLimb(BossMain parent) {
        Parent = parent;
    }

    public void TakeDamage(float value) {
        Health.Value -= value;
        if (Health.Value <= 0) Health.Value = 0;
    }

}
