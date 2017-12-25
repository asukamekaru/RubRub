//=================================================
// 音を管理するスクリプト
// AsukaMekaru
// 2017/12/25
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class AudioSet
{
    public AudioClip seClip;
    public float volume;
}

public class soundManager : MonoBehaviour
{
    #region //シングルトン
    public static soundManager Instance{ get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    #endregion
    [SerializeField]
    public List<AudioSet> seList = new List<AudioSet>();
    public AudioSource bgmSource;//BGM's
    public AudioSource seSource;//SE's
    public AudioClip[] bgmClip;

    //音を再生
    public void PlaySound(int seNum, bool isPlaying)
    {
        if (isPlaying)
        {
            //seSource.volume =  seList[seNum].volume;
            if (!seSource.isPlaying)
            {
                seSource.PlayOneShot(seList[seNum].seClip, seList[seNum].volume);
            }
        }
        else
        {
            seSource.PlayOneShot(seList[seNum].seClip, seList[seNum].volume);
        }
    }

    //音を変える
    public void ChangeBgm(int bgmNum)
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip[bgmNum];
            bgmSource.Play();
        }
    }

    //音を止める
    public void StopBgm()
    {
        bgmSource.Stop();
    }
}
