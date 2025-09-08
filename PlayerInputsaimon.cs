using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsaimon : MonoBehaviour
{
    [Header("Sprite設定")]
    public Sprite right_SR;
    public Sprite forward_SR;
    public Sprite left_SR;
    public Sprite Back_SR;
    protected SpriteRenderer now_SR;

    [Header("移動設定")]
    public float RecastTime = 0.1f;
    public float moveSpeed = 1f;
    private float time = 0f;
    private Vector2 inputValue;

    [Header("マップ判定設定")]
    public LayerMask wallLayer;

    [Header("蛇の体判定")]
    public LayerMask SnakeBodyLayer;

    [Header("前方壁判定のBox設定")]
    [SerializeField] protected Vector2 boxSize = new Vector2(0.8f, 0.8f);
    [SerializeField] private float boxDistance = 1.0f;

    protected List<Vector3> previousPositions = new List<Vector3>();
    protected List<Transform> snakeParts = new List<Transform>();
    protected Vector2 facingDirection = Vector2.down;

    [Header("スクリプトのアタッチ")]
    public ScoreManagersaimon _ScoreManagersaimon;
    public PlayerAnimation _PlayerAnimation;
    public GameManager _GameManager;

    [Header("プレハブ変更設定")]
    public GameObject newPrefab;
    public LayerMask targetLayer;
    public float distance = 1f;

    [Header("エフェクトリスト")]
    public GameObject[] effects;

    


    protected virtual void Start()
    {
        now_SR = GetComponent<SpriteRenderer>();
        snakeParts.Add(this.transform);
        previousPositions.Add(transform.position);
    }


    public void OnNavigate(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>();
    }

    protected void HandleMovement()
    {
        time += Time.deltaTime;

        if (time < RecastTime) return;

        Vector3 direction = Vector3.zero;

        if (inputValue.x <= -0.5f) direction = Vector3.left;
        else if (inputValue.x >= 0.5f) direction = Vector3.right;
        else if (inputValue.y <= -0.5f) direction = Vector3.down;
        else if (inputValue.y >= 0.5f) direction = Vector3.up;

        if (direction == Vector3.left) now_SR.sprite = left_SR;
        else if (direction == Vector3.right) now_SR.sprite = right_SR;
        else if (direction == Vector3.up) now_SR.sprite = forward_SR;
        else if (direction == Vector3.down) now_SR.sprite = Back_SR;

        if (direction != Vector3.zero)
        {
            Vector3 oldHeadPos = transform.position;
            Vector3 nextPos = transform.position + direction * moveSpeed;

            if (!IsWalkable(nextPos))
            {
                return;
            }
            if(!SnakeCheck(nextPos))
            {
                return;
            }

            transform.position = nextPos;

            previousPositions.Insert(0, oldHeadPos);
            while (previousPositions.Count > snakeParts.Count)
            {
                previousPositions.RemoveAt(previousPositions.Count - 1);
            }

            for (int i = 1; i < snakeParts.Count; i++)
            {
                if (snakeParts[i] != null)
                {
                    snakeParts[i].position = previousPositions[i - 1];
                }
            }

            time = 0f;
        }

        if (direction != Vector3.zero)
        {
            facingDirection = direction;
        }
    }

    bool IsWalkable(Vector3 currentPos)
    {
        Vector2 center = (Vector2)currentPos + facingDirection.normalized * boxDistance;

        return !Physics2D.OverlapBox(center, boxSize, 0f, wallLayer);
    }

    bool SnakeCheck(Vector3 currentPos)
    {
        Vector2 center = (Vector2)currentPos + facingDirection.normalized * boxDistance;

        return !Physics2D.OverlapBox(center, boxSize, 0f, SnakeBodyLayer);
    }
}
