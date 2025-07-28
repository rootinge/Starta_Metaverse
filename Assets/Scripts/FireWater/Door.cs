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
        // ���� ���� �ִ� �÷��̾� �±��� ��
        currentPlayerNum = GameObject.FindGameObjectsWithTag("Player").Length;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("�÷��̾� ����");
        if(collision.CompareTag("Player"))
        {
            clearPlayerNum++;
            if (currentPlayerNum == clearPlayerNum)
            {
                // ��� �÷��̾ �������� ��
                Debug.Log("��� �÷��̾ �����߽��ϴ�!");
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

        // ��� �÷��̾� ���� Ŭ����� ������ ����
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<FireWaterController>().GameClear();
        }


        // 3�ʵ� �Լ� ȣ��
        Invoke("GameExit", 3f);
    }

    private void GameExit()
    {
        FWGameManager.Instance.Exit();
    }


}
