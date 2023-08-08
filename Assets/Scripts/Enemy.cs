
using UnityEngine;

//목표 1 아래로 이동
// 목표 2 다른 충돌체와 충돌시 나, 상대를 파괴.
//목표 3 시작시 30프로 확률로 플레이어를 추적/필요 속성 : 30퍼의 확률

//총알과 충돌시 hp감소
//필요 속성 : hp
public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 dir = Vector3.down;
    public GameObject player;
    bool isTracking = false;
    public GameObject explosion;
    public PlayerMove playerMove;
    public int hp = 2;
    Vector3 originalPosition;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        originalPosition = player.transform.position;
        //30퍼 확률 만들기
        int randomValue = Random.Range(0, 10); //0~9 임의값

        if(randomValue < 7)
        {
            dir = (player.transform.position - transform.position).normalized;
        }
        if(randomValue < 5) 
        {
            isTracking = true;
        }
    }
    void Update()
    {
        if(isTracking) 
        {
            dir = (player.transform.position - transform.position).normalized;
        }
         
        transform.position += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);

        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if(dist > Camera.main.orthographicSize * 2.2f)
        {
            Destroy(gameObject);
        }
    }
    //충돌 순간
    //부딪하면 상호 파괴
    //충돌시 폭발효과
    //폭발효과 게임 오브젝트 필요
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                print("충돌하여죽음");
                playerMove.hp--;
                Destroy(gameObject);
            }
            hp--;
        }
        if(hp < 0)
        {
            GameObject explosion0 = Instantiate(explosion);
            explosion0.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
    //충돌 중
    private void OnCollisionStay(Collision other)
    {
        
    }
    //충돌 종료
    private void OnCollisionExit(Collision other)
    {
        
    }
}
