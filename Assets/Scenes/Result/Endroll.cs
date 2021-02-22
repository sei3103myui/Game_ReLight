using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Endroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField]
    private float textScrollSpeed = 30;

    //テキストの制限位置
    [SerializeField]
    private float limitPosition = 730f;

    //エンドロールが終了したかどうか
    private bool isStopEndRoll;

    //　シーン移動用コルーチン
    private Coroutine endRollCoroutine;

    // Update is called once per frame
    void Update()
    {
        //　エンドロール終了
        if (isStopEndRoll)
        {
            //endRollCoroutine = StartCoroutine(GoToNextScene());
            RetruenScene();
           
        }
        else
        {
            //　エンドロール用テキストがリミットを越えるまで動かす
            if (transform.position.y <= limitPosition)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + textScrollSpeed * Time.deltaTime);
               
            }
            else
            {
                isStopEndRoll = true;//スクロールの終わりフラグ             
            }
        }
    }

    //IEnumerator GoToNextScene()
    //{
    //    //　3秒間待つ
    //    yield return new WaitForSeconds(3f);

    //    if (Keyboard.current.qKey.wasPressedThisFrame)
    //    {
    //        StopCoroutine(endRollCoroutine);
    //        SceneManager.LoadScene("Title");
    //    }

    //    yield return null;
    //}

    void RetruenScene()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            SceneManager.LoadScene("Title");
        }
    }
}