using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSFX : MonoBehaviour
{
    float speed = 5f;
    float moveX;
    float moveZ;
    Rigidbody rigid;
    AudioSource audioSource;
    bool isMoving = false;

    private void Start()
    {
        rigid = GetComponentInParent<Rigidbody>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        MoveSfx();
    }

    void MoveSfx()
    {
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        rigid.velocity = new Vector3(moveX, rigid.velocity.y, moveZ);

        if (rigid.velocity.x != 0 && rigid.velocity.z != 0)
        {
            isMoving = true;
        }
        else
            isMoving = false;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }

        }
    }
}
