using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour
{
    public List<GameObject> LayerCollections;
    private List<Layer> Layers;
    private List<GameObject> GroundLayers = new List<GameObject>();
    public LayerSO ActiveLayerID;

    // Start is called before the first frame update
    void Start()
    {
        Layers = new List<Layer>();
        foreach (GameObject LayerCollection in LayerCollections)
        {
            foreach (Layer layer in LayerCollection.GetComponentsInChildren<Layer>())
            {
                Layers.Add(layer);
            }
        }

        
        foreach (Layer layer in Layers)
        {
            if (layer.LayerID.name == "Ground")
            {
                GroundLayers.Add(layer.gameObject);
            }
            else
            {
                layer.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchLayer(LayerSO layerID)
    {
        ActiveLayerID = layerID;
        foreach (Layer layer in Layers)
        {
            layer.gameObject.SetActive(layer.LayerID == layerID);
        }
        foreach (GameObject GroundLayer in GroundLayers)
        {
            GroundLayer.SetActive(true);
        }
        
    }
}
