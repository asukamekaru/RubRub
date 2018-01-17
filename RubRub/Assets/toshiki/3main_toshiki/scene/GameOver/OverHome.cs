using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverHome : MonoBehaviour
{

    public void SceneChangeHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
