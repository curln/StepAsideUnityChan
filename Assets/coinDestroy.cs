using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
        Debug.Log("âÊñ äOÇ…èoÇ‹ÇµÇΩ");
    }
}
