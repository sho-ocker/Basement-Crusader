using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{

    public int width;
    public int height;
    public int X;
    public int Y;

    public List<Door> doors = new List<Door>();

    private bool updatedDoors = false;

    public Room(int x, int y){
        X = x; 
        Y = y;
    }

    void Start(){
        if (RoomController.instance == null){
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();

        foreach (Door d in ds){
            doors.Add(d);
        }
        RoomController.instance.RegisterRoom(this);
    }

    void Update(){
        if (name.Contains("End") && !updatedDoors){
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors(){
        foreach (Door door in doors){
            switch (door.doorType){
                case Door.DoorType.right:
                    if (GetRight() == null){
                        door.removedDoorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null){
                        door.removedDoorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                   if (GetTop() == null){
                        door.removedDoorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null){
                        door.removedDoorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public Room GetRight(){
        if (RoomController.instance.DoesRoomExists(X + 1, Y)){
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room GetLeft(){
        if (RoomController.instance.DoesRoomExists(X - 1, Y)){
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop(){
        if (RoomController.instance.DoesRoomExists(X, Y + 1)){
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom(){
        if (RoomController.instance.DoesRoomExists(X, Y - 1)){
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCenter(){
        return new Vector3(X * width, Y * height);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
