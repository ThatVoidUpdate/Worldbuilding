using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LayerHandler layerHandler;

    public float HorizontalSensitivity;
    public float VerticalSensitivity;

    private float HorizontalPosition = 0; //0 - 360
    private float VerticalPosition = 0; //-90 - 90

    private Vector3 OldMousePosition;
    private Vector3 CurrentMousePosition;
    private Vector2 MouseDelta;

    public float Distance = 2;
    private float OldDistance = 2;

    public float Lerptime = 0.5f;
    private float CurrentLerpTime = 0;

    public float CameraDistanceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Mathf.Cos(VerticalPosition * Mathf.Deg2Rad) * Mathf.Sin(HorizontalPosition * Mathf.Deg2Rad),
                                         Mathf.Sin(VerticalPosition * Mathf.Deg2Rad),
                                         Mathf.Cos(VerticalPosition * Mathf.Deg2Rad) * Mathf.Cos(HorizontalPosition * Mathf.Deg2Rad)) * Distance;
        transform.LookAt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        OldMousePosition = CurrentMousePosition;
        CurrentMousePosition = Input.mousePosition;

        //Looking around
        if (Input.GetMouseButton(1))
        {
            //Calculate mouse movement delta
            MouseDelta.x = -(OldMousePosition - CurrentMousePosition).x;
            MouseDelta.y = (OldMousePosition - CurrentMousePosition).y;
            float XRotationDelta = MouseDelta.x * HorizontalSensitivity;
            float YRotationDelta = MouseDelta.y * VerticalSensitivity;

            //Check for overflows
            HorizontalPosition += XRotationDelta;
            if (HorizontalPosition < 0)
            {
                HorizontalPosition += 2 * Mathf.PI;
            }
            else if (HorizontalPosition > 2 * Mathf.PI)
            {
                HorizontalPosition -= 2 * Mathf.PI;
            }

            VerticalPosition += YRotationDelta;
            if (VerticalPosition < -Mathf.PI / 2 || VerticalPosition > Mathf.PI / 2)
            {
                VerticalPosition -= YRotationDelta;
            }            
        }
        
        float NewDistance = layerHandler.ActiveLayerID != null ? layerHandler.ActiveLayerID.Distance * CameraDistanceMultiplier : 2;

        if (NewDistance != OldDistance)
        {
            Distance = Mathf.SmoothStep(OldDistance, NewDistance, CurrentLerpTime / Lerptime);

            if (Mathf.Abs(NewDistance-Distance) <= 0.01)
            {
                Distance = NewDistance;
                OldDistance = NewDistance;
                CurrentLerpTime = 0;
            }
            else
            {
                CurrentLerpTime += Time.deltaTime;
            }            
        }
        else
        {
            Distance = NewDistance;
        }

        /* Convert LatLong to ECEF
         * https://stackoverflow.com/questions/8981943/lat-long-to-x-y-z-position-in-js-not-working?noredirect=1&lq=1 (modified to fix coordinate system)
             * var cosLat = Math.cos(lat * Math.PI / 180.0);
             * var sinLat = Math.sin(lat * Math.PI / 180.0);
             * var cosLon = Math.cos(lon * Math.PI / 180.0);
             * var sinLon = Math.sin(lon * Math.PI / 180.0);
             * var rad = 500.0;
             * marker_mesh.position.x = rad * cosLat * cosLon;
             * marker_mesh.position.y = rad * cosLat * sinLon;
             * marker_mesh.position.z = rad * sinLat;
             */


        transform.position = Utilities.LatLongToXYZ(VerticalPosition, HorizontalPosition, Distance);
        transform.LookAt(Vector3.zero);
    }
}
