using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player_Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private new BoxCollider2D collider;
    private CircleCollider2D circle_collider;

    [Header("Настройки игрока")]
    [SerializeField] private float currentSpeed; // Текущая скорость
    [SerializeField] private float MaxMovementSpeed = 4; // Максимальная Скорость
    [SerializeField] private float jump = 13.5f; // Высота прыжка
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f; // Плавность движения
    [Range(0, 1)] [SerializeField] private float groundCheckRadius = 0f;

    private const string MaxLevel = "Lvl15"; // Максимальный уровень. Если всего уровней в игре 999, то указывать надо Lvl999, название чувствительно к регистру
    
    private bool control = true; // Включить или выключить управление персонажем
    private bool dead = false; // Помер или не помер
    private bool moveState = true;
    private bool canJump;
    private bool isGrounded;
    private bool LevelComplete = false;
    private Vector3 Velocity = Vector3.zero;

    public int money = 0;
    public int highscore = 0;

    [Header("Остальные объекты")]
    [SerializeField] private Joystick joystick = null;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask platformLayerMask = 0;
    [SerializeField] private GameObject JoystickControl = null;
    [SerializeField] private GameObject ButtonsControl = null;
    //[SerializeField] private GameObject DeleteObj_Trigger = null;
    [SerializeField] private ParticleSystem dust = null;

    [Header("Все что связано с очками")]
    [SerializeField] private Text GameOverScore = null;
    [SerializeField] private Text LevelCompleteScore = null;
    [SerializeField] private Text highscoreValue = null;
    [SerializeField] private Text ScoreLeftText = null;
    [SerializeField] private int tempScore = 0;
    [SerializeField] private int ScoreLeft;

    [Header("Все UI")]
    [SerializeField] private GameObject gameOverUI = null;
    [SerializeField] private GameObject LevelCompleteUI = null;

    private void Awake()
    {
        GetComponents();

        if (PlayerPrefs.GetInt("ActiveControl") == 0)
        {
            JoystickControl.SetActive(true);
        }
        else
        {
            ButtonsControl.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        SaveManager.SavePlayer(this);
    }

    private void Start()
    {
        LevelComplete = false;

        LoadPlayerData();
        SelectTypeOfLevel(LevelTypeHandler.TypeOfLevel);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayerMask);

        if (dead == true || LevelComplete == true)
        {
            rb.velocity = new Vector3(0, rb.velocity.y); // Обнуляем полностью скорость
        }

        Move(control);
    }

    private void Update()
    {
        CheckInput();
        CheckIfCanJump();

        if (Timer.isComplete && !LevelComplete) {
            CompleteLevel();
        }
    }

    #region Methods
    private void Move(bool canMove)
    {
        if (canMove)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(currentSpeed, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            if (!moveState && currentSpeed > 0)
            {
                Flip();
            }
            else if (moveState && currentSpeed < 0)
            {
                Flip();
            }
        } else {
            rb.velocity = new Vector3(0, rb.velocity.y); // Обнуляем полностью скорость
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void CheckInput()
    {
        if (control && PlayerPrefs.GetInt("ActiveControl") == 0)
        {
            if (joystick.Horizontal > .2f)
            {
                anim.SetBool("Run", true);
                currentSpeed = MaxMovementSpeed;
                CreateDust();
            }
            else if (joystick.Horizontal < -.2f)
            {
                anim.SetBool("Run", true);
                currentSpeed = -MaxMovementSpeed;
                CreateDust();
            }
            else
            {
                anim.SetBool("Run", false);
                currentSpeed = 0f;
            }
        }

        if (EventTrigger.multiplier == 1)
        {
            SetMovementSpeedMultiplier(1);
        }
        else if (EventTrigger.multiplier == -1)
        {
            SetMovementSpeedMultiplier(-1);
        }
        else
        {
            SetMovementSpeedMultiplier(0);
        }
    }

    private void LoadPlayerData()
    {
        PlayerData data = SaveManager.LoadPlayer();
        money = data.money;
        highscore = data.highscore;
        highscoreValue.text = highscore.ToString();
    }

    private void GetComponents()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = transform.GetComponent<BoxCollider2D>();
        circle_collider = GetComponentInChildren<CircleCollider2D>();
    }

    private void Flip()
    {
        moveState = !moveState;

        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Jump()
    {
        if (canJump && control)
        {
            rb.velocity = Vector2.up * jump;
            CreateDust();
        }
    }

    public void SetMovementSpeedMultiplier(int multiplier)
    {
        if (control && PlayerPrefs.GetInt("ActiveControl") == 1)
        {
            currentSpeed = MaxMovementSpeed * multiplier;
            if (currentSpeed != 0)
            {
                anim.SetBool("Run", true);
                CreateDust();
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    private void GameOver()
    {
        if (!dead && !LevelComplete)
        {
            dead = true;
            //DeleteObj_Trigger.SetActive(true);
            ObjectsSpawner.gameOver = true;
            gameOverUI.SetActive(true);
            control = false;
            Timer.stopTimer = true;
            anim.SetBool("Run", false);

            if (highscore < tempScore)
            {
                highscore = tempScore;
                highscoreValue.text = highscore.ToString();
            }
            GameOverScore.text = tempScore.ToString();

            currentSpeed = 0f;
            jump = 0f;
            money += tempScore*2;

            SaveManager.SavePlayer(this);
        }
    }

    private void CompleteLevel() {
        if (!dead && !LevelComplete)
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            buildIndex++;
            
            LevelComplete = true;
            LevelCompleteUI.SetActive(true);
            //DeleteObj_Trigger.SetActive(true);
            ObjectsSpawner.gameOver = true;
            control = false;
            anim.SetBool("Run", false);
            currentSpeed = 0f;
            jump = 0f;

            LevelCompleteScore.text = 100.ToString();
            money += 100;

            if (!PlayerPrefs.HasKey("Lvl" + buildIndex) && SceneManager.GetActiveScene().name != MaxLevel)
            {
                PlayerPrefs.SetString("Lvl" + buildIndex, "unlocked");
                Debug.Log("PlayerPrefs | Key created: Lvl" + buildIndex);
            }

            SaveManager.SavePlayer(this);
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }

    private void SelectTypeOfLevel(int TypeOfLevel) {
        if (TypeOfLevel == 1)
            ScoreLeftText.text = ScoreLeft.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Score")) // Если игрок столкнулся с фруктом
        {
            if (!dead)
            {
                if (LevelTypeHandler.TypeOfLevel == 1 && !LevelComplete) {
                    ScoreLeft--;
                    ScoreLeftText.text = ScoreLeft.ToString();
                    if (ScoreLeft == 0 || ScoreLeft < 0)
                        CompleteLevel();
                } else {
                    Debug.Log("+1 Score!");

                    tempScore++;
                }
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Die")) // Если игрок столкнулся с ящиком
        {
            Destroy(other.gameObject);
            if(!LevelComplete)
                GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Die"))
        {
            GameOver();
        }

        if (other.gameObject.CompareTag("End"))
        {
            CompleteLevel();
        }
    }
    #endregion
}