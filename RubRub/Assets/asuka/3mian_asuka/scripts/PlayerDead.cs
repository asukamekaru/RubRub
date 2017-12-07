using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    static Animator _animator;
        
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _animator.SetBool("Dead", true);
           MainManager.NowStatus = MainManager.STATUS._GAME_OVER_;
           
        }
    } 
}
