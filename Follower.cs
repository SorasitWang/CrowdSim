using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wind {
    public Vector3 direction;
    public float y ;


    public float w;

    public float force;

    public Wind() {
        direction = new Vector3(1,0,0);
        y = 50;
        w = 8;
        force = 6;

    }
} ;
public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform fol;
    private Wind wind;

    private Vector3 direction;

    public int id;

    private Vector3 noise;
    private float memZ = 0f , mass = 1.0f;
    private Rigidbody myRig;
    public int pre = 0;
    private Vector3 oldPos;
    private float oldRot = 0.0f;
    private bool side=false,front=false;
    private float h;

    private bool alive = true , ghost = true;
    private float lifeTime = 4;

    private float _ghostTime,ghostTime = 4;
    GameObject mesh ;

    private Material m_Material;
    private void Awake() {
       myRig = GetComponent<Rigidbody>();
      
    }
    void Start()
    {
        myRig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ 
            | RigidbodyConstraints.FreezePositionY; 
        wind = new Wind();
        mass *= Random.Range(0.5f,1.5f);
        Debug.Log("mass " +mass);
        lifeTime =  5 * Random.Range(0.75f,1.75f);
        ghostTime =  6 * Random.Range(0.75f,2);
        _ghostTime = ghostTime;
        //mesh = GameObject.Find("default");
        m_Material = GetComponent<MeshRenderer>().material; //transform.GetChild(0).gameObject.
        //transform.localScale = transform.localScale * Random.Range(0.85f,1.15f);
        //transform.position.Set(transform.localScale/2.0f;
    }

       void FixedUpdate()
    {
        fol = GameObject.Find("Armature.006").transform;
        if (!alive){
            myRig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; 
            if (ghost){
                if (ghostTime <= 0.0f) ghost = false;
                ghostTime -=Time.deltaTime;
                //mesh.GetComponent<Mesh>().alive = false;
               
                m_Material.color = new Color(1.0f, 1.0f, 1.0f, ghostTime/_ghostTime);
                //myRig.velocity = new Vector3(0,10,0);
                //inert
                //myRig.velocity = new Vector3(Mathf.Min(0,myRig.velocity.x+0.1f),10,Mathf.Max(0,myRig.velocity.z-Mathf.Sign(myRig.velocity.z)*0.1f));

                //wind
                Vector3 w = wind.direction * wind.force * 2/(1+Mathf.Exp(Mathf.Abs((wind.y-transform.position.y)/wind.w)));
                w.y = 5;
                Debug.Log("wind "+w);
                myRig.velocity =2* mass * w;
                
            }
            else{
                Destroy(gameObject);
            }
        
        }
        else {
            if (lifeTime <= 0.0f) alive = false;
            lifeTime -= Time.deltaTime;
            
            Vector3 tmp = new Vector3(fol.position.x,fol.position.y,fol.position.z);
            direction = Vector3.Normalize(fol.position - transform.position);
            
            float distance = Vector3.Distance(transform.position,fol.position);

            if (pre==1) direction = new Vector3(-1,0,0);
            else if (pre==2)  {
                
                direction = new Vector3(0,0,memZ);
            }
            //Mathf.Sign(tmp.z-transform.position.z)
        myRig.velocity = direction * 3*calVelo(distance)* Random.Range(0.95f,1.05f);
        Quaternion to = Quaternion.Euler(0, -Mathf.Rad2Deg*Mathf.Atan(direction.z/-Mathf.Abs(direction.x)) * Random.Range(0.9f,1.1f), 0);
        Debug.Log("tan"+ -Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x));
        //Debug.Log("rotate"+-Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x));
        transform.rotation = Quaternion.Lerp(transform.rotation,to , Time.deltaTime * 3f);
        //transform.Rotate(0,oldRot-Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x) ,0);
        oldRot = Mathf.Rad2Deg*Mathf.Atan(direction.z/direction.x);
        }

        
    }

    float calVelo(float x){
        return Mathf.Max(0.1f,Mathf.Exp(Mathf.Min(x,28)/11)-1.0f);
    }

     private void OnCollisionStay(Collision other) {
         
        if (other.gameObject.CompareTag("Barrier")){

            fol = GameObject.Find("Armature.006").transform;


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
