﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : WallBase
{
    [SerializeField, Range(0, 10)]
    float time = 1;

    [SerializeField]
    Vector3 endPosition;

    //[SerializeField]
    //AnimationCurve curve;

    private float startTime;
    private Vector3 startPosition;

    Rigidbody rigidBody;
    public Vector3 force = new Vector3(0, 10, 0);
    public ForceMode forceMode = ForceMode.VelocityChange;

    // Use this for initialization
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        endPosition = new Vector3(this.transform.position.x, 1, this.transform.position.z);

    }

    void OnEnable()
    {
        if (time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
    }

    void Update()
    {
        //翁長君が触った所------------------------------------------
        if(WallType != -1)
        {
            WallTagChange(WallType, this.gameObject);
            if (WallType > 0)
            {
                if (NearObjectRetrieval(this.gameObject, TagName[WallType -1]))//TagNameの要素が２つしかないため　WallType - 1
                {
                    NearTriggerObject = WallType;
                }
            }
            ObjectCreate(this.gameObject, WallName[WallType - 1], NearTriggerObject);
            WallType = -1;
            //Debug.Log("NearTriggerObject " + NearTriggerObject);
        }


        //----------------------------------------------------------
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > time)
        {
            transform.position = endPosition;
            enabled = false;
        }

        var rate = diff / time;
        //var pos = curve.Evaluate(rate);

        transform.position = Vector3.Lerp(startPosition, endPosition, rate);
        //transform.position = Vector3.Lerp (startPosition, endPosition, pos);
    }



    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            startPosition = transform.position;
        }

        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
#endif
        Gizmos.DrawSphere(endPosition, 0.1f);
        Gizmos.DrawSphere(startPosition, 0.1f);

        Gizmos.DrawLine(startPosition, endPosition);
    }

}
