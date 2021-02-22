using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour
{
    public enum GameMode
    {
        Play,
        Pose
    }
    public GameMode gameMode;
    private const float MAX_SPEED = 2.0f;//加速度上限

    public Camera mainCamera;//メインカメラ

    public Rigidbody rb;
    public float moveForceMultiplier;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 moveValue = Vector3.zero;
    public Animator playerAnimator;

    private PlayerInput playerInput;
    private InputAction moveAction;
    
    [SerializeField]
    private float walkSpeed = 8f;
    [SerializeField]
    private float jumpPower = 5f;

    public LayerMask layerMask;
    private bool isGround = false;
    [SerializeField]
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");

    }

    // Update is called once per frame
    void Update()
    {
        //接地判定処理（ジャンプ可能か）
        isGround = Physics.Linecast(
            transform.position + transform.forward * 0.4f + transform.up * 0.05f,
            transform.position + -transform.up * 0.2f,
            layerMask
            ) ||
            Physics.Linecast(
                transform.position + -transform.forward * 0.4f + transform.up * 0.07f,
                transform.position + -transform.up * 0.2f,
                layerMask);

        //Debug.DrawLine(
        //    transform.position  + transform.forward * 0.4f + transform.up * 0.07f,
        //    transform.position ,
        //    Color.red
        //    );

        //Debug.DrawLine(
        //    transform.position  + -transform.forward * 0.4f + transform.up * 0.07f,
        //    transform.position ,
        //    Color.red
        //    );
        


        //ActionMapのMove取得
        moveValue = moveAction.ReadValue<Vector2>();
       
        //入力値を設定
        moveDirection.x = moveValue.x;
        moveDirection.z = moveValue.y;

        //moveDirection = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * moveDirection;
        ////キャラクターの向きを合わせる
        //if (0.01 < moveDirection.sqrMagnitude)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveDirection);
        //}
        
    }

    private void FixedUpdate()
    {
        if(gameMode == GameMode.Play)
        {
            Vector3 moveVector = Vector3.zero;

            //カメラの向きに移動するかどうか

            Vector3 Forward = transform.forward;
            Vector3 Right = transform.right;
            Forward.y = 0.0f;
            Right.y = 0.0f;


            moveVector = walkSpeed * (Right.normalized * moveDirection.x + Forward.normalized * moveDirection.z);

            rb.AddForce(walkSpeed * (moveVector - rb.velocity));



            //移動処理（Velocity）
            //rb.velocity += moveDirection * Time.fixedDeltaTime * walkSpeed;
            //rb.velocity = VelocityLimit(rb.velocity);
        }

    }
    /// <summary>
    /// 加速度上限の設定
    /// </summary>
    /// <param name="velocity"></param>
    /// <returns></returns>
    private Vector3 VelocityLimit(Vector3 velocity)
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

    public void OnMove(InputAction.CallbackContext context)
    {
        if(gameMode == GameMode.Play)
        {
            playerAnimator.SetBool("isWalk", true);
            if (context.canceled)
            {
                playerAnimator.SetBool("isWalk", false);
            }
        }
        
        //var value = context.ReadValue<Vector2>();
        //var x = (value.x * speed);
        //var z = (value.y * speed);

        //rb.AddForce(x, 0f, z);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
        //地面に接地していれば
        if (gameMode == GameMode.Play && isGround)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        
    }   
}
