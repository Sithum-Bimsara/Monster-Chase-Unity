using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        speed = 7;
    }

    void FixedUpdate()
    {
        Vector2 newVelocity = myBody.linearVelocity;
        newVelocity.x = speed;
        myBody.linearVelocity = newVelocity;
    }
}