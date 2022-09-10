using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    //dependencies
    [SerializeField] private ManagerHelper helper;
    [SerializeField] private GameObject currentSelection;
    [SerializeField] private GameObject currentHexPosition;
    [SerializeField] private List<GameObject> currentHexNeighbours;
    [SerializeField] private int hexLayerNumber;
    //members
    [SerializeField] private float m_movementTime;
    [SerializeField] private float m_yOffset;
    [SerializeField] private float m_radiusModifier;



    // Start is called before the first frame update
    void Start()
    {
        helper.selectionManager.OnHexSelection += SelectionManager_OnHexSelection;
        helper.hexSpawner.OnHexSpawn += HexSpawner_OnHexSpawn;
        SetupPawn();
    }

    private void HexSpawner_OnHexSpawn(object sender, HexSpawner.OnSpawnArgs e)
    {
        AddNeighbours();
    }

    private void SelectionManager_OnHexSelection(object sender, SelectionManager.OnHexSelectionArgs e)
    {
        currentSelection = e.currentSelection;
    }
    public void MovePawn()
    {   
        StartCoroutine(MovePawnAndSearch());
    }
    public IEnumerator MovePawnAndSearch()
    {
        if (currentSelection == null) yield break; //if selection is null, do nothing
        if (!SearchNeighboursForTarget(currentSelection)) yield break; //if selection is not neighbour do nothing;

        Vector3 currentPosNoY = new Vector3(transform.position.x, m_yOffset, transform.position.z);
        Vector3 newPosNoY = new Vector3(currentSelection.transform.position.x, m_yOffset, currentSelection.transform.position.z);

        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / m_movementTime;

            if (t > 1) t = 1;

            transform.position = Vector3.Lerp(currentPosNoY, newPosNoY, t);

            yield return null;
        }
        currentHexPosition = currentSelection;
        AddNeighbours();

    }
    public void MovePawnAndSearch2()
    {
        if (currentSelection == null) return; //if selection is null, do nothing
        if (!SearchNeighboursForTarget(currentSelection)) return; //if selection is not neighbour do nothing;

        Vector3 currentPosNoY = new Vector3(transform.position.x, m_yOffset, transform.position.z);
        Vector3 newPosNoY = new Vector3(currentSelection.transform.position.x, m_yOffset, currentSelection.transform.position.z);

        transform.position = Vector3.Lerp(currentPosNoY, newPosNoY, m_movementTime * Time.deltaTime);

        AddNeighbours();
    }
    private bool SearchNeighboursForTarget(GameObject currentSelection)
    {
        if (currentHexNeighbours.Find(x => x.gameObject == currentSelection) == null) return false;
        //else
        return true;
    }
    private void AddNeighbours()
    {
        currentHexNeighbours.Clear();
        Collider[] neighbours = Physics.OverlapSphere(currentHexPosition.transform.position, currentHexPosition.gameObject.GetComponent<SphereCollider>().radius * m_radiusModifier,1 << hexLayerNumber);
        Debug.Log(neighbours.Length);
        foreach (Collider col in neighbours)
        {
            currentHexNeighbours.Add(col.gameObject);
        }
    }
    private void SetupPawn()
    {
        currentSelection = currentHexPosition;
        MovePawnAndSearch();
        currentSelection = null;
    }
}
