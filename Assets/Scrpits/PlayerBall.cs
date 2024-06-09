using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    public int itemCount;
    public GameMangerLogic manager;

    bool isJump;

    Vector3 moveVec;

    Rigidbody rigid; //rigidbody �߰�
    AudioSource audio; //audio ��� �߰�
    Animator anim; //animator �߰�

    void Awake() // ������Ʈ ����
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
    }

   
    void Update() // update �Լ� ����
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }

    void GetInput() // Ű �Է�
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move() // �̵� �ӵ� �� �̵� ����
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }

    void Turn() // �̵� ������ ���� ĳ���� ���⵵ ȸ��
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump() // jump �ѹ��� �� �� �ְ�
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision) // �ٴڰ� �ٽ� ������ ���� ����
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") // ������ ȹ�� �� �Ҹ� ��� �� ������ ȹ�� ���� �߰�
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish") // finish line�� ���� �������� �̵�, gamemanager�� �浹 �� �������� �����, ������ ������ ���� �ʾƵ� �����
        {
            if(itemCount == manager.totalItemCount)
            {
                if (manager.stage == 2)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {
                //Restart..
                SceneManager.LoadScene(manager.stage);
            }
        }
    }
}
