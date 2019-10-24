using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritMaterial : MonoBehaviour
{
    public GameObject self;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        self.GetComponent<Renderer>().material = parent.GetComponent<Renderer>().material;
    }
}
