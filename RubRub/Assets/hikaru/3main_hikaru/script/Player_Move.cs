﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

    float fMoveX = 0;       //初期位置_Ｘ座標
    float fMoveZ = 0;       //初期位置_Ｚ座標
    const float cfMOVE_SPEED = 0.05f;        //移動加速速度

    Transform tTarget;
    float fRotspeed = 500f;     //回転速度

    //プレイヤーの向き
    const float cDIRE_UP = 0.0f;
    const float cDIRE_DOWN = 180.0f;
    const float cDIRE_RIGHT = 90.0f;
    const float cDIRE_LEFT = 270.0f;

    Animator WalkAnimator;      //アニメーション宣言

    // Use this for initialization
    void Start()
    {
        //tTarget = tTarget = GameObject.Find("Yuko_sum_humanoid").transform;
        this.WalkAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))   //上移動
        {
            fMoveZ += cfMOVE_SPEED;
            float step = fRotspeed * Time.deltaTime;
            //指定した方向にゆっくり回転する場合
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cDIRE_UP, 0), step);
            WalkAnime(true);
        }

        else if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))    //下移動
        {
            fMoveZ -= cfMOVE_SPEED;
            float step = fRotspeed * Time.deltaTime;
            //指定した方向にゆっくり回転する場合
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cDIRE_DOWN, 0), step);
            WalkAnime(true);
        }

        else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))   //右移動
        {
            fMoveX += cfMOVE_SPEED;
            float step = fRotspeed * Time.deltaTime;
            //指定した方向にゆっくり回転する場合
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cDIRE_RIGHT, 0), step);
            WalkAnime(true);
        }

        else if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))    //左移動
        {
            fMoveX -= cfMOVE_SPEED;
            float step = fRotspeed * Time.deltaTime;
            //指定した方向にゆっくり回転する場合
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cDIRE_LEFT, 0), step);
            WalkAnime(true);
        }
        else // キー入力無し
        {
            WalkAnime(false);
        }
        //移動値代入
        transform.position = new Vector3(fMoveX, 0, fMoveZ); 
    }

    //Animation管理
    void WalkAnime(bool bChange)
    {
        WalkAnimator.SetBool("Walk", bChange);
    }
}
