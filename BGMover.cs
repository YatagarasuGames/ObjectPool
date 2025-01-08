using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    private float moveSpeed = 2f;

    private void Update()
    {
        Vector2 direction = Vector2.down*moveSpeed*Time.deltaTime;
        transform.Translate(direction);
    }
}
