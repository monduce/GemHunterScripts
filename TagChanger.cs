using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChanger : MonoBehaviour
{
    [Header("点滅設定")]
    public float blinkDuration = 3f;
    public float blinkInterval = 0.2f;
    public float colliderOFFTime = 3f;
    private Renderer objRenderer;

    private float startTime;
    private bool isSwitching = false;

    public SnakeController snakeController;
     PlayerInputsaimon playerInputsaimon;

    public float _stanTime;

    private void OnEnable()
    {
        startTime = Time.time;
        objRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            if (Time.time - startTime < colliderOFFTime)
            {
                col.enabled = false;
            }
            else
            {
                col.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isSwitching) return;

        if (gameObject.tag == "Snake" && other.CompareTag("Hunter"))
        {
            TagChanger otherTagChanger = other.GetComponent<TagChanger>();
            if (otherTagChanger != null && otherTagChanger.isSwitching) return;

            StartCoroutine(SwitchRoles(other));
        }
    }

    private IEnumerator SwitchRoles(Collider2D other)
    {
        isSwitching = true;

        TagChanger otherTagChanger = other.GetComponent<TagChanger>();
        if (otherTagChanger != null)
        {
            otherTagChanger.isSwitching = true;
        }

        if (snakeController != null)
        {
            snakeController.DeleteAllWithTag("SnakeBody");
        }

        ChangeToHunter();
        StartCoroutine(BlinkCoroutine());

        if (otherTagChanger != null)
        {
            otherTagChanger.ChangeToSnake();
            otherTagChanger.StartCoroutine(otherTagChanger.BlinkCoroutine());
        }

        yield return new WaitForSeconds(blinkDuration + 0.1f);

        isSwitching = false;

        if (otherTagChanger != null)
        {
            otherTagChanger.isSwitching = false;
        }
    }

    public void ChangeToSnake()
    {
        gameObject.tag = "Snake";

        PlayerController player = GetComponent<PlayerController>();
        SnakeController snake = GetComponent<SnakeController>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null) player.enabled = false;
        if (snake != null)
        {
            snake.enabled = true;
            snake.StartStun(_stanTime);
            spriteRenderer.sprite = snake.forward_SR;
        }
    }


    public void ChangeToHunter()
    {
        gameObject.tag = "Hunter";

        PlayerController player = GetComponent<PlayerController>();
        SnakeController snake = GetComponent<SnakeController>();

        if (player != null) player.enabled = true;
        if (snake != null) snake.enabled = false;
    }

    private IEnumerator BlinkCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            if (objRenderer != null)
            {
                objRenderer.enabled = !objRenderer.enabled;
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (objRenderer != null)
        {
            objRenderer.enabled = true;
        }
    }
}
