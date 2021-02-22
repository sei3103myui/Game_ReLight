using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    public enum MoveMode
    {
        MoveX,
        MoveY
    }
    
    [Header("どの方向に移動するのか選択してください")]
    public MoveMode moveMode;

    
    [Header("どのくらい移動するのか")]
    public float movePower = 2.0f;
    private Rigidbody2D stageRb;

    private bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        stageRb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            switch (moveMode)
            {
                case MoveMode.MoveX:
                    stageRb.velocity = new Vector2(Mathf.Sin(Time.time) * movePower, 0f);
                    break;
                case MoveMode.MoveY:
                    stageRb.velocity = new Vector2(0f, Mathf.Sin(Time.time) * movePower);
                    break;
                
            }
        }
        
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }
}
