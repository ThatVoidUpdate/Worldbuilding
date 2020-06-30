using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour
{
    public GameObject LayerCollection;
    private List<Layer> Layers;
    private GameObject GroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        Layers = new List<Layer>(LayerCollection.GetComponentsInChildren<Layer>());
        foreach (Layer layer in Layers)
        {
            if (layer.LayerID.name == "Ground")
            {
                GroundLayer = layer.gameObject;
            }
            else
            {
                layer.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchLayer(LayerSO layerID)
    {
        foreach (Layer layer in Layers)
        {
            layer.gameObject.SetActive(layer.LayerID == layerID);
        }
        GroundLayer.SetActive(true);
    }
}
