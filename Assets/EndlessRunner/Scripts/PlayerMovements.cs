using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform _sprite;
    [SerializeField] private float _jump = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float _groundDis = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded = false;
    private bool isJump = false;
    private float jumpTimer;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, _groundDis, _groundLayer);

        #region Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJump = true;
            rb.velocity = Vector2.up * _jump;
        }

        if (isJump && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * _jump;

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
            jumpTimer = 0f;
        }
        #endregion

        #region crouch

        if (isGrounded && Input.GetButton("Crouch"))
        {
            _sprite.localScale = new Vector3(_sprite.localScale.x, crouchHeight, _sprite.localScale.z);
        }



        if (Input.GetButtonUp("Crouch"))
        {
            _sprite.localScale = new Vector3(_sprite.localScale.x, 1f, _sprite.localScale.z);
        }

        #endregion
    }
}
