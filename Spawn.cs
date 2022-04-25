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
    private float radius = 70 , count = 0.0f;
    private int num = 40;

     public GameObject[] n;
    void Start()
    {
        for (int i=0;i<num;i++){
            int randomIdx = Random.Range(0,folRef.Length);

            spawnFol = Instantiate(folRef[randomIdx]);
            spawnFol.GetComponent<Follower>().id = i;
                //left
            Vector3 center = GameObject.Find("Armature.006").transform.position;
            float s = size*Random.Range(1-randSize,1+randSize);
            spawnFol.transform.position = new Vector3(Random.Range(-15,radius)+center.x,
                2.0f,Random.Range(-radius,radius)+center.z);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if (count<0){
            count = 0.05f;
            spawnFol = Instantiate(folRef[0]);
                //left
            Vector3 center = GameObject.Find("Armature.006").transform.position;
            float s = size*Random.Range(1-randSize,1+randSize);
            spawnFol.transform.position = new Vector3(Random.Range(5,10)+center.x,
                2.0f,Random.Range(-10,10)+center.z);
        }
        n = GameObject.FindGameObjectsWithTag("Crowd");
        Debug.Log("Particle "+n.Length);
        count -= Time.deltaTime;
        
    }

    
}
