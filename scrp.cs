using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrp : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 velocity;
     private float movementZ,move=-2f;
     private float moveForce = 30;
    private Rigidbody myRig;
    private Transform obs;
    private float countTime = 0.0f;
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        countTime += Time.deltaTime;
        if (countTime > Random.Range(1.0f,3f)){
            countTime = 0f;
            movementZ =  Random.Range(-0.7f,0.7f);
        }


        for (int i=1;i<=3;i++){
            obs = GameObject.Find("Barrier"+i.ToString()).transform;
  
            //โดนบัง
            //Debug.Log((transform.position.z <= obs.localScale.z/2 + obs.position.z) && (transform.position.z >= obs.position.z-obs.localScale.z/2));
             
            if (transform.position.x - 30 < obs.localPosition.x && transform.position.x > obs.localPosition.x ){
                /*Debug.Log("First");
                Debug.Log(transform.position.z);
                Debug.Log(obs.localScale.z/2 + obs.position.z);*/
                if(transform.position.z <= (obs.localScale.z/2 + obs.position.z+Road.rightMost)/2.0 
                && transform.position.z >= (obs.position.z-obs.localScale.z/2+Road.LeftMost)/2.0){
                    Debug.Log(i.ToString());
                    //turn left
                    Vector3 dest;
                    if (Road.rightMost - (obs.localScale.z/2 + obs.position.z) < -(Road.LeftMost - (obs.position.z-obs.localScale.z/2))){
                       
                        movementZ = -8f;
                        
                    }
                    else {
                       
                        movementZ = 8f;
                    }
                    //if (Mathf.Abs(transform.position.x - obs.localPosition.x) < 20f ) movementZ *= 2.0f;
                Debug.Log(movementZ);
                break;
                }

               
                
            }
        }
        Debug.Log(Time.time); 
        //velocity = new Vector3(move,0f,movementZ) * moveForce;
        float a = Random.Range(0.8f,1.2f);
        float b= Random.Range(0.8f,1.2f);
        myRig.velocity = Vector3.Normalize(new Vector3(move*a,0f,movementZ*b))  * moveForce*Random.Range(0.8f,1.2f);
        //Debug.Log(Vector3.Normalize(new Vector3(move,0f,movementZ)));
   
    }
}
