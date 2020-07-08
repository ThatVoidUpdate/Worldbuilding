using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickHandler : MonoBehaviour
{
    private Camera cam;

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
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Unit", "Building")))
            {
                IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
                clickable?.OnBecomeClicked();
            }
        }
    }
}
