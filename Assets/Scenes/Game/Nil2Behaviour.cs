using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nil2Behaviour : MonoBehaviour
{
    public Sprite nil2RedSprite;
    public Sprite nil2BlueSprite;
    public Sprite nil2GreenSprite;
    public Sprite nil2YellowSprite;
    public Sprite nil2PurpleSprite;
    public Sprite nil2OrangeSprite;
    public Sprite nil2RainbowSprite;
    public Sprite nil2WhiteSprite;
    public Sprite nil2GraySprite;
    public Sprite nil2BlackSprite;
    public float fowardSpeed;
    public float rotateSpeed;

    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;

    private SpriteRenderer spriteRender;

    private Vector3 fowardVector;
    private int nil2Type;

    void Start()
    {
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        spriteRender = GetComponent<SpriteRenderer>();

        fowardVector = (new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.4f, -0.6f), 0f)).normalized;
        nil2Type     = GameManager.instance.GetNil2TypeRandom();

        transform.position = new Vector3(Random.Range(screenMinPos.x, screenMaxPos.x), screenMaxPos.y+1, 0f);

        if      (nil2Type == GameManager.NIL2_TYPE_RED)     spriteRender.sprite = nil2RedSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_BLUE)    spriteRender.sprite = nil2BlueSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_GREEN)   spriteRender.sprite = nil2GreenSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_YELLOW)  spriteRender.sprite = nil2YellowSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_PURPLE)  spriteRender.sprite = nil2PurpleSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_ORANGE)  spriteRender.sprite = nil2OrangeSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_RAINBOW) spriteRender.sprite = nil2RainbowSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_WHITE)   spriteRender.sprite = nil2WhiteSprite;
        else if (nil2Type == GameManager.NIL2_TYPE_GRAY)    spriteRender.sprite = nil2GraySprite;
        else if (nil2Type == GameManager.NIL2_TYPE_BLACK)   spriteRender.sprite = nil2BlackSprite;
    }

    void Update()
    {
        transform.position += fowardVector * fowardSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        if (transform.position.x < screenMinPos.x || transform.position.x > screenMaxPos.x)
        {
            fowardVector.x = -fowardVector.x;
        }

        if (transform.position.y > screenMaxPos.y+2)
        {
            Destroy(gameObject);
            return;
        }

        if (transform.position.y < screenMinPos.y-1)
        {
            GameManager.instance.OnNil2DropEvent();
            Destroy(gameObject);
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ishitsumiko")
        {
            CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
            if (circleCollider2D != null)
            {
                Destroy(circleCollider2D);
            }

            if (GameManager.instance.IsAvailable())
            {
                GameManager.instance.OnNil2HitEvent(nil2Type);
                fowardVector.y = Mathf.Abs(fowardVector.y);
                return;
            }
        }
    }
}
