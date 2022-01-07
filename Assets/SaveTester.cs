using System.Collections;
using System.Collections.Generic;
using Base;
using Base.UI;
using UnityEditorInternal;
using UnityEngine;
using static Enum_MainSave;

public class SaveTester : MonoBehaviour
{
    IEnumerator Start() {
        for (int i = 0; i < 5; i++) 
            B_VFM_EffectsManager.instance.SpawnAParticle(Enum_Particles.CubeExplosion, transform.position).PlayParticle().transform.SetParent(transform);
        yield return new WaitForSeconds(5f);
        Base_GameManager.instance.ActivateEndgame(true,2f);
    }
    
}
