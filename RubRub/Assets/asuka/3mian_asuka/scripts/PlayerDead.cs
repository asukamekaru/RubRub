using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{

    Animator _animator;
    AnimatorStateInfo animInfo;
    
    cameraScript camerascript;
    MainManager mainmanager;
    soundManager soundmanager;

    // Use this for initialization
    void Start()
    {
        soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
        mainmanager = GameObject.Find("MainManager").GetComponent<MainManager>();
        camerascript = GameObject.Find("Main Camera").GetComponent<cameraScript>();
        _animator = GetComponent<Animator>();
        animInfo = _animator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    public bool DEAD()
    {
        /*if (camerascript.iCameraView <= camerascript.CAMERA_SCALE_DEAD)*/ _animator.SetBool("Dead", true);

        // ここに到達直後はnormalizedTimeが"Default"の経過時間を拾ってしまうので、Resultに遷移完了するまではreturnする。
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("GoDown"))
        {
            return false;
        }
        // 待機時間を作りたいならば、ここの値を大きくする。
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.2f) soundmanager.PlaySound(15, true);
            return false;
        }
        else
        {
            Time.timeScale = 0;//時間を止める
            return true;
        }
        //return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "sakura")
        {
            MainManager.ChangeStatus(MainManager.STATUS._GAME_OVER_);
        }
    }
}
