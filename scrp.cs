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

    float FLT_MAX = 1000000;
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
        Debug.Log("DeltaTime "+Time.deltaTime);
        Debug.Log("local"+transform.position);
        countTime += Time.deltaTime;
        float m = moveForce;
        movementZ = 0;
        move = 0;
        
        Vector3 target = GameObject.Find("Cube").transform.position;
        Vector3 direction = Vector3.Normalize(target- transform.position);
        direction.y = 0.0f;
        Debug.Log("dirZ "+direction.z);
        movementZ =  direction.z;
        move = direction.x;

        if (countTime > Random.Range(1.0f,3.0f)){
            countTime = 0f;
            movementZ +=  Random.Range(-0.5f,0.5f);
            m *= Random.Range(0.5f,1.5f);
        }

        bool t = true;

        for (int i=1;i<=5;i++){
            obs = GameObject.Find("Barrier"+i.ToString()).transform;
  
            //โดนบังไหม
            if (transform.position.x - offset < obs.localPosition.x && transform.position.x > obs.localPosition.x
            && target.x < obs.localPosition.x){


                Vector2 intersection = lineLineIntersection(new Vector2(obs.position.x,obs.position.z+obs.localScale.z/2)
                    ,new Vector2(obs.position.x,obs.position.z-obs.localScale.z/2),new Vector2(transform.position.x,transform.position.z)
                    ,new Vector2(target.x,target.z));
                Debug.Log("intersection "+intersection.x+" "+intersection.y+" / "+transform.position.x+" "+transform.position.z);
                //จุดตัดอยู่ใน range
                if (intersection.y < obs.position.z+obs.localScale.z/2+10 && intersection.y > obs.position.z-obs.localScale.z/2-10)
    
                    //โดนบัง
                
                    if(transform.position.z <= (obs.localScale.z/2 + obs.position.z+Road.rightMost)/2.0 
                    && transform.position.z >= (obs.position.z-obs.localScale.z/2+Road.LeftMost)/2.0){
                     
                        //turn left
                        
                        float x =  transform.position.x - obs.localPosition.x;
                        //left
                        if (Road.rightMost - (obs.localScale.z/2 + obs.position.z) < -(Road.LeftMost - (obs.position.z-obs.localScale.z/2))){
                        if (curBar != i) {
                            curBar = i;
                            dest = Mathf.Abs(transform.position.z -(1.25f*obs.position.z-obs.localScale.z/2+0.75f*Road.LeftMost)/2.0f);
                                _offset = Mathf.Abs(transform.position.x-obs.localPosition.x);
                                Debug.Log("offset"+_offset);
                        }
                        movementZ = dest*ease(_offset-x)*move;
                            
                    }
                    //right
                    else {
                        if ( curBar != i) {
                            curBar = i;
                            dest = Mathf.Abs(transform.position.z -(1.25f*obs.position.z+obs.localScale.z/2+0.75f*Road.rightMost)/2.0f);
                              _offset = Mathf.Abs(transform.position.x-obs.localPosition.x);
                               Debug.Log("offset"+_offset);
                        }
                        movementZ = dest*-ease(_offset-x)*move;
                    }
                }
                //Debug.Log("dest"+dest.ToString());
                Debug.Log("dest "+dest);
                t = false;
                break;
                }  
            
        }
        if (t) dest = -1;
     
        //velocity = new Vector3(move,0f,movementZ) * moveForce;
        float a = Random.Range(0.8f,1.2f);
        float b= Random.Range(0.8f,1.2f);
        Vector3 norm = Vector3.Normalize(new Vector3(move,0f,movementZ));
        Quaternion to = Quaternion.Euler(0, (90-Mathf.Rad2Deg*Mathf.Atan(movementZ/move)) * Random.Range(0.9f,1.1f), 0);
        myRig.velocity =  norm * m;
        transform.rotation = Quaternion.Lerp(transform.rotation,to , Time.deltaTime * 6f);
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
    
    Vector2 lineLineIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
{
    Debug.Log("intersectionParam  " +A.x+" "+A.y+" / "+B.x+" "+B.y+" / "+C.x+" "+C.y+" / "+D.x+" "+D.y);
    // Line AB represented as a1x + b1y = c1
    float a1 = B.y - A.y;
    float b1 = A.x - B.x;
    float c1 = a1*(A.x) + b1*(A.y);
  
    // Line CD represented as a2x + b2y = c2
    float a2 = D.y - C.y;
    float b2 = C.x - D.x;
    float c2 = a2*(C.x)+ b2*(C.y);
  
    float determinant = a1*b2 - a2*b1;
  
    if (determinant == 0)
    {
        // The lines are parallel. This is simplified
        // by returning a pair of FLT_MAX
        return new Vector2(FLT_MAX, FLT_MAX);
    }
    else
    {
        float x = (b2*c1 - b1*c2)/determinant;
        float y = (a1*c2 - a2*c1)/determinant;
        return new Vector2(x, y);
    }
}
}
