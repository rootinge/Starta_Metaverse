using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int currentPlayerNum;
    private int clearPlayerNum;

    private Animator anim;
    void Start()
    {
        // 현재 씬에 있는 플레이어 태그의 수
        currentPlayerNum = GameObject.FindGameObjectsWithTag("Player").Length;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("플레이어 들어옴");
        if(collision.CompareTag("Player"))
        {
            clearPlayerNum++;
            if (currentPlayerNum == clearPlayerNum)
            {
                // 모든 플레이어가 도착했을 때
                Debug.Log("모든 플레이어가 도착했습니다!");
                Clear();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            clearPlayerNum--;
    }

    private void Clear()
    {
        anim.SetTrigger("IsOpen");
        FWGameManager.Instance.ScoreStorage();
        FWGameManager.Instance.isGameClear = true;

        // 모든 플레이어 게임 클리어시 움직임 제거
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<FireWaterController>().GameClear();
        }


        // 3초뒤 함수 호출
        Invoke("GameExit", 3f);
    }

    private void GameExit()
    {
        FWGameManager.Instance.Exit();
    }


}
