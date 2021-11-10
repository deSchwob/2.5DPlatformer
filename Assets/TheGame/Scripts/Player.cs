using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controls of the player character.
public class Player : MonoBehaviour
{
    // Movingspeed of the player character.
    public float speed = 0.05f;

    /// <summary>
    /// Verstärkung der Gravitation damit Person schneller fällt
    /// </summary>
    public float extraGravity = -20f;

    /// <summary>
    /// DIe Kraft mit der nach oben gesprungen wird
    /// </summary>
    public float jumpPush = 1f;

    //Grafical model to spin the character
    public GameObject model;

    //Winkel zu dem sich die Figur um die eigene Achse (=Y) drehen soll
    private float towardsY = 0f;

    /// <summary>
    /// Zeiger auf die Physik-Komponente
    /// </summary>
    private Rigidbody rigid;


    /// <summary>
    /// Zeiger auf die Animations-Komponente
    /// </summary>
    private Animator anim;


    /// <summary>
    /// Ist Figur auf dem Boden?
    /// Sonst fäll oder springt sie
    /// </summary>
    private bool onGround = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float h = Input.GetAxis ("Horizontal"); //Eingabesignal fürs laufen
        float j = Input.GetAxis ("Jump"); //Eingabesignal fürs springen
        anim.SetFloat("forward", Mathf.Abs(h));

        //Vorwärts bewegen:
        transform.position += h * speed * transform.forward;

        //Drehen:
        if (h > 0f)
            towardsY = 0f;
        else if(h < 0f)
            towardsY = -180f;

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime*10f);

        //Springen
        RaycastHit hitInfo;
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f);
        anim.SetBool("grounded", onGround);
        if (j > 0f && onGround)
        {
            Vector3 power = rigid.velocity;
            power.y = jumpPush;
            rigid.velocity = power;
        }
        rigid.AddForce(new Vector3(0f, extraGravity, 0f));
    }
}
