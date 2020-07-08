using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickHandler : MonoBehaviour
{
    private Camera cam;
    public ShipList Ships;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Left clicked
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Unit", "Building")))
            {
                print("Clicked on unit/building");
                IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
                clickable?.OnBecomeClicked();
            }
            else if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Layer")))
            {
                print("Clicked on a layer");
                Layer clickedLayer = hit.collider.gameObject.GetComponent<Layer>();
                if (clickedLayer != null)
                {
                    (float, float) LatLong = Utilities.XYZtoLatLong(hit.point, hit.collider.gameObject.GetComponent<Layer>().LayerID.Distance);
                    print("Clicked at " + LatLong.Item1 * Mathf.Rad2Deg + ", " + LatLong.Item2 * Mathf.Rad2Deg);
                    foreach (GameObject ship in Ships.SelectedShips)
                    {
                        ship.GetComponent<ShipMover>().TargetLat = LatLong.Item1 * Mathf.Rad2Deg;
                        ship.GetComponent<ShipMover>().TargetLong = LatLong.Item2 * Mathf.Rad2Deg;
                    }
                }
            }
            else
            {
                print("Reset selected ships");
                Ships.SelectedShips.Clear();
            }
        }
    }
}
