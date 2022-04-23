using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public GameObject dimensionA, dimensionB;

    // switch dimensions
    public void DimensionSwap()
    {
        dimensionA.SetActive(!dimensionA.activeInHierarchy);
        dimensionB.SetActive(!dimensionB.activeInHierarchy);
    }
}
