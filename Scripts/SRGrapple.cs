using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SRGrapple : MonoBehaviour
{
    public bool grappleAnchored;
    public GameObject sugarRush;
    Rigidbody2D rb;

    public float distanceToCursorPoint;
    
    // Start is called before the first frame update 
    void Start()
    {
        sugarRush = GameObject.FindGameObjectWithTag("SugarRush");
        sugarRush.GetComponent<SugarRush>().usingGrapple = true;  
        grappleAnchored = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCursorPoint = Vector2.Distance(transform.position, sugarRush.GetComponent<SugarRush>().cursorWorldPos);
        if(!grappleAnchored)
        {
            Vector2 grappleMovement = Vector2.MoveTowards(transform.position, sugarRush.GetComponent<SugarRush>().cursorWorldPos, Time.deltaTime * 5);
            transform.position = grappleMovement;
        }
        if(grappleAnchored)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            sugarRush.GetComponent<Collider2D>().layerOverridePriority = 1;
        }

        if(distanceToCursorPoint <= 0.5)
        {
            grappleAnchored = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Solid")
        {
            grappleAnchored = true;
        }

        if(collision.collider.tag == "SugarRush")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {   
        sugarRush.GetComponent<SugarRush>().usingGrapple = false;
        sugarRush.GetComponent<Collider2D>().layerOverridePriority = -1;
        sugarRush.GetComponent<Rigidbody2D>().velocity = new Vector2(sugarRush.GetComponent<Rigidbody2D>().velocity.x, 4f);
        sugarRush.GetComponent<BasicMovement>().addedForce = new Vector2(7f, 0f);
    }
}
