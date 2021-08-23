﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed =8f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }

    }

    void MoveUp()
    {
        transform.Translate((Vector3.up) * _speed * Time.deltaTime);

        if (transform.position.y > 9f)
        {
            //Check if this object has a parent.
            // Destroy the parent too !

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);

        }
    }

    void MoveDown()
    {
        transform.Translate((Vector3.down) * _speed * Time.deltaTime);

        if (transform.position.y < -9f)
        {
            //Check if this object has a parent.
            // Destroy the parent too !

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);

        }
    }

    public void AsssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player" && _isEnemyLaser==true)
        {
            Player player = other.GetComponent<Player>();
            if(player!= null)
            {
                player.Damage();
            }
        }
    }
}
