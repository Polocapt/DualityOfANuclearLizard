using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterContents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Blabbeur.Objects.PropertyDictionary tempblab = new Blabbeur.Objects.PropertyDictionary("tempblab");
        string txt = Blabbeur.TextGen.Request("LetterBlab", tempblab).Replace('@', '\n');

        TMPro.TMP_Text textcomponent = GetComponent<TMPro.TMP_Text>();
        textcomponent.text = txt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
