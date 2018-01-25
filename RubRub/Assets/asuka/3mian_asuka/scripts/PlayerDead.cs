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
        /*if (camerascript.iCameraView <= camerascript.CAMERA_SCALE_DEAD)*/ _animator.SetBool("Dead", true);

        // ここに到達直後はnormalizedTimeが"Default"の経過時間を拾ってしまうので、Resultに遷移完了するまではreturnする。
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("GoDown"))
        {
            Debug.Log("1");
            return false;
        }
        // 待機時間を作りたいならば、ここの値を大きくする。
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            Debug.Log("2");
            return false;
        }
        else
        {
            Time.timeScale = 0;//時間を止める
            Debug.Log("3");
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
