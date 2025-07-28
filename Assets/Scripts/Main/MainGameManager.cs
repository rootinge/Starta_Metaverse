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

        // 플레이어의 위치를 저장
        DataManager.Instance.oldPlayerPos = playerpos;
    }

}
