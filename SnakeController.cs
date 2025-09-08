using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SnakeController : PlayerInputsaimon
{

    [Header("ヘビパーツのマテリアル設定")]
    public Sprite cubeMaterial;
    public Vector2 cubeScale = Vector2.one * 0.8f;

    private bool isStunned = false;

    protected override void Start()
    {
        base.Start();
        now_SR.sprite = forward_SR;
    }

    void Update()
    {
        if (isStunned) return;

        HandleMovement();

        Vector3 direction = Vector3.zero;

        if (direction != Vector3.zero)
        {
            Vector3 nextPos = transform.position + direction * moveSpeed;

            snakeParts[0].position = nextPos;
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            Grow();
        }
    }


    public void Grow()
    {
        snakeParts.RemoveAll(t => t == null);
        GameObject newPart = new GameObject("SnakeBody 1");

        newPart.transform.localScale = cubeScale;
        newPart.tag = "SnakeBody";
        newPart.layer = 6;
        BoxCollider2D collider = newPart.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.8f, 0.8f);

        if (cubeMaterial != null)
        {
            SpriteRenderer renderer = newPart.AddComponent<SpriteRenderer>();
            renderer.sprite = cubeMaterial;

        }


        Transform lastPart = snakeParts[snakeParts.Count - 1];
        newPart.transform.position = lastPart.position;
        snakeParts.Add(newPart.transform);
        previousPositions.Add(newPart.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag != "Snake") return;

        if (other.gameObject.CompareTag("OrangeGem"))
        {
            _ScoreManagersaimon.GemCountUp();
            Instantiate(effects[0], this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if(CompareTag("Snake"))
            {
                Grow();
            }
        }
        else if (other.gameObject.CompareTag("BlueGem"))
        {
            _ScoreManagersaimon.GemCountUp();
            Instantiate(effects[1], this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if (CompareTag("Snake"))
            {
                Grow();
            }
        }
    }

    public void DeleteAllWithTag(string tagName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

        snakeParts.RemoveAll(t => t == null);
    }

    public void StartStun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}