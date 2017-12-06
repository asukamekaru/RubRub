using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    Animator _animator;

	// Use this for initialization
	void Start () {
        this._animator = GetComponent<Animator>();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") _animator.SetBool("Dead", true);
    } 
}
