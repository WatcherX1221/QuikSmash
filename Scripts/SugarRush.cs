using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
    Vector2 grappleTarget;
    public GameObject grapple;
    public GameObject existGrapple;

    public bool usingGrapple;

    public Vector3 cursorWorldPos;
    public float grapplePullSpeed = 2f;

    public Vector2 storedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        usingGrapple = false;
        GetComponent<Collider2D>().layerOverridePriority = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!usingGrapple)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Clicked");
                Grapple();
            }
        }

        if(usingGrapple)
        {
            if(existGrapple.GetComponent<SRGrapple>().grappleAnchored)
            {
                Vector2 grapplePull = Vector2.MoveTowards(transform.position, existGrapple.transform.position, Time.deltaTime * grapplePullSpeed);
                transform.position = grapplePull;
            }
        }
        
    }

    public void Grapple()
    {
        Debug.Log("Grapple Function Called");
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        cursorWorldPos = worldPos;

        GameObject newGrapple = Instantiate(grapple, transform.position, transform.rotation);
        existGrapple = newGrapple;
    }
}
