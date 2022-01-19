using System.Collections;
using System.Collections.Generic;
using Base;
using Base.UI;
using UnityEditorInternal;
using UnityEngine;
using static Enum_MainSave;

public class GeneralTester : MonoBehaviour
{
    void Start() {
        B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(Testers, false);
        
    }

    void Testers() {
        TestBurst().RunCoroutine(2);
        TestSave().RunCoroutine();
        B_ExtentionFunctions.RunWithDelay(TestScoreChange, 2f);
    }

    IEnumerator TestBurst() {
        for (int i = 0; i < 5; i++) {
            B_VFM_EffectsManager.instance.SpawnAParticle(Enum_Particles.CubeExplosion, transform.position).PlayParticle().transform.SetParent(transform);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(1f);
        B_GameManager.instance.ActivateEndgame(true,2f);
    }

    IEnumerator TestSave() {
        string _oldPlayerCoin = PlayerCoin.DataToString();
        Debug.Log($"Current Player Coin is : {PlayerCoin.DataToString()}");
        yield return new WaitForSeconds(1f);
        Debug.Log($"Saving Player Coin");
        PlayerCoin.SetData(5);
        yield return new WaitForSeconds(.5f);
        Debug.Log("Saving Completed");
        yield return new WaitForSeconds(.1f);
        Debug.Log($"Old Player Coin Was {_oldPlayerCoin} // Current Player Coin is : {PlayerCoin.DataToString()}");
        yield return null;
    }

    void TestScoreChange() {
        B_GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.TestScore).ChangeText(PlayerCoin.DataToString());
    }
}
