using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public ShipList Ships;
    public GameObject[] ShipPrefabs;

    public GameObject Planet;
    public GameObject OtherPlanet;

    public StringEvent SpawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject Ship = Instantiate(ShipPrefab, Vector3.zero, Quaternion.identity);
        Ship.transform.position = Utilities.LatLongToXYZ(0, 0, Ship.GetComponent<ShipMover>().CurrentLayer.Distance);
        Ships.Ships.Add(Ship);
        Ship.GetComponent<ShipMover>().Ships = Ships;
        Ship.GetComponent<ShipMover>().ID = (int)Random.Range(0, 9999999999);
        */
        //SpawnShip("SHIP|0|0|0,0,1.5|111|1");
        //SpawnShip("SHIP|0|1|0,0,-1.5|112|0");
    }

    /// <summary>
    /// Spawns a ship, taking data from the networking system
    /// </summary>
    /// <param name="data">A string of the form "SHIP|shipid|planetid|x,y,z|id|owned"</param>
    public void SpawnShip(string data)
    {
        string[] SplitData = data.Split('|');
        if (SplitData.Length == 6 && SplitData[0] == "SHIP")
        {
            GameObject SpawningShip = Instantiate(ShipPrefabs[Convert.ToInt32(SplitData[1])]);
            string[] positionString = SplitData[3].Split(',');
            Vector3 SpawnPosition = new Vector3((float)Convert.ToDouble(positionString[0]), (float)Convert.ToDouble(positionString[1]), (float)Convert.ToDouble(positionString[2]));
            SpawningShip.transform.position = SpawnPosition;
            SpawningShip.GetComponent<ShipMover>().ID = Convert.ToInt32(SplitData[4]);
            Ships.Ships.Add(SpawningShip);
            SpawningShip.GetComponent<ShipMover>().Ships = Ships;

            if (SplitData[2] == "0")
            {
                SpawningShip.GetComponent<ShipMover>().Planet = Planet;
            }
            else
            {
                SpawningShip.GetComponent<ShipMover>().Planet = OtherPlanet;
            }

            if (SplitData[5] == "1")
            {
                SpawningShip.GetComponent<ShipMover>().Owned = true;
            }
            else
            {
                SpawningShip.GetComponent<ShipMover>().Owned = false;
            }
        }
    }

    public void Spawn()
    {
        System.Random rnd = new System.Random();
        int ID = rnd.Next(0, 99999999);
        SpawnShip("SHIP|0|0|0,0,1.5|" + ID + "|1");
        SpawnEvent.Invoke("SHIP|0|1|0,0,1.5|" + ID + "|0");
    }
}
