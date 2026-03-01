using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
    private Vector3 tempPos;

    [SerializeField]
    private float minX,maxX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // DON'T immediately access transform
        GameObject obj = GameObject.FindWithTag("Player");

        if (obj != null)
            player = obj.transform;
        //Debug.Log("The selected index: " + GameManager.instance.CharIndex);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj != null)
                player = obj.transform;
            else
                return;
        }

        tempPos = transform.position;
        tempPos.x = player.position.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;
    }
}
