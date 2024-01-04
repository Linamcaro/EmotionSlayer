using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollection : MonoBehaviour
{

    [SerializeField] private string playerCollisionTag;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerCollisionTag)
        {
            OrbCollected.Instance.IncreaseScore();
            Destroy(gameObject);
        }

    }

   
}

