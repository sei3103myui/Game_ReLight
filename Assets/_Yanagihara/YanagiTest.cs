using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YanagiTest : MonoBehaviour
{
    public GameObject obj;
    public float distance = 10f;
    public LayerMask layerMask;

    private PlayerInput playerInput;
    private InputAction lookAction;

    private Vector2 lookValue;
// 日本語試してみて
    void Start()
    {
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        lookAction = playerInput.currentActionMap.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {


        
        
    }


    
}
