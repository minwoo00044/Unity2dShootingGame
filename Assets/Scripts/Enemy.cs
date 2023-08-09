
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
    public bool isMoveable = true;
    public GameObject explosion;
    private PlayerMove playerMove;
    public int hp = 2;
    Vector3 originalPosition;
    EnemyFire fire;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        originalPosition = player.transform.position;
        fire = GetComponent<EnemyFire>();
        //30퍼 확률 만들기
        int randomValue = Random.Range(0, 10); //0~9 임의값

        if (randomValue < 7)
        {
            dir = (player.transform.position - transform.position).normalized;
        }
        if (randomValue < 5)
        {
            isTracking = true;
        }
    }
    void Update()
    {
        if (isMoveable)
        {
            EnemyMove();
        }
        
    }
    void EnemyMove()
    {
        if (player != null && fire != null)
        {
            if (isTracking)
            {
                if (player != null)
                {
                    dir = (player.transform.position - transform.position).normalized;
                }

            }
            transform.position += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);

            float dist = Vector2.Distance(Camera.main.transform.position, transform.position);

            if (dist > Camera.main.orthographicSize * 1.3f)
            {
                fire.isShootable = false;
                if (dist > Camera.main.orthographicSize * 2.5f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                fire.isShootable = true;
            }
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

            GameObject explosion0 = Instantiate(explosion);
            explosion0.transform.position = transform.position;
            if (other.gameObject.CompareTag("Player"))
            {
                playerMove.hp--;
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Bullet"))
            {
                GameManager.Instance.attackScore += 10;
                print(GameManager.Instance.attackScore + "attack");
            }
            hp--;
        }
        if (hp < 0)
        {
            GameObject explosion0 = Instantiate(explosion);
            explosion0.transform.position = transform.position;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.destroyScore += 100;
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {

        
    }
}
