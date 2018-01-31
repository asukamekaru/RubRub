using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireJudge : MonoBehaviour
{
    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("入った");
        CubeControl2 cubeControl2;
        GameObject DeleteObject = null;
        if (collision.gameObject.tag == "sakura" || collision.gameObject.tag == "Enemy")
        {
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag("FireWall"))
            {
                cubeControl2 = obs.GetComponent<CubeControl2>();
                if (cubeControl2.NearTriggerObject != -1)
                {
                    if (cubeControl2.SetFire != null)
                    {
                        DeleteObject = cubeControl2.SetFire;
                    }
                }
            }
         
            if(DeleteObject != null)
            {
                SetFireDelete.DeleteFlg = true;
                //Destroy(DeleteObject);
            }
        }
    }
}

