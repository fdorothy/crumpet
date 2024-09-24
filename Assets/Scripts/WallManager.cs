using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager singleton;
    public Material seeThroughMaterial;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
