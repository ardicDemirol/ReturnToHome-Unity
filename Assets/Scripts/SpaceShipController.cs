using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShipController : MonoBehaviour
{
    public static SpaceShipController spaceShipController; 
    [SerializeField] float moveSpeed = 5f;
    private Vector3 rotation;
    [SerializeField] float rotate = 10f;


    [SerializeField] int maxHealth = 100;
    private float currentHealth;
    private float damage;



    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Image healthBar;

    private float duration;
    private float enemyCount;

    private FollowEnemy enemy;
    private LevelManager levelManager;


    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip gameplayAudio;

    private void Awake()
    {
        spaceShipController = this;
    }

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        enemy = GetComponent<FollowEnemy>();
        levelManager = GetComponent<LevelManager>();
        currentHealth = maxHealth;
        StartCoroutine(LoopAudio());

    }

    void Update()
    {
        duration = Time.deltaTime / 75;

        Turn();
        Die();
        Win();
        Bar();
    }


    void Turn()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotation = new Vector3(0, 0, -rotate);
            transform.Rotate(rotation * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotation = new Vector3(0, 0, rotate);
            transform.Rotate(rotation * Time.deltaTime * moveSpeed);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            damage = UnityEngine.Random.Range(1, 6);
            currentHealth -= damage; 
            healthBar.fillAmount -= damage/20;
        }
    }


    void Die()
    {
        if (currentHealth < 0 || healthBar.fillAmount <= 0.02f)
        {
            Destroy(gameObject);
            deathScreen.SetActive(true);
            audioSource.PlayOneShot(deathSound);
        }
    }

    void Win()
    {
        if (healthBar.fillAmount >= 0.97f)
        {
            audioSource.PlayOneShot(winSound);
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Sahne yüklenmedi");

        }

    }


    void Bar()
    {
        healthBar.fillAmount -= duration;

    }

    public void DoSmthng(float a)
    {
        healthBar.fillAmount += a;
    }

    IEnumerator LoopAudio()
    {
        float length = gameplayAudio.length;

        while (true)
        {
            audioSource.PlayOneShot(gameplayAudio);
            yield return new WaitForSeconds(length);
        }
    }
}
