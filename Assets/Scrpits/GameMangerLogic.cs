using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMangerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text PlayerCountText;

    void Awake() // 게임 중 왼쪽 상단 아이템 갯수 관련
    {
        stageCountText.text = "/ " + totalItemCount.ToString();
    }

    public void GetItem(int count) // 플레이어가 얻은 아이템 갯수 확인
    {
        PlayerCountText.text = count.ToString();
    }
    void OnTriggerEnter(Collider other) // 플레이어랑 부딪히면 재시작
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
