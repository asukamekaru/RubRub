using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //回転の変数
    public float MoveTotalRotation = 0.0f;
    float MoveRotationSpeed = 2.0f;
    float MaxRotation = 360.0f;
    //接近するタイミング
    float MovePoint = 0.0f;
    //接近の変数
    float MovePositionMaxY = -1.0f;
    float MovePositionMaxZ = 3.0f;
    float MovePositionYSpeed = 0.01f;
    float MovePositionZSpeed = 0.03f;
    float CameraNowPositionY = 0.0f;
    float CameraNowPositionZ = 0.0f;
    void Update()
    {
        //回転
        if (MoveTotalRotation < MaxRotation)
        {
            MoveTotalRotation += MoveRotationSpeed;
            transform.Rotate(new Vector3(0.0f, MoveRotationSpeed, 0.0f));

            if (MoveTotalRotation >= MaxRotation) MoveTotalRotation = MaxRotation;
        }
        //接近
        if(MoveTotalRotation >= MovePoint)
        {
            if(CameraNowPositionY > MovePositionMaxY)
            {
                CameraNowPositionY -= MovePositionYSpeed;
            }
            if(CameraNowPositionZ < MovePositionMaxZ)
            {
                CameraNowPositionZ += MovePositionZSpeed;
            }
            transform.position = new Vector3(0.0f, CameraNowPositionY, CameraNowPositionZ);
        }
       
    }
}
