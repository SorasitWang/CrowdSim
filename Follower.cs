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
    private float oldRot = 0.0f;
    private bool side=false,front=false;
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

       void FixedUpdate()
    {

        fol = GameObject.Find("Armature.006").transform;
        Vector3 tmp = new Vector3(fol.position.x,fol.position.y,fol.position.z);
        direction = Vector3.Normalize(fol.position - transform.position);
         
        float distance = Vector3.Distance(transform.position,fol.position);

        if (pre==1) direction = new Vector3(-1,0,0);
        else if (pre==2)  direction = new Vector3(0,0,Mathf.Sign(tmp.z-transform.position.z));
        myRig.velocity = direction * calVelo(distance)* Random.Range(0.95f,1.05f);
        transform.Rotate(0,oldRot-Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x) ,0);
        oldRot = Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x);

    }

    float calVelo(float x){
        return Mathf.Exp(Mathf.Min(x,30)/7)-1.0f;
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
