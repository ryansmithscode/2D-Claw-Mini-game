using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ryan Smith 2D Claw Mini-game Script

public class Claw : MonoBehaviour
{
    [Header("Instantiating Objects")]
    public GameObject[] objectArray;
    public GameObject instantiateTransform;
    private int objectNumber;

    [Header("Displaying & Storing Player's Score")]
    private static int scoreCount;
    public Text scoreUI;

    [Header("Movement")]
    public float playerSpeed;

    [Header("Countdown Timer")]
    private float currentTime = 0f;
    public float startTime = 30f;
    public Text countdownUI;

    [Header("Arm Movement")]
    public Animator closeAnimation;
    public Animator closeAnimationR;

    //-----------------------------------Start is called once upon creation-------------------------
    private void Start()
    {
        // Starts Spawning Objects
        StartCoroutine(instantiateObject());

        // Sets To Max
        currentTime = startTime;
    }

    //-----------------------------------Update is called once per frame----------------------------
    private void Update()
    {
        Countdown();

        // Converts The Point Count To Text For HUD
        scoreUI.text = scoreCount.ToString();

        // Player Movement
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) && pos.x > -7.5f) // Magic Numbers for Scene Boundaries
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) && pos.x < 7.5f)
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) && pos.y < 6f)
        {
            transform.position += Vector3.up * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) && pos.y > 2.5f)
        {
            transform.position += Vector3.down * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            closeAnimation.SetBool("Close", true); // Bool, Not Trigger So Holding Key Keeps Closed
            closeAnimationR.SetBool("Close", true);
        }

        else
        {
            closeAnimation.SetBool("Close", false);
            closeAnimationR.SetBool("Close", false);
        }
    }

    //-----------------------------------Object Spawner----------------------------
    private IEnumerator instantiateObject()
    {
        // Picks Random Object Through Correlating Index Number
        objectNumber = Random.Range(0, objectArray.Length);
        
        // Spawning Object/s At The Correct Location
        Instantiate(objectArray[objectNumber], instantiateTransform.transform.position, Quaternion.identity);
        
        // Prevents Loads Spawning At Once 
        yield return new WaitForSeconds(1f);

        StartCoroutine(instantiateObject());
    }

    //-----------------------------------Countdown----------------------------
    private void Countdown()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownUI.text = Mathf.Floor(currentTime).ToString();

        // Restarts Game Once Time Runs Out
        if (currentTime < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

}
