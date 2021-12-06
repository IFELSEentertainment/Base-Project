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
    public static Action<ChracterStat> ChangeAnim;
    public ParticleSystem fireParticle;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {
        GUIManager.GetButton(Enum_Menu_MainComponent.BTN_Start).AddFunction(()=> 
            anim.SetBool("Start",true)
        );
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
        anim.SetBool("Fire",true);
        //FireAnimTrigger(true);
    }
    
    private void OnScreenMouseUp(LeanFinger leanFinger)
    {
        anim.SetBool("Fire",false);
        FireAnimTrigger(0);
        //FireAnimTrigger(false,true);
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

    public void FireAnimTrigger(int a)
    {
        // if(!immediately) await Task.Delay(1250);
        
        if (a == 1)
        {
            if(Player.OnDown)
                fireParticle.Play();
        }
        else fireParticle.Stop();
    }
    
}
