using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum JumpMode
    {
        Jump,
        NoJump
    }
    public JumpMode jumpMode;
    [Header("加速度上限")]
    [SerializeField] private float MAX_SPEED = 5.0f;//加速度上限
    [Header("プレイヤーとその子オブジェクト")]
    public GameObject player;
    public GameObject lightImage;
    public GameObject mirraImage;
    [Header("プレイヤーの移動設定")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 moveValue = Vector2.zero;

    [Header("オブジェクトサーチモード設定")]
    public float distance = 10f;
    [SerializeField] private LayerMask lookLayerMask;
    private InputAction lookAction;
    private Vector2 lookValue;
    private SearchMode searchMode;

    private bool isGround = false;
    private bool iswait = false;
    [HideInInspector] public bool isSearchPoint = false;
    [HideInInspector] public bool isTalk = false;
    [HideInInspector] public bool isFinish = false;
    [Header("デフォルトマテリアル")]
    public Material ma;
    [Header("ディフューズマテリアル（暗闇設定）")]
    public Material diffuseMateriar;

    private TalkManager talkManager;
    public GameManager gameManager;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");
        lookAction = playerInput.currentActionMap.FindAction("Look");
        
    }

    void Update()
    {
        
        //      プレイ中のみ移動
        if(GameManager.gameMode == GameManager.GameMode.Play)
        {
            isGround = Physics2D.Linecast(
            transform.position - transform.up * 2f,
                transform.position - transform.up * 1.2f + transform.right * 1.1f,
            layerMask
            ) ||
            Physics2D.Linecast(
              transform.position - transform.up * 2f,
                transform.position - transform.up * 1.2f - transform.right * 1.1f,
                layerMask);

            Debug.DrawLine(
                transform.position - transform.up * 2f,
                transform.position - transform.up * 1.2f + transform.right * 1.1f,
                Color.red
                );

            Debug.DrawLine(
                transform.position - transform.up * 2f,
                transform.position - transform.up * 1.2f - transform.right * 1.1f,
                Color.red
                );


            //ActionMapのMove取得
            moveValue = moveAction.ReadValue<Vector2>();

            //入力値を設定
            moveDirection.x = moveValue.x;
            moveDirection.y = moveValue.y;
        }

    }

    private void FixedUpdate()
    {
        
        if(GameManager.gameMode == GameManager.GameMode.Play)
        {
            if (lightImage.activeInHierarchy)
            {
                lightImage.SetActive(false);
            }
            //if(moveDirection == new Vector2(0,0))
            //{
            //    rb.velocity = default;
            //}
            rb.velocity += moveDirection * Time.fixedDeltaTime * walkSpeed;
            rb.velocity = VelocityLimit(rb.velocity);
        }
        else if(GameManager.gameMode == GameManager.GameMode.Search)
        {
            if (!lightImage.activeInHierarchy)
            {
                lightImage.SetActive(true);
            }
            Reflection();
        }
        else if(GameManager.gameMode != GameManager.GameMode.Play)
        {
            rb.velocity = default;
        }

    }
    /// <summary>
    /// 加速度上限
    /// </summary>
    /// <param name="velocity"></param>
    /// <returns></returns>
    private Vector2 VelocityLimit(Vector2 velocity)
    {
        var v = velocity;
        v.y = 0f;

        if (v.magnitude > MAX_SPEED)
        {
            v = v - (v.normalized * MAX_SPEED);
            v.y = 0f;
            return velocity -= v;
        }
        return velocity;
    }
    /// <summary>
    /// ジャンプありなら
    /// </summary>
    /// <param name="context"></param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if(jumpMode == JumpMode.Jump)
        {
            //接地判定
            if (isGround && GameManager.gameMode == GameManager.GameMode.Play)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

    }
    /// <summary>
    /// サーチライトのRay飛ばす処理
    /// </summary>
    public void Reflection()
    {
        lookValue += lookAction.ReadValue<Vector2>();
        Vector2 newdirection = Vector2.zero;
        
        
        //distance = searchMode.dis;
        //ライトの向きを決める
        switch (searchMode.lightMode)
        {
            case SearchMode.LightMode.Left:
                distance = searchMode.dis;
                newdirection = new Vector2(-player.transform.position.x , -lookValue.y);
                
                break;
            case SearchMode.LightMode.Right:
                distance = searchMode.dis;
                newdirection = new Vector2(player.transform.position.x , lookValue.y);
                
                break;
        }
        Ray2D ray = new Ray2D(player.transform.position, newdirection);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance, lookLayerMask);

        
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        Vector3 spotlight = gameObject.transform.position;
        lightImage.transform.position = new Vector3(spotlight.x + distance, spotlight.y + ray.direction.y * distance, lightImage.transform.position.z);
      
        if (hit.collider)
        {
            
                if (hit.collider.gameObject.tag == "Appear" && hit.collider.gameObject == searchMode.appearObj)
                {
                    //動的にする時間
                    switch (searchMode.appearMode)
                    {
                        case SearchMode.AppearTimeMode.Repeart:
                            searchMode.appearRenderer.material = ma;
                            searchMode.setEvent();
                            break;
                        case SearchMode.AppearTimeMode.Limit:
                            if (!iswait)
                            {
                                iswait = true;
                                searchMode.appearRenderer.material = ma;
                                searchMode.setEvent();
                                StartCoroutine(AppearTime(searchMode.waitTime));
                            }
                            break;
                        case SearchMode.AppearTimeMode.True:
                            searchMode.appearRenderer.material = ma;
                            searchMode.setEvent();
                            break;
                    }

               
            }
            
        }
        else
        {
           
            if (searchMode.appearMode == SearchMode.AppearTimeMode.True)
            {
                searchMode.appearRenderer.material = diffuseMateriar;
            }
           
            
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Search")
        {
            
            mirraImage.SetActive(true);
            isSearchPoint = true;
            searchMode = collision.gameObject.GetComponent<SearchMode>();
        }

        if (collision.gameObject.tag == "Talk")
        {
            isTalk = true;
            talkManager = collision.gameObject.GetComponent<TalkManager>();
            gameManager.talkManager = talkManager;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Search")
        {
            
            mirraImage.SetActive(true);
            isSearchPoint = true;
            searchMode = collision.gameObject.GetComponent<SearchMode>();
        }
        if (collision.gameObject.tag == "Talk")
        {
            isTalk = true;
            talkManager = collision.gameObject.GetComponent<TalkManager>();
            gameManager.talkManager = talkManager;
        }
        if(collision.gameObject.tag == "Finish")
        {
            isFinish = true;
            talkManager = collision.gameObject.GetComponent<TalkManager>();
            gameManager.talkManager = talkManager;
            
        }
       
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Search")
        {
            mirraImage.SetActive(false);
            isSearchPoint = false;
        }
        if (collision.gameObject.tag == "Talk")
        {
            isTalk = false;
            
        }else if(collision.gameObject.tag == "Finish")
        {
            isFinish = false;
        }       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Search")
        {
            mirraImage.SetActive(false);
            isSearchPoint = false;
        }
        if (collision.gameObject.tag == "Talk")
        {
            isTalk = false;
            
        }
        if(collision.gameObject.tag == "Finish")
        {
            isFinish = false;
        }
    }


    public IEnumerator AppearTime(float time)
    {
        //渡された時間待つ
        yield return new WaitForSeconds(time);
        //暗闇に戻す
        
        searchMode.appearRenderer.material = diffuseMateriar;
        
        
        iswait = false;
    }
}
