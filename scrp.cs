using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrp : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 velocity;
     private float movementZ=0.0f,move=-2f;
    [SerializeField]
     private float moveForce = 15;
    private Rigidbody myRig;
    private Transform obs;
    private float countTime = 0.0f;

    private float curBar = -1;
    private float offset = 60f;
    private float _offset = 60f;
    private float dest = -1f;

    private float oldRot = 0.0f;

    public static Vector3 last; 
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame

    float ease(float x){
        if (x >= _offset/2){
            return Mathf.Pow(_offset-x,2)/(Mathf.Pow(_offset,3)/12.0f);
        }
        else{
            return Mathf.Pow(x,2)/(Mathf.Pow(_offset,3)/12.0f);
        }
    }
    void FixedUpdate()
    {
        Debug.Log("local"+transform.position);
        countTime += Time.deltaTime;
        float m = moveForce;
        if (countTime > Random.Range(1.0f,3.0f)){
            countTime = 0f;
            movementZ =  Random.Range(-0.5f,0.5f);
            m *= Random.Range(0.5f,1.5f);
        }

        bool t = true;

        for (int i=1;i<=5;i++){
            obs = GameObject.Find("Barrier"+i.ToString()).transform;
  
            //โดนบัง
            //Debug.Log((transform.position.z <= obs.localScale.z/2 + obs.position.z) && (transform.position.z >= obs.position.z-obs.localScale.z/2));
             
            if (transform.position.x - offset < obs.localPosition.x && transform.position.x > obs.localPosition.x ){
                /*Debug.Log("First");
                Debug.Log(transform.position.z);
                Debug.Log(obs.localScale.z/2 + obs.position.z);*/
                if(transform.position.z <= (obs.localScale.z/2 + obs.position.z+Road.rightMost)/2.0 
                && transform.position.z >= (obs.position.z-obs.localScale.z/2+Road.LeftMost)/2.0){
                    Debug.Log(i.ToString());
                    //turn left
                     
                    float x =  transform.position.x - obs.localPosition.x;
                    //left
                    if (Road.rightMost - (obs.localScale.z/2 + obs.position.z) < -(Road.LeftMost - (obs.position.z-obs.localScale.z/2))){
                       if (curBar != i) {
                           curBar = i;
                           dest = Mathf.Abs(transform.position.z -(obs.position.z-obs.localScale.z/2+Road.LeftMost)/2.0f);
                            _offset = Mathf.Abs(transform.position.x-obs.localPosition.x);
                            Debug.Log("offset"+_offset);
                       }
                       movementZ = dest*ease(_offset-x)*move;
                        
                    }
                    //right
                    else {
                        if ( curBar != i) {
                            curBar = i;
                            dest = Mathf.Abs(transform.position.z -(obs.position.z+obs.localScale.z/2+Road.rightMost)/2.0f);
                              _offset = Mathf.Abs(transform.position.x-obs.localPosition.x);
                               Debug.Log("offset"+_offset);
                        }
                        movementZ = dest*-ease(_offset-x)*move;
                    }
                //Debug.Log("dest"+dest.ToString());
                Debug.Log("dest "+dest);
                t = false;
                break;
                }  
            }
        }
        if (t) dest = -1;
     
        //velocity = new Vector3(move,0f,movementZ) * moveForce;
        float a = Random.Range(0.8f,1.2f);
        float b= Random.Range(0.8f,1.2f);
        Vector3 norm = Vector3.Normalize(new Vector3(move,0f,movementZ));
        Quaternion to = Quaternion.Euler(0, (90-Mathf.Rad2Deg*Mathf.Atan(movementZ/move)) * Random.Range(0.9f,1.1f), 0);
        myRig.velocity =  norm * m;
        transform.rotation = Quaternion.Lerp(transform.rotation,to , Time.deltaTime * 3f);
        //transform.Rotate(0,oldRot-Mathf.Rad2Deg*Mathf.Atan(norm.z/norm.x) ,0);
        oldRot = Mathf.Rad2Deg*Mathf.Atan(norm.z/norm.x);
        //Debug.Log(Vector3.Normalize(new Vector3(move,0f,movementZ)));
           Debug.Log("Y"+Mathf.Rad2Deg*Mathf.Atan(norm.z/norm.x)); 
   
        findLastChick();
    }

     void findLastChick(){
        GameObject[] respawns =  GameObject.FindGameObjectsWithTag("Crowd");
        last = new Vector3(-1000,0,0);
        foreach (GameObject chick in respawns)
        {
            if (chick.transform.position.x > last.x && chick.GetComponent<Follower>().pre == 0){
                last = chick.transform.position;
            }
        }
        Debug.Log("last"+last);

    }
}
