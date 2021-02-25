using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Play,
        Talk,
        Search,
        Pose,
        Clear
    }

    public PlayerController playerController;
    public static GameMode gameMode = GameMode.Play;
    public TalkManager talkManager;
    public KeyItemManager keyItemManager;
    public bool onTalk = false;
    void Start()
    {

    }

    void Update()
    {
        if (playerController.isSearchPoint && Keyboard.current.ctrlKey.isPressed)
        {
            gameMode = GameMode.Search;
        }
        if(gameMode == GameMode.Search)
        {

            if (Keyboard.current.spaceKey.isPressed)
            {
                gameMode = GameMode.Play;
            }
        }
        //トークフラグがONなら
        if (playerController.isTalk)
        {
            if (talkManager.talkFlg && Keyboard.current.ctrlKey.isPressed)
            {
                gameMode = GameMode.Talk;
                if (!onTalk)
                {
                    onTalk = true;
                    StartCoroutine(talkManager.TalkingCommon());
                }
            }
        }
        //
        if (gameMode == GameMode.Talk && !onTalk)
        {

            gameMode = GameMode.Play;
            if (Keyboard.current.spaceKey.isPressed)
            {

            }

        }

        if (playerController.isFinish)
        {
            if (talkManager.talkFlg && Keyboard.current.ctrlKey.isPressed)
            {
                if(keyItemManager != null)
                {
                    keyItemManager.ClearSave();
                }

                gameMode = GameMode.Clear;
                if (!onTalk)
                {
                    onTalk = true;
                    StartCoroutine(talkManager.TalkingCommon());
                }
            }
        }
    }
}
