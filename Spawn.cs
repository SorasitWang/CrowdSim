using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] folRef;
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
            spawnFol.transform.position = center.position + new Vector3(Random.Range(5,radius),0.0f,Random.Range(-radius,radius));
            BoxCollider col = spawnFol.GetComponent<BoxCollider>();
      
        }


       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
