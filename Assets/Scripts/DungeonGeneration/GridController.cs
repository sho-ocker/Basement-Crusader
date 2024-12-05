﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour{
    
    public Room room;

    [System.Serializable]
    public struct Grid{
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    void Awake(){
        room = GetComponentInParent<Room>();
        grid.columns = room.width - 2;
        grid.rows = room.height - 2;
        GenerateGrid();
    }

    public void GenerateGrid(){
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int i = 0; i < grid.rows; i++){
            for (int j = 0; j < grid.columns; j++){
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector2(j - (grid.columns - grid.horizontalOffset), i - (grid.rows - grid.verticalOffset));
                go.name = "X: " + j + ", Y: " + i;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
