using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearObjController : MonoBehaviour
{
    [Header("表示させたい会話を設定しているTalkTriggerをアタッチ")]
    public GameObject TalkTrigger;
    [HideInInspector]public TalkManager talkManager;
    void Start()
    {
        talkManager = TalkTrigger.GetComponent<TalkManager>();
    }

    void Update()
    {
        
    }

    public void TalkFlgOn()
    {
        if(talkManager != null)
        {
            talkManager.talkFlg = true;
        }
        else
        {
            Debug.Log("TalkManagerが取得できていません");
        }
        
    }
}
