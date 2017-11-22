using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnime_Con : MonoBehaviour {

    Animator WalkAnimation;     //歩行アニメ

	// Use this for initialization
	void Start () {

        WalkAnimation = GetComponent<Animator>();
	}

    public void StartWalkAnimation()
    {
        if (WalkAnimation)
        {
            WalkAnimation.Play("Walking@loop");
        }
    }

     void changeAnimation(bool banimechange)
    {

        //idol状態からwalkにアニメーション切り替え
        WalkAnimation.SetBool("Walk", banimechange);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            changeAnimation(true);
        }
        else
        {
            changeAnimation(false);
        }
	}
}
