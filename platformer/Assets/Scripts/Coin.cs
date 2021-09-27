using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public GameObject character;
    public GameObject coin;
    private RaycastHit hit_character;
    public GameObject enamy;


    void Start()
    {

    }


    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.gameObject.tag == "isAlive")
        {
        
            coin.SetActive(false);
        }
    }

}





