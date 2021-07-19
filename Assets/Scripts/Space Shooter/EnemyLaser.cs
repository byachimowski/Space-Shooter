﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down) * _speed * Time.deltaTime);

        if (transform.position.y < -4f)
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        
    }
}
