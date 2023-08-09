using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 : 스칼 아이템이 아래로 이동한다.
public class SkillItmeMove : MonoBehaviour
{
    public int speed = 1;
    public GameObject vfx;
    public AudioClip clip;
    public GameObject soundEff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        float dist = Vector2.Distance(Camera.main.transform.position, transform.position);
        if (dist > Camera.main.orthographicSize * 2.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject fx = Instantiate(vfx);
            fx.transform.position = transform.position;

            GameObject soundManager = GameObject.Find("SoundManager");
            AudioSource audioSource = soundManager.GetComponent<SoundManager>().effAudioSource;
            audioSource.clip = clip;
            audioSource.Play();
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {

    }
}
