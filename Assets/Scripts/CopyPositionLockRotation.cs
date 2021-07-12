using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPositionLockRotation : MonoBehaviour
{

    public GameObject parent = null;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
        
        if(lockX)
            transform.eulerAngles =new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        if(lockY)
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

        if(lockZ)
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);


    }
}
