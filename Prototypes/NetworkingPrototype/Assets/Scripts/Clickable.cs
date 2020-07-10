using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    public abstract void OnBecomeClicked();
    public bool Owned;
}
