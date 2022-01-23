using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public Vector3 restAngle = new Vector3(0f, 0f, 0f);
    public Vector3 restPosition;
    public Vector3 drawingAngle = new Vector3(-90f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        restPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
