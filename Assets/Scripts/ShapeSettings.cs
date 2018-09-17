using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject {

    [Range(0.01f, 10f)]
    public float PlanetRadius = 1f;


}
