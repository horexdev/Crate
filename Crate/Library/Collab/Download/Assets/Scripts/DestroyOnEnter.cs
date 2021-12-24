using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Score")
        {
            Debug.Log("Score Objects Deleted!");
            Destroy(other.gameObject, 2f);
        }
        else if (other.gameObject.tag == "Die")
        {
            Debug.Log("GameOver Objects Deleted!");
            Destroy(other.gameObject);
        }
    }
}
