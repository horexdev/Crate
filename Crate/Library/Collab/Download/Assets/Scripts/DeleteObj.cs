using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteObj : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Score" || other.gameObject.tag == "Die")
        {
            Debug.Log("Deleted");
            Destroy(other.gameObject);
        }
    }
}