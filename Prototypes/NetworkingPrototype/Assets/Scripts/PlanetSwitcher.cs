using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSwitcher : MonoBehaviour
{
    public bool AtMyPlanet = true;

    public BoolEvent SwitchEvent;

    public void SwitchPlanet()
    {
        AtMyPlanet = !AtMyPlanet;
        SwitchEvent.Invoke(AtMyPlanet);
    }
}
