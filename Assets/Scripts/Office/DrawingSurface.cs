using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingSurface : MonoBehaviour
{
    public bool drawingEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.parent.gameObject.transform.eulerAngles.y);
        if(drawingEnabled)
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
}
