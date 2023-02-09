using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float velocity = 12.0f;

    private Rigidbody2D rigidbody;
    private Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mAnimator.SetTrigger("TriggerFlap");
                rigidbody.velocity = Vector2.up * velocity;
            }
        }
    }
}
