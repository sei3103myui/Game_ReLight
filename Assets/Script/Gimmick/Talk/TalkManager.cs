using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TalkManager : MonoBehaviour
{
    public enum TalkingMode
    {
        Auto,
        Button,
        Skip
    }
    [Header("基本Autoで")]
    public TalkingMode talkingModes;
    [Header("Secne{0}_{1}/ファイル名で入力")]
    public string csvfilesName;
    [Header("会話終了で次のシーン移動があれば入力")]
    public string nextSceneName;

    [Header("自分のTalkLoadを指定すること")]
    public TalkLoad talkLoad;
    //public AudioClip prologueBgm;

    //[Header("オプション等UIの設定ここから")]
    //public GameObject skipMenu;
    //public GameObject firstSkip;
    [Header("記憶の欠片オブジェクト出現用")]
    public GameObject keyobj;
    private bool isAuto = false;
    [Header("会話フラグありならFalse 最初から再生するならTrue")]
    public bool talkFlg = false;
    //エピソード開始
    public IEnumerator TalkingCommon()
    {
        List<string[]> csvDatas = new List<string[]>();
        //設定したモードの数だけ会話文をつなげる
        
        csvDatas = talkLoad.CSVLoad(csvfilesName);//csvファイル読み込みしてもらう
        if (talkingModes == TalkingMode.Auto)
        {
            //自動で会話文を表示
            talkLoad.isTalk = true;
            StartCoroutine(talkLoad.Talking(csvDatas));
        }
        else if (talkingModes == TalkingMode.Button)
        {
            //ボタンが押されてからスタート
            while (!Keyboard.current.anyKey.isPressed) yield return null;
            talkLoad.isTalk = true;
            StartCoroutine(talkLoad.Talking(csvDatas));
        }
            
        //会話が終了するのを待つ
        while (talkLoad.isTalk) yield return null;

        //もし次のシーン名があれば
        if (nextSceneName != "")
        {
            AudioManager2D.Instance.AudioBgm.Stop();
            SceneManager.LoadScene(nextSceneName);
        }
        if(keyobj != null)
        {
            keyobj.SetActive(true);
        }
        
    }

    //public void OnClickSkip()
    //{
    //    skipMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(firstSkip);
    //}

    //public void OnClickOK()
    //{
    //    if (skipMenu.activeInHierarchy)
    //    {
    //        AudioManager2D.Instance.AudioBgm.Stop();
    //        SceneManager.LoadScene(nextSceneName);
    //    }
    //}
    //public void OnClickCancel()
    //{
    //    if (skipMenu.activeInHierarchy)
    //    {
    //        skipMenu.SetActive(false);
    //    }
    //}

    public void OnClickAuto()
    {
        talkLoad.talkingMode = TalkLoad.TalkingMode.Auto;
        isAuto = true;
    }
}
