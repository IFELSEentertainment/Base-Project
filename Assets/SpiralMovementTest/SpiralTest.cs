using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralTest : MonoBehaviour {
    public float MovementForward = 12f;

    private Vector3 movePos;

    [SerializeField] private bool Spiral;

    [SerializeField] private float currentAngle;
    [SerializeField] private float angularSpeed = 1f;
    [SerializeField] private float circleRad = 1f;

    Vector2 fixedPoint = Vector2.zero;
    private void Start() {
        movePos = transform.position;
    }
    void Update()
    {
        movePos = Vector3.zero;
        if (Spiral) {
            currentAngle += angularSpeed * Time.deltaTime;
            Vector2 offset = new Vector2 (Mathf.Sin (currentAngle), Mathf.Cos (currentAngle)) * circleRad;
            movePos.x = (fixedPoint + offset).x * Time.deltaTime;
            movePos.y = (fixedPoint + offset).y * Time.deltaTime;
        }
        movePos.z += (Vector3.forward * Time.deltaTime * MovementForward).z;
    }

    private void LateUpdate() {
        transform.position += movePos;
    }
}
