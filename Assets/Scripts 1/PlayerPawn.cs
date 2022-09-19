using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPawn : MonoBehaviour
{
    //dependencies
    [SerializeField] private ManagerHelper helper;
    [SerializeField] private GameObject currentSelection;
    [SerializeField] private GameObject currentHexPosition;
    [SerializeField] private List<GameObject> currentHexNeighbours;
    [SerializeField] private int hexLayerNumber;
    //members
    public event System.EventHandler<OnCrystalHarvestArgs> OnCrystalHarvest;
    public class OnCrystalHarvestArgs : System.EventArgs
    {
        public GameObject playerHexPosition;
        public float crystalAmount;
        public float harvestedCrystalAmount;
        public bool isLooted;

    }

    [SerializeField] private float m_movementTime;
    [SerializeField] private float m_yOffset;
    [SerializeField] private float m_radiusModifier;
    [SerializeField] private float currentCrystalAmount;
    [SerializeField] private float crystalAmountPerDay;



    // Start is called before the first frame update
    void Start()
    {
        helper.selectionManager.OnHexSelection += SelectionManager_OnHexSelection;
        helper.hexSpawner.OnHexSpawn += HexSpawner_OnHexSpawn;
        helper.dayManager.OnDayPassed += DayManager_OnDayPassed;
        SetupPawn();
        AddNeighbours();
    }

    private void DayManager_OnDayPassed(object sender, DayCycle.OnDayPassedArgs e)
    {
        if (currentHexPosition == null)
        {
            SceneManager.LoadScene(0);
            //lose game;
        }
        currentCrystalAmount -= crystalAmountPerDay;
        if (currentCrystalAmount <= 0)
        {
            //lose game
            SceneManager.LoadScene(0);
        }
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
        //OnSelectionHex?.Invoke(this, new OnSelectionHexArgs() { playerHexPosition = this.currentHexPosition });
        helper.hexSpawner.AttemptSpawnNeighboursHexes(currentHexPosition.transform.position);
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
        foreach (Collider col in neighbours)
        {
            currentHexNeighbours.Add(col.gameObject);
        }
    }
    private void SetupPawn()
    {
        currentSelection = currentHexPosition;
        currentCrystalAmount = 5;
        MovePawnAndSearch();
        currentSelection = null;
    }

    //used in button
    public void HarvestCrystal()
    {
        if (currentHexPosition.GetComponent<Hex>() == null) return;
        if (currentHexPosition.GetComponent<Hex>().GetHexData().isLooted) return;

        currentCrystalAmount += currentHexPosition.GetComponent<Hex>().GetHexData().crystalAmount;
        currentHexPosition.GetComponent<Hex>().LootHex();

        OnCrystalHarvest?.Invoke(this, new OnCrystalHarvestArgs()
        {
            crystalAmount = currentCrystalAmount,
            playerHexPosition = currentHexPosition,
            harvestedCrystalAmount = currentHexPosition.GetComponent<Hex>().GetHexData().crystalAmount,
        });

    }
    public float GetRemainingCrystalTime()
    {
        return currentCrystalAmount / crystalAmountPerDay;
    }
    public float GetCrystalAmount()
    {
        return currentCrystalAmount;
    }
}
