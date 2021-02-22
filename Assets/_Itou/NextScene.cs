using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class NextScene : MonoBehaviour
{
    public enum StageMode
    {
        Main,
        SubStage
    }
    [Header("Mainは触れたらシーン遷移")]
    public StageMode stageMode;
    //次の読み込むシーンの名前
    public string nextScene;

    //移動のみ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (stageMode == StageMode.Main)
        {
            SceneManager.LoadScene(nextScene);
        }
        else if (stageMode == StageMode.SubStage)
        {
            if (Keyboard.current.ctrlKey.isPressed)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

}
