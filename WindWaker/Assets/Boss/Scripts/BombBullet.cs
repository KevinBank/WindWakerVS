﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [SerializeField] private GameObject zelda;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
        }
        Destroy(this.gameObject);
    }
}
