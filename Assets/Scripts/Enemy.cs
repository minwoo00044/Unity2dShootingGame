
using UnityEngine;

//��ǥ 1 �Ʒ��� �̵�
// ��ǥ 2 �ٸ� �浹ü�� �浹�� ��, ��븦 �ı�.
//��ǥ 3 ���۽� 30���� Ȯ���� �÷��̾ ����/�ʿ� �Ӽ� : 30���� Ȯ��

//�Ѿ˰� �浹�� hp����
//�ʿ� �Ӽ� : hp
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
        //30�� Ȯ�� �����
        int randomValue = Random.Range(0, 10); //0~9 ���ǰ�

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
            Destroy(gameObject);
        }
    }
    //�浹 ����
    //�ε��ϸ� ��ȣ �ı�
    //�浹�� ����ȿ��
    //����ȿ�� ���� ������Ʈ �ʿ�
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {


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
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.destroyScore += 100;
        print(GameManager.Instance.attackScore);
    }
}
