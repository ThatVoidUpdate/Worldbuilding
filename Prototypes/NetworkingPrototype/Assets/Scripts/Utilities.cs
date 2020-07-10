using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    /// <summary>
    /// Converts Lat and Long into ECEF XYZ
    /// </summary>
    /// <param name="Lat">Latitude (up down) in radians</param>
    /// <param name="Long">Longitude (roundy round) in radians</param>
    /// <param name="Radius">Distance from the center</param>
    /// <returns>A vector3 for the position in world space</returns>
    public static Vector3 LatLongToXYZ(float Lat, float Long, float Radius)
    {
        return new Vector3(Mathf.Cos(Lat) * Mathf.Sin(Long), Mathf.Sin(Lat), Mathf.Cos(Lat) * Mathf.Cos(Long)) * Radius;
    }

    /// <summary>
    /// Converts ECEF XYZ to Lat and Long
    /// </summary>
    /// <param name="XYZ">World space position</param>
    /// <param name="Radius">Distance from the center</param>
    /// <returns>Lat and Long in radians</returns>
    public static (float, float) XYZtoLatLong(Vector3 XYZ, float Radius)
    {
        XYZ /= Radius;
        float Lat = Mathf.Asin(XYZ.y);
        float Long = Mathf.Asin(XYZ.x / Mathf.Cos(Lat));
        if (XYZ.z < 0)
        {
            Long = (Mathf.PI - Long);
        }
        return (Lat, Long);
    }
}
