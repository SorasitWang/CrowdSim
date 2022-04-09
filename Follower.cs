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
    private Rigidbody myRig;
    private int pre = 0;
    private Vector3 oldPos;
    private bool side=false,front=false;
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        

        //stuck aside
    }

       void FixedUpdate()
    {

        fol = GameObject.Find("box").transform;
        direction = Vector3.Normalize(fol.localPosition - transform.localPosition);
       
        
        float distance = Vector3.Distance(transform.localPosition,fol.localPosition);

        //transform.position += direction * Time.deltaTime * calVelo(distance)* Random.Range(0.95f,1.05f);
 
        
        float z ;
        for (int i=1;i<=3;i++){
            Transform obs = GameObject.Find("Barrier"+i.ToString()).transform;
  
            //โดนบัง
            //Debug.Log((transform.position.z <= obs.localScale.z/2 + obs.position.z) && (transform.position.z >= obs.position.z-obs.localScale.z/2));
             
            /*if (transform.position.x - 10 < obs.localPosition.x && transform.position.x > obs.localPosition.x ){
                
                if(transform.position.z <= (obs.localScale.z/2 + obs.position.z)+5
                && transform.position.z >= (obs.position.z-obs.localScale.z/2)-5){
                    Debug.Log(i.ToString());
                    //turn left
                    if (Road.rightMost - (obs.localScale.z/2 + obs.position.z) < -(Road.LeftMost - (obs.position.z-obs.localScale.z/2))){
                       
                        direction = new Vector3(0,0,-1f);
                        
                    }
                    else {
                       
                        direction = new Vector3(0,0,1f);
                    }
                    //if (Mathf.Abs(transform.position.x - obs.localPosition.x) < 20f ) movementZ *= 2.0f;

                break;
                }

               
                
            }*/
        }
        //if (side) direction = new Vector3(-1,0,0);
        Debug.Log("Dir"+Mathf.Sign(fol.transform.position.z-transform.position.z));
        //if (front) direction = new Vector3(0,0,Mathf.Sign(fol.transform.position.z-transform.position.z));
        //if (side) direction = new Vector3(-1,0,0);
        if (pre==1) direction = new Vector3(-1,0,0);
        else if (pre==2)  direction = new Vector3(0,0,Mathf.Sign(fol.transform.position.z-transform.position.z));
        myRig.velocity = direction * calVelo(distance)* Random.Range(0.95f,1.05f);

    }

    float calVelo(float x){
        return Mathf.Exp(Mathf.Min(x,40)/10)-1.0f;
    }

     private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Barrier")){
            if (pre ==0)
                pre = 1;//go forward
            else if (pre==1){
                if (Mathf.Abs(oldPos.x-transform.position.x) < 0.1){
                    pre = 2 ;// left/right
                }
            }
            else 
                pre = 1;
            oldPos = transform.position;


            //if stuch infront
            //if (Mathf.Abs(transform.position.x-transform.localScale.x - (other.transform.position.x+other.transform.localScale.x)) < 0.1){
            /*if (transform.position.x > other.transform.position.x){
                 Debug.Log("Front"+id.ToString());
                 front = true;
            }
            if (Mathf.Abs(transform.position.z - other.transform.position.z) - (transform.localScale.z+other.transform.localScale.z)
                Debug.Log("Side"+id.ToString());
                side = true;
            }*/
        }

    }

     
     void OnCollisionExit(Collision col)
     {
         if (col.gameObject.CompareTag("Barrier")){
            pre = 0;
            side = false;
            front = false;
     }
     }
}
