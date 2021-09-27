using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_fall : MonoBehaviour
{
    public float gravity;
    public float verticalVelocity;
    private float TDT;
    //public CharacterController stone_fall;
    public GameObject character;
    private RaycastHit hit_character;
    public float height;
    public Rigidbody rb;


    void Start()
    {
        //stone_fall = transform.GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();


    }


    void Update()
    {
        TDT = Time.deltaTime;
        //the_fall();
        return_stone();
    }

    /*void the_fall()
    {
        verticalVelocity -= gravity * TDT;
        Debug.Log(verticalVelocity);

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        stone_fall.Move(jumpVector * TDT);

    }*/
    void return_stone()

    {

        if (_isGrounded())
        {
            rb.velocity = new Vector3 (0, 0, 0);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);

            /*if(hit_character.transform.gameObject == character)
            {
                character.tag = "isDead";
                stone_fall.transform.position = new Vector3(transform.position.x, height, transform.position.z);
            }*/
        }

    }


    bool _isGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, out hit_character, 0.35f, 1, QueryTriggerInteraction.Ignore));

    }


    void OnCollisionEnter(Collision collision)
    {
        if (character.transform.position == collision.transform.position)
        {
            character.tag = "isDead";
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            
        }

    }





}

