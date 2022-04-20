using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] folRef;
    [SerializeField]
    private float size = 2.4f;
    [SerializeField]
    private float randSize = 0.2f;
    private GameObject spawnFol;
    private float radius = 70;
    private int num = 40;
    void Start()
    {
        for (int i=0;i<num;i++){
            int randomIdx = Random.Range(0,folRef.Length);
    

            spawnFol = Instantiate(folRef[randomIdx]);
            spawnFol.GetComponent<Follower>().id = i;
                //left
            Transform center = GameObject.Find("Armature.006").transform;
            float s = size*Random.Range(1-randSize,1+randSize);
            spawnFol.transform.position = center.position + new Vector3(Random.Range(5,radius),
                s/2f,Random.Range(-radius,radius));
            spawnFol.transform.localScale = new Vector3(s,s,s);
            BoxCollider col = spawnFol.GetComponent<BoxCollider>();
      
        }


       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
