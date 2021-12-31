using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;
using static Enum_MainSave;

public class SaveTester : MonoBehaviour
{
    void Start() {
        Debug.Log(PlayerCoin.GetData());
        PlayerCoin.SetData(100);
        Debug.Log(PlayerCoin.GetData());
    }
    
}
