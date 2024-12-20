﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    public static CameraController instance;
    public Room currentRoom;
    public float moveSpeed;

    void Awake(){
        instance = this;
    }

    void Update(){
        UpdatePosition();
    }

    void UpdatePosition(){
        if (!currentRoom)
            return;

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    Vector3 GetCameraTargetPosition(){
        if (!currentRoom)
            return Vector3.zero;

        Vector3 targetPos = currentRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene(){
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
