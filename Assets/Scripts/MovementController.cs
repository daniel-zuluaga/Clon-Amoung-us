using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Rigidbody2D rig2D;
    [SerializeField] private Animator anim;

    private Vector2 targetPosition;
    private Vector2 direction;

    private ChangeFlipXSprite changeFlipXSprite;

    private void Awake()
    {
        changeFlipXSprite ??= new();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        changeFlipXSprite.FlipOrientationPlayer(transform, direction);
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            PlayAnimationWalk(true);

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            direction = new(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
            direction.Normalize();

            Vector2 velocity = direction * speed;

            rig2D.velocity = velocity;
        }
        else
        {
            ZeroPositionPlayIdleAnim();
        }
    }

    private void ZeroPositionPlayIdleAnim()
    {
        PlayAnimationWalk(false);
        rig2D.velocity = Vector2.zero;
    }

    private void PlayAnimationWalk(bool playAnim)
    {
        anim.SetBool("isWalking", playAnim);
    }
}
