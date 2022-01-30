using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drawing : MonoBehaviour
{
    Pencil pencil;
    Renderer rend;
    Texture2D texture;
    Vector2 LastMousePos;
    GameObject DrawingSurface;
    public bool PageSigned = false;
    StampManager stamp;
    TaskManager TM;
    GameObject hand;

    granulator granu;

    int pixelsCovered = 0;
    public int pixelsNeeded = 140;
    bool exiting = false;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("PaperManager").GetComponent<PaperManager>().hand;
        granu = GameObject.Find("granulator").GetComponent<granulator>();
        pencil = GameObject.FindGameObjectWithTag("Pencil").GetComponent<Pencil>();
        stamp = GameObject.Find("Stamp").GetComponent<StampManager>();
        TM = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        TM.StartSigningPaper();

        stamp.StampingEnabled = false;
        DrawingSurface = transform.parent.GetComponentInChildren<DrawingSurface>().gameObject;

        rend = DrawingSurface.GetComponent<Renderer>();
        // duplicate the original texture and assign to the material
        texture = Instantiate(rend.material.mainTexture) as Texture2D;
        rend.material.mainTexture = texture;
        
    }


    Vector2 WorldPosToTextureCoord(Vector3 input)
    {
        Vector2 coord = new Vector2(
            (texture.width - texture.width * (input.x - transform.position.x + rend.bounds.size.x / 2) / rend.bounds.size.x),
            (texture.height - texture.height * (input.z - transform.position.z + rend.bounds.size.z / 2) / rend.bounds.size.z)
            );
        
        return coord;
    }

    // Update is called once per frame
    void Update()
    {
        if (!exiting)
        {

        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // if mouse if over paper, display pencil over paper
                    pencil.gameObject.transform.eulerAngles = pencil.drawingAngle;
                    if(!stamp.stamping) hand.SetActive(true);
                    hand.gameObject.transform.position = hit.point;
                    pencil.gameObject.transform.position = hit.point;

                    Vector2 coord = WorldPosToTextureCoord(hit.point);

                    if (Input.GetMouseButton(0))
                    {
                        // mouse is pressed and over paper
                        //if (texture.GetPixel(Mathf.RoundToInt(coord.x), Mathf.RoundToInt(coord.y)).a == 0f)
                        if(!TM.dayIsOver)
                        granu.playing = true;

                        if (LastMousePos == null)
                        {
                            LastMousePos = coord;
                            return;
                        }

                        DrawLine(coord, LastMousePos, Color.black);
                        texture.Apply();
                    }
                    else granu.playing = false;

                    LastMousePos = coord;
                }
                else
                {
                    // if mouse is not over paper, put pencil back
                    RestPencil();
                    granu.playing = false;
                }
            }
        }

        if (!PageSigned && pixelsCovered > pixelsNeeded)// && !Input.GetMouseButton(0))
        {
            stamp.ReadyToStamp();
            PageSigned = true;
        }
        
    }

    void RestPencil() {
        pencil.gameObject.transform.position = pencil.restPosition;
        pencil.gameObject.transform.eulerAngles = pencil.restAngle;
        if(!stamp.stamping) hand.SetActive(false);
    }

    public void FinishDrawing()
    {
        // page signing completed
        if (!exiting)
        {
            
            exiting = true;
            GameObject.Find("PaperManager").GetComponent<PaperManager>().SendPaperToPile();
        }
    }

    public void DrawLine(Vector2 p1, Vector2 p2, Color col)
    {
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        float ctr = 0;
        int pixelz = 0;

        while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y)
        {
            t = Vector2.Lerp(p1, p2, ctr);
            ctr += frac;

            pixelsCovered++;
            pixelz++;

            Vector2[] brushpixels = GetBrushPixels((int)t.x, (int)t.y);

            foreach (Vector2 pixel in brushpixels)
                texture.SetPixel((int)pixel.x, (int)pixel.y, col);

            
        }

        if(p1!=p2)
        Rage.value = Mathf.Min(Rage.value + Mathf.Max(1, pixelz/10), Rage.MaxRage );
        

        TM.UpdatePaperSigningProgress((100 * pixelsCovered) / pixelsNeeded);
    }

    Vector2[] GetBrushPixels (int inputx, int inputy)
    {
        /*
        int brushSize = 5;
        int halfBrushSize = brushSize / 2;
        Vector2 point = new Vector2(inputx, inputy);
        List<Vector2> result = new List<Vector2>();

        for(int x = inputx-halfBrushSize; x<inputx+halfBrushSize; x++)
        {
            for(int y=inputy-halfBrushSize; y<inputy+halfBrushSize; y++)
            {
                Vector2 pix = new Vector2(x, y);
                if ((pix - point).magnitude < halfBrushSize) result.Add(pix);

            }
        }

        return result.ToArray();

        */

        return new Vector2[]
        {
            new Vector2(inputx, inputy),
            new Vector2(inputx-1, inputy),
            new Vector2(inputx+1, inputy),
            new Vector2(inputx, inputy-1),
            new Vector2(inputx, inputy+1)
        };
    }
}
