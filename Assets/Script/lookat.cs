using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public GameObject obj;
    // Update is called once per frame
    
    private void Start()
    {
           
    }
    void Update()
    {
       
        transform.LookAt(obj.transform.position);     
        
    }
}
