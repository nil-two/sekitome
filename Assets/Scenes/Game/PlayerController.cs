using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;

    void Start()
    {
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + dx, screenMinPos.x, screenMaxPos.x),
                transform.position.y,
                transform.position.z);
    }
}
