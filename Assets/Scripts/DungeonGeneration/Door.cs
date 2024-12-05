using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{
    
    public enum DoorType{
        left, right, top, bottom
    }

    public DoorType doorType;
    public GameObject doorCollider;
    public GameObject removedDoorCollider;

    private void Start(){
    }
}
