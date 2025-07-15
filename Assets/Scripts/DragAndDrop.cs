using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    bool moveAllowed;
    Collider2D col;
    public GameObject selectionEffect;
    public GameObject deathEffect;
    private GameMaster gm;
    public int countCollisions = 0;
    public int netScore = 5;



    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;


 
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        col = GetComponent<Collider2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            startPoint = cam.ScreenToWorldPoint(touch.position);


            if (touch.phase == TouchPhase.Began) {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchedCollider) {
                    Instantiate(selectionEffect, transform.position, Quaternion.identity);
                    moveAllowed = true;
                }
            }
            if (touch.phase == TouchPhase.Moved) {
                if (moveAllowed) {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }

            }
            if (touch.phase == TouchPhase.Ended) {
                endPoint = cam.ScreenToWorldPoint(touch.position);
                

                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                rb.AddForce(force * power, ForceMode2D.Impulse);
                
                moveAllowed = false;
            }
        }
    }



    private void OnTriggerEnter2D (Collider2D collision) 
    {
        
        if (collision.tag == "fire") 
        {
            LifeScript.scoreValue -= 1;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
            if (LifeScript.scoreValue == 0) {
                gm.LostGameOver();
            }
        }  
        if (collision.tag == "net") 
        {
            ScoreScript.scoreValue += 5;

            Destroy(gameObject); 
        } 
    }

}
