using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeFlipXSprite
{
    public void FlipOrientationPlayer(Transform player, Vector2 targetPosition)
    {
        player.localScale = new Vector2(targetPosition.x < 0 ? -1 : 1, player.localScale.y);
    }

    //public void ChangeFlipSprite(bool flipSprite, SpriteRenderer spriteRenderer)
    //{
    //    spriteRenderer.flipX = flipSprite;
    //}
}
