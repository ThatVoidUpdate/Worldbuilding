using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*Target 0,0,0
     * Mouse horizontal changes latitude
     * Mouse vertical changes longitude
     * 
     */


    public float HorizontalSensitivity;
    public float VerticalSensitivity;

    private float HorizontalPosition = 0; //0 - 360
    private float VerticalPosition = 0; //-90 - 90

    private Vector3 OldMousePosition;
    private Vector3 CurrentMousePosition;
    private Vector2 MouseDelta;

    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        float cosLon = Mathf.Cos(HorizontalPosition * Mathf.PI / 180f);
        float sinLon = Mathf.Sin(HorizontalPosition * Mathf.PI / 180f);
        float cosLat = Mathf.Cos(VerticalPosition * Mathf.PI / 180f);
        float sinLat = Mathf.Sin(VerticalPosition * Mathf.PI / 180f);

        transform.position = new Vector3(cosLat * sinLon, sinLat, cosLat * cosLon) * Distance;
        transform.LookAt(Vector3.zero);
        print(HorizontalPosition + " " + VerticalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        OldMousePosition = CurrentMousePosition;
        CurrentMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            MouseDelta.x = -(OldMousePosition - CurrentMousePosition).x;
            MouseDelta.y = (OldMousePosition - CurrentMousePosition).y;
            //Debug.Log(MouseDelta);

            float XRotationDelta = MouseDelta.x * HorizontalSensitivity;
            float YRotationDelta = MouseDelta.y * VerticalSensitivity;

            HorizontalPosition += XRotationDelta;
            if (HorizontalPosition < 0)
            {
                HorizontalPosition += 360;
            }
            else if (HorizontalPosition > 360)
            {
                HorizontalPosition -= 360;
            }

            VerticalPosition += YRotationDelta;
            if (VerticalPosition < -90 || VerticalPosition > 90)
            {
                VerticalPosition -= YRotationDelta;
            }
            

            //convert lat-long to ecef
            /* https://stackoverflow.com/questions/8981943/lat-long-to-x-y-z-position-in-js-not-working?noredirect=1&lq=1 (modified to fix coordinate system)
             * var cosLat = Math.cos(lat * Math.PI / 180.0);
             * var sinLat = Math.sin(lat * Math.PI / 180.0);
             * var cosLon = Math.cos(lon * Math.PI / 180.0);
             * var sinLon = Math.sin(lon * Math.PI / 180.0);
             * var rad = 500.0;
             * marker_mesh.position.x = rad * cosLat * cosLon;
             * marker_mesh.position.y = rad * cosLat * sinLon;
             * marker_mesh.position.z = rad * sinLat;
             */

            float cosLon = Mathf.Cos(HorizontalPosition * Mathf.PI / 180f);
            float sinLon = Mathf.Sin(HorizontalPosition * Mathf.PI / 180f);
            float cosLat = Mathf.Cos(VerticalPosition * Mathf.PI / 180f);
            float sinLat = Mathf.Sin(VerticalPosition * Mathf.PI / 180f);
            
            transform.position = new Vector3(cosLat * sinLon, sinLat, cosLat * cosLon) * Distance;
            transform.LookAt(Vector3.zero);
            print(HorizontalPosition + " " + VerticalPosition);
            
        }
    }
}
