using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動床
/// </summary>
public class MoveGround : MonoBehaviour
{
    //変数定義
    [SerializeField] private Rigidbody2D rb = default;
    [Tooltip("相対的な目的地")] [SerializeField] private Vector2 moveDirection = default;
    [Tooltip("移動速度")] [SerializeField] private float moveSpeed = 2;
    [Tooltip("初期値")] private Vector3 defaultPos;
    [Tooltip("目的地")] private Vector3 targetPos;
    private bool isReturn;

    void Start()
    {
        defaultPos = transform.position;
        targetPos = defaultPos + (Vector3)moveDirection;
    }

    private void FixedUpdate()
    {
        // 移動
        transform.position = isReturn ?
            Vector3.MoveTowards(transform.position, defaultPos, moveSpeed * Time.deltaTime) : // 元の座標に戻る
            Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); // 目的地に進む

        // 目的地に着いたら反転する
        if ((Vector3.Distance(transform.position, targetPos) < 0.001) ||
            (Vector3.Distance(transform.position, defaultPos) < 0.001))
        {
            isReturn = !isReturn;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 触れたプレイヤーを子オブジェクトにする
        if (col.gameObject.name.Equals("Player"))
        {
            col.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            col.transform.SetParent(null);
        }
    }
}
