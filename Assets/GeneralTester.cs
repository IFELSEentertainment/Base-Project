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
            Enum_Particles.CubeExplosion.SpawnAParticle(transform.position).PlayParticle();
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(1f);
        B_GameControl.ActivateEndgame(true, 2);
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
        Enum_Menu_PlayerOverlayComponent.TestScore.GetText().ChangeText(PlayerCoin.DataToString());
    }
}
