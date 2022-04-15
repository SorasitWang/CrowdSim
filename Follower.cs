using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform fol;
    private Vector3 direction;

    public int id;

    private Vector3 noise;
    private float memZ = 0f;
    private Rigidbody myRig;
    public int pre = 0;
    private Vector3 oldPos;
    private float oldRot = 0.0f;
    private bool side=false,front=false;
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //transform.localScale = transform.localScale * Random.Range(0.85f,1.15f);
        //transform.position.Set(transform.localScale/2.0f;
    }

       void FixedUpdate()
    {

        fol = GameObject.Find("Armature.006").transform;
        Vector3 tmp = new Vector3(fol.position.x,fol.position.y,fol.position.z);
        direction = Vector3.Normalize(fol.position - transform.position);
         
        float distance = Vector3.Distance(transform.position,fol.position);

        if (pre==1) direction = new Vector3(-1,0,0);
        else if (pre==2)  {
            
            direction = new Vector3(0,0,memZ);
        }
        //Mathf.Sign(tmp.z-transform.position.z)
        myRig.velocity = direction * 3*calVelo(distance)* Random.Range(0.95f,1.05f);
        Quaternion to = Quaternion.Euler(0, -Mathf.Rad2Deg*Mathf.Atan(direction.z/-Mathf.Abs(direction.x)), 0);
        Debug.Log("tan"+ -Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x));
        //Debug.Log("rotate"+-Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x));
        transform.rotation = Quaternion.Lerp(transform.rotation,to , Time.deltaTime * 3f);
        //transform.Rotate(0,oldRot-Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x) ,0);
        oldRot = Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x);

        
    }

    float calVelo(float x){
        return Mathf.Exp(Mathf.Min(x,25)/8)-1.0f;
    }

     private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Barrier")){
            Vector3 tmp = new Vector3(fol.position.x,fol.position.y,fol.position.z);
            Debug.Log("memZ"+memZ);
            if (pre ==0)
                pre = 1;//go forward
            else if (pre==1){
                if (Mathf.Abs(oldPos.x-transform.position.x) < 0.1){
                    pre = 2 ;// left/right
                     if (Road.rightMost - (other.transform.localScale.z/2 + other.transform.position.z) < 
                        -(Road.LeftMost - (other.transform.position.z-other.transform.localScale.z/2))){
                            memZ = -1.0f;
                        }
                    else memZ = 1.0f;
                    
                }
            }
            else 
                pre = 1;
            oldPos = transform.position;
        }

    }
   
     
     void OnCollisionExit(Collision col)
     {
         if (col.gameObject.CompareTag("Barrier")){
            pre = 0;
            side = false;
            front = false;
            memZ = 0f;
     }
     }
}
