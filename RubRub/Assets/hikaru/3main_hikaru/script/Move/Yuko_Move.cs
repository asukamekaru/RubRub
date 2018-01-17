using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuko_Move : MonoBehaviour
{



    public float fSpeed = 3f;
    //float RotateSpeed = 2f;
    float fMoveX = 0f;
    float fMoveZ = 0f;

    //float fAngle = 1f;
    Rigidbody rb;

    const float cfRotspeed = 1000f;     //振りむく、回転する速度
    const float cfDIRE_UP = 0.0f;
    const float cfDIRE_DOWN = 180.0f;
    const float cfDIRE_RIGHT = 90.0f;
    const float cfDIRE_LEFT = 270.0f;

    Animator WalkAnimator;      //アニメーション宣言

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        this.WalkAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //斜め移動阻止処理
        if (fMoveX != 0) fMoveZ = 0;
        if (fMoveZ != 0) fMoveX = 0;

        Vector3 direction = new Vector3(fMoveX, 0, fMoveZ);

        //キャラ回転処理
        //transform.Translate(Quaternion.AngleAxis(fAngle, Vector3.up) * new Vector3(moveX, 0, moveZ));
        //transform.Rotate(0, moveX * RotateSpeed, 0);
        if (fMoveX > 0)  //右移動
        {
            MainManager.LastKey = MainManager.LAST_KEY._KEY_RIGHT_;
            float fStep = cfRotspeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cfDIRE_RIGHT, 0), fStep);
        }
        else if (fMoveX < 0) //左
        {
            MainManager.LastKey = MainManager.LAST_KEY._KEY_LEFT_;
            float fStep = cfRotspeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cfDIRE_LEFT, 0), fStep);
        }
        else if (fMoveZ > 0) //上
        {
            MainManager.LastKey = MainManager.LAST_KEY._KEY_UP_;
            float fStep = cfRotspeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cfDIRE_UP, 0), fStep);
        }
        else if (fMoveZ < 0) //下
        {
            MainManager.LastKey = MainManager.LAST_KEY._KEY_DOWN_;
            float fStep = cfRotspeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, cfDIRE_DOWN, 0), fStep);
        }

        //動いているときアニメ処理
        if (fMoveX != 0 || fMoveZ != 0)
        {
            WalkAnime(true);
        }
        else
        {
            WalkAnime(false);
        }

    }

    public void getControll(Vector3 v)
    {
        float backupx = v.x;
        float buckupz = v.y;

        

    }

    

    //回転処理
    void FixedUpdate()
    {
        rb.velocity = new Vector3(fMoveX, 0, fMoveZ);
    }

    //Animation管理
    void WalkAnime(bool bChange)
    {
        WalkAnimator.SetBool("Walk", bChange);
    }
}
