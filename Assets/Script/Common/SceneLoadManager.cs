using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class SceneLoadManager : MonoBehaviour
{
    [Header("移動するシーンの名前")]
    public string NextSceneName;

    private bool isNextFlg = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(isNextFlg && Keyboard.current.ctrlKey.isPressed)
        {
            NextSceneLoad();
        }
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(NextSceneName);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isNextFlg = true;
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isNextFlg = false;
        }
    }
}
