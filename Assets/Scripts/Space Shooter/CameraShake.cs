﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed");
            StartCoroutine(Shake(.3f, .3f));

        }
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("Camera Shake");
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x =Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;

    }
}
