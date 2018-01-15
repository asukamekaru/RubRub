using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{

    Animator _animator;
    AnimatorStateInfo animInfo;

    [SerializeField]
    public cameraScript camerascript;
    public MainManager mainmanager;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        animInfo = _animator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    public bool DEAD()
    {
        if (camerascript.iCameraView <= camerascript.CAMERA_SCALE_DEAD) _animator.SetBool("Dead", true);


        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))    // ここに到達直後はnormalizedTimeが"Default"の経過時間を拾ってしまうので、Resultに遷移完了するまではreturnする。
            return false;
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)    // 待機時間を作りたいならば、ここの値を大きくする。
            return false;
        if (animInfo.normalizedTime <= 1.0f)
        {
            Debug.Log("DEAD");
           // return true;
        }
        else
        {
            Debug.Log("DEAD");
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MainManager.ChangeStatus(MainManager.STATUS._GAME_OVER_);
        }
    }
}
