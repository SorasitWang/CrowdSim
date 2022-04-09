using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    // Start is called before the first frame update
     private float movementX;
     private float moveForce = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX,0f,0f) * Time.deltaTime * moveForce;
    }
}
