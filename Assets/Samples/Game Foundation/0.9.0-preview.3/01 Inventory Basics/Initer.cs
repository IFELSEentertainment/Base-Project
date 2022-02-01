using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Data;
using UnityEngine.GameFoundation.DefaultLayers;
using UnityEngine.GameFoundation.DefaultLayers.Persistence;
using UnityEngine.Promise;

public class Initer : MonoBehaviour {

    public IDataPersistence localPersistence;

    public MemoryDataLayer DataLayer;
    
    void Start() {
        // JsonDataSerializer ds = new JsonDataSerializer();
        // localPersistence = new LocalPersistence("InventoryTest", ds);
        DataLayer = new MemoryDataLayer();
        GameFoundationSdk.Initialize(DataLayer);
        // localPersistence.Save();

    }   

    void OnInitSucceeded()
    {
        Debug.Log("Game Foundation is successfully initialized");
    }

    // Called if Game Foundation initialization fails 
    void OnInitFailed(Exception error)
    {
        Debug.LogException(error);
    }

    private void OnDisable() {
        
        GameFoundationSdk.Uninitialize();
    }
}
