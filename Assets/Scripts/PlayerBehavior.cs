using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Text winText;
    private bool dead = false;
    private Vector3 zeroLoc = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody player;
    public int speed;
    public int lives;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        winText.text = "";
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            float horizontalMov = Input.GetAxis("Horizontal"); //x
            float verticalMov = Input.GetAxis("Vertical"); //y
            Vector3 movement = new Vector3(horizontalMov, 0.0f, verticalMov);
            player.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
    //checks if "hit" a collectible. If so, "pick it up" by removing it from the board
        if (other.gameObject.CompareTag("Pick Up"))
        {
                other.gameObject.SetActive(false);

            if (NoPickUpsLeft())
            //You win if you collect all collectible items.
            {
                Debug.Log("Win Code Here");
                winText.text = ("Hey man, you won");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    //if the user hit the wall, reset the game. You are not allowed to hit walls!
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit Wall");
            lives--;
            Debug.Log("A life has been removed, as you hit the wall");
            if (lives <= 0)
            {
                Debug.Log("Game Over");
                dead = true;
            }
            else
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                player.transform.position = zeroLoc ; 
            }
        }
    }

    private bool NoPickUpsLeft()
    {
        GameObject[] pickUpObjects = GameObject.FindGameObjectsWithTag("Pick Up");
        return (pickUpObjects.Length == 0) ? true : false;
    }
}
