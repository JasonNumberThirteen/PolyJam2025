using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed = 0.005f;
    [SerializeField] private int direction = 1;
    private SpriteRenderer spriteRenderer;
    

    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.Translate((direction*Vector2.right)*speed);
    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
        if(direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
   
}
