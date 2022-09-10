using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject currentSelection;
    public event System.EventHandler<OnHexSelectionArgs> OnHexSelection;
    public class OnHexSelectionArgs : System.EventArgs
    {
        public GameObject currentSelection;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSelection = null;
    }

    public void SetSelection(GameObject selection)
    {
        this.currentSelection = selection;
        Debug.Log("Hex Selected at :" + selection?.transform.position);
        OnHexSelection?.Invoke(this, new OnHexSelectionArgs() { currentSelection = this.currentSelection });
    }

}
