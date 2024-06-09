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

    void Awake() // ���� �� ���� ��� ������ ���� ����
    {
        stageCountText.text = "/ " + totalItemCount.ToString();
    }

    public void GetItem(int count) // �÷��̾ ���� ������ ���� Ȯ��
    {
        PlayerCountText.text = count.ToString();
    }
    void OnTriggerEnter(Collider other) // �÷��̾�� �ε����� �����
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
