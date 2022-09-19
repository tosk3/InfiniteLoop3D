using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private GameObject[,] hexList;
    [SerializeField] private Vector2Int[,] gridPos;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridXPosOffset;
    [SerializeField] private float gridYPosOffset;
    [SerializeField] private float gridXOffset;
    [SerializeField] private float gridYOffset;

    // Start is called before the first frame update
    void Start()
    {
        gridPos = new Vector2Int[gridWidth, gridHeight];
        hexList = new GameObject[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                gridPos[i, j] = new Vector2Int(i, j);
            }
        }

        gridXOffset = hexPrefab.GetComponentInChildren<Renderer>().bounds.size.x + gridXPosOffset;
        gridYOffset = hexPrefab.GetComponentInChildren<Renderer>().bounds.size.z + gridYPosOffset;

        SpawnGridHexes();

    }

    private void SpawnGridHexes()
    {
        for(int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (j % 2 == 0)
                {
                    hexList[i, j] = Instantiate(hexPrefab, new Vector3(i * gridXOffset, 0, j * (3 * gridYOffset / 4)), Quaternion.identity);
                }
                else
                {
                    hexList[i, j] = Instantiate(hexPrefab, new Vector3(i * gridXOffset + gridXOffset/2, 0, j * (3 * gridYOffset / 4)), Quaternion.identity);
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
