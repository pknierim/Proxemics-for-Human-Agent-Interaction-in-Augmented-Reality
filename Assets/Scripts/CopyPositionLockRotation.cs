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

        Vector3 v3 = new Vector3(0,0,0);

        //if (lockX)
            //v3.x = parent.transform.rotation.eulerAngles.x;
            //transform.eulerAngles =new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //if (lockY)
            //v3.y = parent.transform.rotation.eulerAngles.y;
            //transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

         //if (lockZ)
            v3.y = parent.transform.localRotation.eulerAngles.y;
         //parent.transform.rotation.eulerAngles.y;
        //transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
         transform.rotation = Quaternion.Euler(v3);
    }
}
