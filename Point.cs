using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ryan Smith 2D Claw Mini-game Smaller Script

public class Point : MonoBehaviour
{

    [Header("Appropriate Tag")]
    public string objectTag; // Always change in inspector depending on gameobject script is on

    [Header("Scoring Points")]
    private static int clawScore; // Static, so script can be re-used but with same text
    public Text clawScoreUIText;

    //-----------------------------------Start is called once upon creation-------------------------
    private void Start()
    {
        clawScore = 0; // In start so score isn't kept on restart
    }

    //-----------------------------------Update is called once per frame----------------------------
    void Update()
    {
        clawScoreUIText.text = clawScore.ToString(); // Score shown through UI
    }

    //-----------------------------------Collision----------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(objectTag)) // Checks if the tag is the same as the set one in inspector (so script can be changed and be reused)
        {
            Destroy(collision.gameObject); // Removes the object to create the illusion of it going in, regardless of tag
            clawScore += 10; // Gives player points
        }

        else
        {
            Destroy(collision.gameObject);

            if (clawScore > 0)
            {
                clawScore -= 10; // Removes points if more than 0, so no negatives

            }
        }
    }
}