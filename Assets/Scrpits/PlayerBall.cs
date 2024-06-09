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

    Rigidbody rigid; //rigidbody 추가
    AudioSource audio; //audio 출력 추가
    Animator anim; //animator 추가

    void Awake() // 컴포넌트 연결
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
    }

   
    void Update() // update 함수 내용
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }

    void GetInput() // 키 입력
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move() // 이동 속도 및 이동 관련
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }

    void Turn() // 이동 방향이 돌면 캐릭터 방향도 회전
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump() // jump 한번만 할 수 있게
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision) // 바닥과 다시 만나면 점프 가능
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") // 아이템 획득 시 소리 출력 및 아이템 획득 갯수 추가
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish") // finish line에 들어가면 스테이지 이동, gamemanager와 충돌 시 스테이지 재시작, 아이템 갯수가 맞지 않아도 재시작
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
