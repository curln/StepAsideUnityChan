using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
     GameObject unityChan;
    // Start is called before the first frame update
    void Start()
    {
        unityChan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        if (unityChan.transform.position.z - 5 > transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
   
}
