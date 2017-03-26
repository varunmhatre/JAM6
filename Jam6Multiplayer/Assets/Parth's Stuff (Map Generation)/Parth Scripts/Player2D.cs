using UnityEngine;
using System.Collections;

public class Player2D : MonoBehaviour
{

    Rigidbody2D rb2D;
    Vector2 velocity;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10;
    }

    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}

/*
 * For testing 2D player controller
 * for 2D player controller, change the rotation of the plane, and the cave mesh. select is2D on the map generator object
 * and change player properties to 2D such as box collider 2d, rigidbody 2d etc. etc
 * Can be removed if not needed
 */