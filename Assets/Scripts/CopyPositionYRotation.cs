using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPositionYRotation : MonoBehaviour
{

    public GameObject parent = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;


        Vector3 v3 = new Vector3(0,0,0);
        v3.y = parent.transform.localRotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(v3);
    }
}
