using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Vector3 LatLongToXYZ(float Lat, float Long, float Radius)
    {
        return new Vector3(Mathf.Cos(Lat) * Mathf.Sin(Long), Mathf.Sin(Lat), Mathf.Cos(Lat) * Mathf.Cos(Long)) * Radius;
    }
}
