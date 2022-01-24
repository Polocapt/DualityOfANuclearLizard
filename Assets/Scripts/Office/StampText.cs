using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StampManager SM = GameObject.Find("Stamp").GetComponent<StampManager>();
        SM.stampMark = gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
