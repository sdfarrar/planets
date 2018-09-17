using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [Range(2, 256)]
    public int Resolution = 10;

    public ShapeSettings ShapeSettings;
    public ColorSettings ColorSettings;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void Initialize() {
        shapeGenerator = new ShapeGenerator(ShapeSettings);

        if(meshFilters == null || meshFilters.Length==0){
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for(int i=0; i<6; ++i){
            if(meshFilters[i]==null){
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, Resolution, directions[i]);
        }

    }

    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    void GenerateMesh() {
        foreach (TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
    }

    void GenerateColors(){
        foreach(MeshFilter m in meshFilters){
            m.GetComponent<MeshRenderer>().sharedMaterial.color = ColorSettings.PlanetColor;
        }
    }


    #if UNITY_EDITOR

    public bool AutoUpdate = true;
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    public void OnShapeSettingsUpdated(){
        if(!AutoUpdate){ return; }
        Initialize();
        GenerateMesh();
    }

    public void OnColorSettingsUpdated(){
        if(!AutoUpdate){ return; }
        Initialize();
        GenerateColors();
    }

    #endif
}
