using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.UI;
using Lean.Touch;
using UnityEngine;
using static MiniEnum;
public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {

    }


    private void OnEnable()
    {
        Player.PlayerDead += WhenDead;
        Player.PlayerFail += WhenFail;
        Player.PlayerSuccesful += WhenWin;
        
        Player.OnMouseDown += OnScreenMouseDown;
        Player.OnMouseUp += OnScreenMouseUp;
        Player.OnMouseUp += OnScreenMouseDrag;
    }
    
    private void OnDisable()
    {
        Player.PlayerDead -= WhenDead;
        Player.PlayerFail -= WhenFail;
        Player.PlayerSuccesful -= WhenWin;
        
        Player.OnMouseDown -= OnScreenMouseDown;
        Player.OnMouseUp -= OnScreenMouseUp;
        Player.OnMouseUp -= OnScreenMouseDrag;
    }

    private void OnScreenMouseDown(LeanFinger leanFinger)
    {
  
    }
    
    private void OnScreenMouseUp(LeanFinger leanFinger)
    {

    }
    
    private void OnScreenMouseDrag(LeanFinger leanFinger)
    {
        
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
