using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh : MonoBehaviour
{
    // Start is called before the first frame update

    private Material m_Material;

    public bool alive = true;
    void Start()
    {
         m_Material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
            m_Material.color = Color.white;
        
    }
}
