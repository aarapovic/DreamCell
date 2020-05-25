﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public Transform target;
    public float smoothing;
    
    public Vector2 maxPosition;
    public Vector2 minPosition;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }
    }
    private Vector3 roundPosition(Vector3 position)
    {
        float xOffset = position.x % 0.625f;
        if (xOffset != 0)
        {
            position.x -= xOffset;
        }
        float yOffset = position.y % .0625f;
        if (yOffset != 0)
        {
            position.y -= yOffset;
        }
        return position;
    }
    public void BeginKick()
    {
        Debug.Log("kick");
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo()
    {

        yield return null;
        anim.SetBool("kickActive", false);
    }
}
