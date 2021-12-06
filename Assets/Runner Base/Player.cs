using System;
using Base;
using Lean.Touch;
using UnityEngine;
using static  MiniEnum;
public class Player : MonoBehaviour
{
    private int health;
    public int Health => health;
    public static Action<int> ChangeHealth;
    
    public static Action<LeanFinger> OnMouseDown;
    public static Action<LeanFinger> OnMouseUp;
    public static Action<LeanFinger> OnMouseDrag;

    public static Action PlayerSuccesful;
    public static Action PlayerFail;
    public static Action PlayerDead;

    public static bool OnDown;
    
    private void Start()
    {
        B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.AddFunction(() => PlayerSuccesful.Invoke(),true);
        B_CES_CentralEventSystem.OnBeforeLevelDisableNegative.AddFunction(() => PlayerFail.Invoke(),true);
    }

    private void OnEnable()
    {
        Player.PlayerDead += WhenDead;
        Player.PlayerFail += WhenFail;
        Player.PlayerSuccesful += WhenWin;
        
        Lean.Touch.LeanTouch.OnFingerDown += OnScreenMouseDown;
        Lean.Touch.LeanTouch.OnFingerUp += OnScreenMouseUp;
        
        //Lean.Touch.LeanTouch.OnFingerExpired += OnScreenMouseDrag;
    }
    
    private void OnDisable()
    {
        Player.PlayerDead -= WhenDead;
        Player.PlayerFail -= WhenFail;
        Player.PlayerSuccesful -= WhenWin;
        
        Lean.Touch.LeanTouch.OnFingerDown -= OnScreenMouseDown;
        Lean.Touch.LeanTouch.OnFingerUp -= OnScreenMouseUp;
        
        //Lean.Touch.LeanTouch.OnFingerExpired -= OnScreenMouseDrag;
    }

    private void OnScreenMouseDown(LeanFinger leanFinger)
    {
        if(leanFinger.IsOverGui | !B_GM_GameManager.instance.IsGamePlaying()) return;
        OnMouseDown.Invoke(leanFinger);
        OnDown = true;
    }
    
    private void OnScreenMouseUp(LeanFinger leanFinger)
    {
        if(leanFinger.IsOverGui | !B_GM_GameManager.instance.IsGamePlaying()) return;
        OnMouseUp.Invoke(leanFinger);
        OnDown = false;
    }
    
    private void OnScreenMouseDrag(LeanFinger leanFinger)
    {
        OnMouseDrag.Invoke(leanFinger);
    }
    
    private void WhenFail()
    {
        
    }

    private void WhenWin()
    {
        
    }

    private void WhenDead()
    {
        
    }
}
