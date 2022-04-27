using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
    
{

    public float y = 50;


    public float w = 10;

    public float force = 20;

    public Vector3 direction = new Vector3(-1,0,0);
   
   void Awake(){


   }

   void update(){
       Debug.Log("windCheck1");
       change();

   }

   void change(){
        Debug.Log("windCheck");
         direction.z = Input.GetAxisRaw("Horizontal");
      
        Debug.Log("windDirect"+direction);
   }
} ;