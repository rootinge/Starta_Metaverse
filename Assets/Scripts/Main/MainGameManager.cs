using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;
    public static MainGameManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public void PlayerTransformStorage()
    {
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;

        // �÷��̾��� ��ġ�� ����
        DataManager.Instance.oldPlayerPos = playerpos;
    }

}
