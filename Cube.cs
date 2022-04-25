using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform center = GameObject.Find("Armature.006").transform;
        Debug.Log("Touch"+Vector3.Distance(transform.position,center.position));
        if (Vector3.Distance(transform.position,center.position) < 2.6){
            transform.position = new Vector3( Random.Range(transform.position.x-75,transform.position.x-30), 2.5f ,Random.Range(-50,50));
        }

    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Barrier")){
           transform.position = new Vector3( transform.position.x, 2.5f ,Random.Range(-50,50));
        }


    }
   
}
