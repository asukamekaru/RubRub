using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    static Animator _animator;

    [SerializeField]
    public cameraScript camerascript;
    public MainManager mainmanager;
        
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}

    void Update()
    {
        if (camerascript.iCameraView <= camerascript.CAMERA_SCALE_DEAD) _animator.SetBool("Dead", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MainManager.ChangeStatus(MainManager.STATUS._GAME_OVER_);
        }
    } 
}
