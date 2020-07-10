using System;
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

    /// <summary>
    /// Sets the position of a ship
    /// </summary>
    /// <param name="data">A string of the form "SETPOS|id|lat,long"</param>
    public void SetTargetPosition(string data)
    {
        string[] SplitData = data.Split('|');
        if (SplitData.Length == 3 && SplitData[0] == "SETPOS")
        {
            foreach (GameObject ship in Ships)
            {
                if (ship.GetComponent<ShipMover>().ID == Convert.ToInt32(SplitData[1]))
                {
                    float Lat = (float)Convert.ToDouble(SplitData[2].Split(',')[0]);
                    float Long = (float)Convert.ToDouble(SplitData[2].Split(',')[1]);

                    ship.GetComponent<ShipMover>().TargetLat = Lat;
                    ship.GetComponent<ShipMover>().TargetLong = Long;

                    break;
                }
            }
        }
    }
}
