using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SliceTest : MonoBehaviour
{
    [SerializeField] GameObject graphic;
    Material[] materials;

    private void Start()
    {
        materials = GetMaterials(graphic);
    }

    private void Update()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_sliceCenter", transform.position);
            materials[i].SetVector("_sliceNormal", transform.forward);
        }
    }

    Material[] GetMaterials(GameObject g)
    {
        MeshRenderer[] renderers = g.GetComponentsInChildren<MeshRenderer>();
        List<Material> materialList = new();
        foreach (MeshRenderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                materialList.Add(material);
            }
        }
        return materialList.ToArray();
    }
}
