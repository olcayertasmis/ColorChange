using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Other Compenents")]
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject colorChangerPrefab;
    private Rigidbody2D _rigidbody;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject settingsPanel;


    [Header("Degiskenler")]
    [SerializeField] private float jumpPower = 3f;
    [SerializeField] private string activeColorName;
    [SerializeField] private Color ballColor;
    [SerializeField] private Color turkuaz, sari, mor, pembe;

    private float _score;
    private bool _isTouch, _isStart;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        scoreText.text = "Score: " + _score;
        bestScoreText.text = "BestScore : " + PlayerPrefs.GetFloat("BestScore", 0);
        SetRandomColor();
    }

    private void Update()
    {
        if (!_isStart || settingsPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            if (transform.position.y < -5)
                RestartGame();
            if (Input.GetMouseButtonDown(0))
                _isTouch = true;
            else if (Input.GetMouseButtonUp(0))
                _isTouch = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
            _isStart = true;
    }

    private void FixedUpdate()
    {
        if (_isTouch)
            _rigidbody.velocity = Vector2.up * jumpPower;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RenkTekeri"))
        {
            SetRandomColor();
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            return;
        }

        if (collision.CompareTag("PuanArttirici"))
        {
            _score += 5;
            scoreText.text = "Score: " + _score;
            if (PlayerPrefs.GetFloat("BestScore") < _score)
                PlayerPrefs.SetFloat("BestScore", _score);
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            Instantiate(ringPrefab, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z), Quaternion.identity);
            Instantiate(colorChangerPrefab, new Vector3(transform.position.x, transform.position.y + 11f, transform.position.z), Quaternion.identity);
        }

        if (collision.tag != activeColorName && !collision.CompareTag("PuanArttirici"))
        {
            _score = 0;
            RestartGame();
        }
    }

    private void SetRandomColor()
    {
        int rastgeleSayi = Random.Range(0, 4);
        switch (rastgeleSayi)
        {
            case 0:
                activeColorName = "Turkuaz";
                ballColor = turkuaz;
                break;
            case 1:
                activeColorName = "Sari";
                ballColor = sari;
                break;
            case 2:
                activeColorName = "Pembe";
                ballColor = pembe;
                break;
            case 3:
                activeColorName = "Mor";
                ballColor = mor;
                break;
        }

        GetComponent<SpriteRenderer>().color = ballColor;
    }

    private void RestartGame()
    {
        bestScoreText.text = "BestScore : " + PlayerPrefs.GetFloat("BestScore");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
} //Class