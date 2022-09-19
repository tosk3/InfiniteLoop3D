using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public event System.EventHandler<OnSpawnArgs> OnHexSpawn;
    public class OnSpawnArgs { public List<GameObject> spawnedHexes; }

    [SerializeField] private HexSpawnerData_SO hexSpawnerData_SO;
    [SerializeField] private ManagerHelper helper;
    [SerializeField] private GameObject selectedHex;
    [SerializeField] private GameObject baseHex;

    [SerializeField] private List<GameObject> hexList;
    [SerializeField] private float gridXOffset;
    [SerializeField] private float gridYOffset;


    // Start is called before the first frame update
    void Start()
    {
        gridXOffset = hexSpawnerData_SO.hexPrefab.GetComponentInChildren<Renderer>().bounds.size.x ;
        gridYOffset = hexSpawnerData_SO.hexPrefab.GetComponentInChildren<Renderer>().bounds.size.z ;
        helper.selectionManager.OnHexSelection += SelectionManager_OnHexSelection;
        helper.dayManager.OnDayPassed += DayManager_OnDayPassed;
        AttemptSpawn(baseHex.transform.position);
    }

    private void DayManager_OnDayPassed(object sender, DayCycle.OnDayPassedArgs e)
    {
        AttemptSpawn(baseHex.transform.position);
    }

    public void AttemptSpawn(Vector3 pos)
    {
        AttemptSpawnNeighboursHexes(pos);
    }

    private void SelectionManager_OnHexSelection(object sender, SelectionManager.OnHexSelectionArgs e)
    {
        selectedHex = e.currentSelection;
    }

    public void SpawnNeighboursHexes(Vector3 pos)
    {
        //Find Neighbour locations
        //for each neighbour loc
        List<Vector3> neighbours = CreateNeighbourPositions(pos);
        foreach (Vector3 neighbor in neighbours)
        {
            if (!Physics.CheckSphere(neighbor, hexSpawnerData_SO.hexPrefab.GetComponent<SphereCollider>().radius))
            {
                GameObject hex = Instantiate(hexSpawnerData_SO.hexPrefab, new Vector3(neighbor.x, 0, neighbor.z), Quaternion.identity);
                hex.GetComponent<Hex>().SetupHex(helper, HexGenerator.CreateRandomHex(helper, (HexDataList_SO)Resources.Load(typeof(HexDataList_SO).ToString())));
                hexList.Add(hex);
            }
        }
        OnHexSpawn?.Invoke(this, new OnSpawnArgs() { spawnedHexes = hexList });
    }
    public void AttemptSpawnNeighboursHexes(Vector3 pos)
    {
        //Find Neighbour locations
        //for each neighbour loc
        List<Vector3> neighbours = CreateNeighbourPositions(pos);
        foreach (Vector3 neighbor in neighbours)
        {
            bool spawn = hexSpawnerData_SO.chanceOfSpawn[TotoUtils.RandomIntBasedOnWeight(hexSpawnerData_SO.chanceOfSpawnWeight, 0)];
            if (hexSpawnerData_SO.chanceOfSpawn[TotoUtils.RandomIntBasedOnWeight(hexSpawnerData_SO.chanceOfSpawnWeight, 0)])
            {
                if (!Physics.CheckSphere(neighbor, hexSpawnerData_SO.hexPrefab.GetComponent<SphereCollider>().radius))
                {
                    GameObject hex = Instantiate(hexSpawnerData_SO.hexPrefab, new Vector3(neighbor.x, 0, neighbor.z), Quaternion.identity);
                    hex.GetComponent<Hex>().SetupHex(helper, HexGenerator.CreateRandomHex(helper, (HexDataList_SO)Resources.Load(typeof(HexDataList_SO).ToString())));
                    hexList.Add(hex);
                }
            }  
        }
        OnHexSpawn?.Invoke(this, new OnSpawnArgs() { spawnedHexes = hexList });
    }
    public void SpawnNeighboursHexes()
    {
        if (selectedHex == null) return; 
        
        List<Vector3> neighbours = CreateNeighbourPositions(selectedHex.transform.position);
        foreach (Vector3 neighbor in neighbours)
        {
            if (!Physics.CheckSphere(neighbor, hexSpawnerData_SO.hexPrefab.GetComponent<SphereCollider>().radius))
            {
                GameObject hex = Instantiate(hexSpawnerData_SO.hexPrefab, new Vector3(neighbor.x, 0, neighbor.z), Quaternion.identity);
                hex.GetComponent<Hex>().SetupHex(helper, HexGenerator.CreateRandomHex(helper,(HexDataList_SO)Resources.Load(typeof(HexDataList_SO).ToString())));
                hexList.Add(hex);
            }
        }
        OnHexSpawn?.Invoke(this, new OnSpawnArgs() { spawnedHexes = hexList });
    }

    private List<Vector3> CreateNeighbourPositions(Vector3 pos)
    {
        List<Vector3> neighbours = new List<Vector3>();
        //top
        neighbours.Add(new Vector3(pos.x + gridXOffset + (gridXOffset * hexSpawnerData_SO.gridXPosOffset), 0, pos.z));
        //bottom
        neighbours.Add(new Vector3(pos.x - gridXOffset - (gridXOffset * hexSpawnerData_SO.gridXPosOffset), 0, pos.z));
        //bottom left
        neighbours.Add(new Vector3(pos.x - gridXOffset / 2 - (gridXOffset * hexSpawnerData_SO.gridXPosOffset - (hexSpawnerData_SO.gridXPosOffset / 2)), 0, pos.z + (3 * gridYOffset/4) + (gridYOffset * hexSpawnerData_SO.gridYPosOffset)));
        //top right
        neighbours.Add(new Vector3(pos.x + gridXOffset / 2 + (gridXOffset * hexSpawnerData_SO.gridXPosOffset - (hexSpawnerData_SO.gridXPosOffset / 2)), 0, pos.z - (3 * gridYOffset / 4) - (gridYOffset * hexSpawnerData_SO.gridYPosOffset)));
        //top left
        neighbours.Add(new Vector3(pos.x + gridXOffset - gridXOffset / 2 + (gridXOffset * hexSpawnerData_SO.gridXPosOffset - (hexSpawnerData_SO.gridXPosOffset / 2)), 0, pos.z + (3 * gridYOffset / 4) + (gridYOffset * hexSpawnerData_SO.gridYPosOffset)));
        //bottom right
        neighbours.Add(new Vector3(pos.x - gridXOffset + gridXOffset / 2 - (gridXOffset * hexSpawnerData_SO.gridXPosOffset - (hexSpawnerData_SO.gridXPosOffset / 2)), 0, pos.z - (3 * gridYOffset / 4) - (gridYOffset * hexSpawnerData_SO.gridYPosOffset)));

        return neighbours;
    }

}
