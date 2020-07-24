using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IshitsumikoBehaviour : MonoBehaviour
{
    public Sprite ishitsumikoUpSprite;
    public Sprite ishitsumikoLeftSprite;
    public Sprite ishitsumikoRightSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal");

        if (dx == 0f && spriteRenderer.sprite != ishitsumikoUpSprite)
        {
            spriteRenderer.sprite = ishitsumikoUpSprite;
        }
        else if (dx < 0f && spriteRenderer.sprite != ishitsumikoLeftSprite)
        {
            spriteRenderer.sprite = ishitsumikoLeftSprite;
        }
        else if (dx > 0f && spriteRenderer.sprite != ishitsumikoRightSprite)
        {
            spriteRenderer.sprite = ishitsumikoRightSprite;
        }
    }
}
