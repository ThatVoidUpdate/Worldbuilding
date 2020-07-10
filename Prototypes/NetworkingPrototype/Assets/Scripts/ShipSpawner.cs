using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public ShipList Ships;
    public GameObject ShipPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Ship = Instantiate(ShipPrefab, Vector3.zero, Quaternion.identity);
        Ship.transform.position = Utilities.LatLongToXYZ(0, 0, Ship.GetComponent<ShipMover>().CurrentLayer.Distance);
        Ships.Ships.Add(Ship);
        Ship.GetComponent<ShipMover>().Ships = Ships;
        Ship.GetComponent<ShipMover>().ID = (int)Random.Range(0, 9999999999);
    }
}
