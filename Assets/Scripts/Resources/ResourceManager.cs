using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static Resource Blood;
    public static Resource Metal;
    public static Resource Matter;

    [SerializeField] private Resource setBlood;
    [SerializeField] private Resource setMetal;
    [SerializeField] private Resource setMatter;

    void Awake()
    {
        SetResourceReferences();
    }

    void Start()
    {
        Blood.Reset();
        Metal.Reset();
        Matter.Reset();
    }

    void Update()
    {
        Debug.Log("Blood: " + Blood.Value);
        Debug.Log("Metal: " + Metal.Value);
        Debug.Log("Matter: " + Matter.Value);
    }

    void SetResourceReferences()
    {
        Blood = setBlood;
        Metal = setMetal;
        Matter = setMatter;
    }
}
 