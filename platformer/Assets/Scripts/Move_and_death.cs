using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move_and_death : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    private float TDT;
    public CharacterController character;
    public float gravity;
    public float verticalVelocity;


    public float gravity_isDead;
    public float jumpSpeed_isDead;
    public GameObject charactercamera;

    static string tagIsDead = "isDead";
    static string tagIsAlive = "isAlive";

    private float rotate_z;
    private float rotate_z_min = 0;
    private float rotate_z_max = 90;
    private float active;
    private float active_jump = 1;

    public float quantity_coin;
    public Text coin_counter;
    public GameObject coin;
    
    private float Horizontal;
    private float moveRight;
    private float moveLeft;
    private float moveUp;



    void Start()
    {
        character = transform.GetComponent<CharacterController>();
        character.tag = tagIsAlive;

    }



    void Update()
    {
        TDT = Time.deltaTime;
        _jumpDead();
        moveLogic();
        jumpLogic();
        return_axis_z();
        MoveRight_Button();
        MoveLeft_Button();
    }



    void moveLogic()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        

        if (character.tag == tagIsAlive)
        {

            Vector3 right = transform.right * Horizontal * speed * TDT;
            character.Move(right);
        }
        
    }

   
    void jumpLogic()
    {
        if (character.tag == tagIsAlive)
        {


            if (_isGrounded())
            {

                verticalVelocity = 0;
                if (Input.GetButton("Jump") || moveUp == 1)
                {

                    verticalVelocity = jumpSpeed;
                }


            }
        }
        if (_isСeiling())
        {
            verticalVelocity = 0;

        }


        verticalVelocity -= gravity * TDT;

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        character.Move(jumpVector * TDT);


    }



    bool _isGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, 0.38f, 1, QueryTriggerInteraction.Ignore));

    }



    bool _isСeiling()
    {
        return (Physics.Raycast(transform.position, Vector3.up, 0.35f, 1, QueryTriggerInteraction.Ignore));
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            character.tag = tagIsDead;
            jumpSpeed = 0;
            speed = 0;
        }
        else
        {
            character.tag = tagIsAlive;
        }

        if (coin.tag == "_coin")
        {

            quantity_coin += 1;
            coin_counter.text = "   Coin " + quantity_coin;
        }

        if (other.tag == "GameWin")
        {
            SceneManager.LoadScene("WinScean");
        }

    }


    void _jumpDead()
    {
        if (character.tag == tagIsDead)
        {

            charactercamera.transform.parent = null;

            if (_isGrounded())
            {


                if (active_jump == 1)
                {
                    verticalVelocity = jumpSpeed_isDead;
                    active_jump = 0;
                }

                active = 1;



            }
            if (active == 1)
            {
                character.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(rotate_z += 150 * TDT, rotate_z_min, rotate_z_max));
            }
            if (_isGrounded())
            {
                if (character.transform.rotation == Quaternion.Euler(0, 0, 90))
                {
                    SceneManager.LoadScene("DeathMenu");
                }
            }


            verticalVelocity -= gravity_isDead * TDT;



            Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
            character.Move(jumpVector * TDT);


        }


    }
    void return_axis_z()
    {
        if (character.transform.position.z != 0)
        {
            character.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }

    void MoveRight_Button()
    {

        if (moveRight == 1)
        {


            if (character.tag == tagIsAlive)
            {

                Vector3 right = transform.right * 1 * speed * TDT;
                character.Move(right);
            }
        }


    }

    public void MoveRight_Button_Down()
    {
        moveRight = 1;
    }
    public void MoveRight_Button_Up()
    {
        moveRight = 0;
    }
    public void MoveRight_Button_Exit()
    {
        moveRight = 0;
    }


    void MoveLeft_Button()
    {

        if (moveLeft == 1)
        {


            if (character.tag == tagIsAlive)
            {

                Vector3 right = transform.right * -1 * speed * TDT;
                character.Move(right);
            }
        }


    }

    public void MoveLeft_Button_Down()
    {
        moveLeft = 1;
    }
    public void Moveleft_Button_Up()
    {
        moveLeft = 0;
    }
    public void Moveleft_Button_Exit()
    {
        moveLeft = 0;
    }

    public void Jump_Button_Down()
    {
        moveUp = 1;
    }

    public void Jump_Button_Up()
    {
        moveUp = 0;
    }

    public void Jump_Button_Exit()
    {
        moveUp = 0;
    }



}
