using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public float MovementSpeed = 0.1f;
    public float Limit = 12;
    public float PlayerSize = 2;
    public float BouncyForce = 40;
    public bool Player2;
    public UnityEvent Splat;
    void Update()
    {
        if (!Player2) {
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < Limit) {
                transform.Translate(MovementSpeed, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -Limit) {
                transform.Translate(-MovementSpeed, 0, 0);
            }
        }
        else {
            if (Input.GetKey(KeyCode.D) && transform.position.x < Limit) {
                transform.Translate(MovementSpeed, 0, 0);
            }

            if (Input.GetKey(KeyCode.A) && transform.position.x > -Limit) {
                transform.Translate(-MovementSpeed, 0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Rigidbody2D orb = other.rigidbody;
        orb.velocity = Vector2.zero;
        float collisionDeltaX = other.GetContact(0).point.x-transform.position.x;
        float normalized = collisionDeltaX / PlayerSize;
        orb.AddForce(new Vector2(normalized,0.5f).normalized*BouncyForce,ForceMode2D.Impulse);
        Splat.Invoke();

    }
}
