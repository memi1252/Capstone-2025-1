using System;
using Unity.VisualScripting;
using UnityEngine;

public class FliterSystem : MonoBehaviour
{
    public bool isbroken = false;
    public bool off = false;
    public bool outFliter = false;
    public bool clear = false;
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obj3;
    [SerializeField] private GameObject obj4;
    
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blackMaterial;

    private void Update()
    {
        if (clear)
        {
            GameManager.Instance.BBASS.GetComponent<BBABB_FliterCLEAR>().Clear();
            clear = false;
        }
        
        if (isbroken && !off)
        {
            ChangeMaterial(obj1, 0, redMaterial);
            ChangeMaterial(obj2, 1, redMaterial);
            ChangeMaterial(obj3, 1, redMaterial);
            ChangeMaterial(obj4.transform.GetChild(0).gameObject, 2, redMaterial);
            ChangeMaterial(obj4.transform.GetChild(1).gameObject, 0, redMaterial);
        }
        else if (isbroken && off)
        {
            ChangeMaterial(obj1, 0, blackMaterial);
            ChangeMaterial(obj2, 1, blackMaterial);
            ChangeMaterial(obj3, 1, blackMaterial);
            ChangeMaterial(obj4.transform.GetChild(0).gameObject, 2, blackMaterial);
            ChangeMaterial(obj4.transform.GetChild(1).gameObject, 0, blackMaterial);
        }
        else
        {
            ChangeMaterial(obj1, 0, blueMaterial);
            ChangeMaterial(obj2, 1, blueMaterial);
            ChangeMaterial(obj3, 1, blueMaterial);
            ChangeMaterial(obj4.transform.GetChild(0).gameObject, 2, blueMaterial);
            ChangeMaterial(obj4.transform.GetChild(1).gameObject, 0, blueMaterial);
        }
    }

    private void ChangeMaterial(GameObject obj, int index, Material newMaterial)
    {
        var materials = obj.GetComponent<Renderer>().materials;
        if (index < materials.Length)
        {
            materials[index] = newMaterial;
            obj.GetComponent<Renderer>().materials = materials;
        }
    }
}
