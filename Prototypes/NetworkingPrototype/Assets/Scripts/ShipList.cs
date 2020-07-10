using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship List")]
public class ShipList: ScriptableObject
{
    public List<GameObject> Ships = new List<GameObject>();
    public List<GameObject> SelectedShips = new List<GameObject>();

    private void OnValidate()
    {
        Ships = new List<GameObject>();
        SelectedShips = new List<GameObject>();
    }
}
