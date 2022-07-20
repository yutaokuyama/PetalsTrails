using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour
{
    public float velocity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0.0f,0.0f,0.1f)*Time.deltaTime*velocity;
        if(this.transform.position.z > 4.0){
            this.transform.position  =  new Vector3( this.transform.position.x, this.transform.position.y,0.0f);
        }
    }
}
