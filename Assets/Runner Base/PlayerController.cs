using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Base.UI;
using Cinemachine;
using DG.Tweening;
using Dreamteck.Splines;
using Dreamteck.Splines.Primitives;
using Lean.Touch;
using Sirenix.OdinInspector;
using UnityEngine;
using static MiniEnum;

[RequireComponent(typeof(SplineFollower))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private SplineComputer splineComputer;
    private SplineFollower splineFollower;
    private Rigidbody _rigidbody;
    
    [SerializeField] private float offsetX, offsetY;
    private float smoothMove;
    
    public bool LeftRightControll;
    
    [ShowIf("LeftRightControll")]
    [SerializeField] private float swipeSpeed;
    [ShowIf("LeftRightControll")]
    [SerializeField] private float xLimit;
    [SerializeField] private float speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        splineComputer = FindObjectOfType<SplineComputer>();
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.followSpeed = 0;
    }
    
    private void Start()
    {
        GUIManager.GetButton(Enum_Menu_MainComponent.BTN_Start).AddFunction(()=> 
                splineFollower.followSpeed = speed
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

    private void Update()
    {
        splineFollower.motion.offset = new Vector2(offsetX,offsetY);
        
        if (LeftRightControll)
        {
            LeftRightSwipe();
        }
    }

    private void LeftRightSwipe()
    {
        var fingers = Lean.Touch.LeanTouch.Fingers;

        if (fingers.Count > 0 & splineComputer != null)
        {
            var move = fingers[0].ScreenDelta.x;
            move = Mathf.Clamp(move, -15, 15);
            
            var delay = 0f;

            if (Math.Abs(move) < 3)
            {
                delay = 0.25f;
            }
            else
            {
                delay = 0.1f;
            }

            DOTween.Kill("turn");
            if (Math.Abs(move) != 0)
            {
                DOTween.To(() => smoothMove, x => smoothMove = x, move, delay).SetId("turn");
            }
            else DOTween.To(() => smoothMove, x => smoothMove = x, 0, delay).SetId("turn");

            offsetX += smoothMove * Time.fixedDeltaTime;
            offsetX = Mathf.Clamp(offsetX, -xLimit, xLimit);

        }
        splineFollower.motion.offset = new Vector2(offsetX, offsetY);
    }


    
    #if UNITY_EDITOR
    [Button]
    [DetailedInfoBox("Kamerada takılma sorunu varsa düzeltir.", "SetCameraSettings")]
    private void SetCameraSettings()
    {
        var camera = FindObjectOfType<CinemachineBrain>();

        camera.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        camera.m_BlendUpdateMethod = CinemachineBrain.BrainUpdateMethod.FixedUpdate;
        FindObjectOfType<SplineComputer>().updateMode = SplineComputer.UpdateMode.FixedUpdate;
        GetComponent<SplineFollower>().physicsMode = SplineTracer.PhysicsMode.Rigidbody;
        GetComponent<SplineFollower>().updateMethod = SplineUser.UpdateMethod.FixedUpdate;
        GetComponent<SplineFollower>().spline = FindObjectOfType<SplineComputer>();
        FindObjectOfType<CinemachineVirtualCamera>().LookAt = transform;
        FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
    }
    #endif
}
