using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour, IClickable
{
    public float TargetLat;
    public float TargetLong;
    private float TargetLatRad;
    private float TargetLongRad;
    float currentLat;
    float currentLong;

    public float speed;
    public LayerSO CurrentLayer;

    public int ID;

    Vector3 LastPosition;

    float Distance;

    bool selected;

    [HideInInspector]
    public ShipList Ships;

    // Start is called before the first frame update
    void Start()
    {
        TargetLatRad = TargetLat * Mathf.Deg2Rad;
        TargetLongRad = TargetLong * Mathf.Deg2Rad;
        transform.position = Utilities.LatLongToXYZ(TargetLatRad, TargetLongRad, CurrentLayer.Distance);
    }

    // Update is called once per frame
    void Update()
    {
        LastPosition = transform.position;
        TargetLatRad = TargetLat * Mathf.Deg2Rad;
        TargetLongRad = TargetLong * Mathf.Deg2Rad;

        //calculate target position
        Vector3 Target = Utilities.LatLongToXYZ(TargetLatRad, TargetLongRad, CurrentLayer.Distance);

        //calculate bearing
        /* https://www.movable-type.co.uk/scripts/latlong.html#bearing
         * const y = Math.sin(λ2-λ1) * Math.cos(φ2);
           const x = Math.cos(φ1)*Math.sin(φ2) - Math.sin(φ1)*Math.cos(φ2)*Math.cos(λ2-λ1);
           const θ = Math.atan2(y, x);
           const brng = (θ*180/Math.PI + 360) % 360; // in degrees
         */
        float y = Mathf.Sin(TargetLongRad - currentLong) * Mathf.Cos(TargetLatRad);
        float x = Mathf.Cos(currentLat) * Mathf.Sin(TargetLatRad) - Mathf.Sin(currentLat) * Mathf.Cos(TargetLatRad) * Mathf.Cos(TargetLongRad - currentLong);
        float theta = Mathf.Atan2(y, x);
        float bearing = (theta * 180 / Mathf.PI + 360) % 360;

        //calculate next position
        /*
         * const sinφ2 = Math.sin(φ1) * Math.cos(δ) + Math.cos(φ1) * Math.sin(δ) * Math.cos(θ);
            const φ2 = Math.asin(sinφ2);
            const y = Math.sin(θ) * Math.sin(δ) * Math.cos(φ1);
            const x = Math.cos(δ) - Math.sin(φ1) * sinφ2;
            const λ2 = λ1 + Math.atan2(y, x);
         */
        float sineLat2 = Mathf.Sin(currentLat) * Mathf.Cos(speed / CurrentLayer.Distance) + Mathf.Cos(currentLat) * Mathf.Sin(speed / CurrentLayer.Distance) * Mathf.Cos(theta);
        float nextLat = Mathf.Asin(sineLat2);
        y = Mathf.Sin(theta) * Mathf.Sin(speed / CurrentLayer.Distance) * Mathf.Cos(currentLat);
        x = Mathf.Cos(speed / CurrentLayer.Distance) - Mathf.Sin(currentLat) * sineLat2;
        float nextLong = currentLong + Mathf.Atan2(y, x);

        Vector3 nextPosition = Utilities.LatLongToXYZ(nextLat, nextLong, CurrentLayer.Distance);

        transform.position = nextPosition;

        /*//check if we are about to reach the target point
        if (Mathf.Acos(Vector3.Dot(transform.position - LastPosition, 
            Utilities.LatLongToXYZ(TargetLatRad, TargetLongRad, CurrentLayer.Distance) - transform.position) / 
            ((transform.position - LastPosition).magnitude * (Utilities.LatLongToXYZ(TargetLatRad, TargetLongRad, CurrentLayer.Distance) - transform.position).magnitude)) * Mathf.Rad2Deg > 90)

        {
            transform.position = Utilities.LatLongToXYZ(TargetLatRad, TargetLatRad, CurrentLayer.Distance);
        } 
        */

        currentLat = nextLat;
        currentLong = nextLong;

        transform.up = transform.position.normalized;
    }

    public void OnBecomeClicked()
    {
        selected = true;
        Debug.Log("Ship was clicked");
        Ships.SelectedShips.Add(this.gameObject);
    }
}
