using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_enemy : MonoBehaviour
{
    public float speed;
    public float TDT;
    public CharacterController enemy;   
    private float rotate_enamy;
    public float gravity;
    private float verticalVelocity;
    private float vx;
    private float vy;
    public GameObject character;
    private RaycastHit hit_character;



    void Start()
    {
        enemy = transform.GetComponent<CharacterController>();
    }


    void Update()
    {
        TDT = Time.deltaTime;
        move_logic();
        Gravity();
        return_axis_z();
        kill_character();

    }
    void move_logic()
    {
        Vector3 right = transform.right * speed * TDT;
        enemy.Move(right);
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "rotate_zone")
        {

            enemy.transform.rotation = Quaternion.Euler(0, rotate_enamy += 180, 0);
        }
        if (other.transform.gameObject.tag == "isAlive")
        {
            character.tag = "isDead";
            speed = 0;
        }
    }

    void Gravity()
    {
        verticalVelocity -= gravity * TDT;
        Vector3 gravity_vector = new Vector3(0, verticalVelocity, 0);
        enemy.Move(gravity_vector);
    }


    void return_axis_z()
    {
        if (enemy.transform.position.z != 0)
        {
            enemy.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }
    void kill_character()
    {
        if (_killed_right() || _killed_left())
        {
            if (hit_character.transform.gameObject == character)
            {
                character.tag = "isDead";
                speed = 0;
            }
        }
    }


    bool _killed_right()
    {
        return (Physics.Raycast(transform.position, Vector3.right, out hit_character, 0.35f, 1, QueryTriggerInteraction.Ignore));
    }

    bool _killed_left()
    {
        return (Physics.Raycast(transform.position, -Vector3.right, out hit_character, 0.35f, 1, QueryTriggerInteraction.Ignore));
    }
}

