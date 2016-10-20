using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System.Collections;

public class BeefyLeg : MonoBehaviour {

    public Rigidbody2D myBody;
    public Rigidbody2D myHips;
    public HingeJoint2D myKnee;
    public Slider powerMeter;
    public Slider healthBar;
    public Slider damageBar;


    public Transform checkpoint = null;
    public bool onElevator = false;
    public SpriteRenderer dealWithIt;
    bool secretStarting = false;
    bool secretsOn = false;

    public HingeJoint2D myLeftShoulder;
    public HingeJoint2D myRightShoulder;
    public bool crossArmPose = false;

    public AudioSource sound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip jumpAround;
    public AudioClip death;

    public float hitVolume = 1f;
    
    public Rigidbody2D myCalf;
    public HingeJoint2D myAnkle;

    public BlurOptimized blurEffect;

    public float maxHealth = 100f;
    public float health;
    public float healthInc = 1f;
    float lastHealth;
    Coroutine healthStep = null;

    public float damage;
    public float regenTime = 10f;
    public float hitHeadTime = 1f;

    public int onGround;
    public bool hitHead;

    public Vector2 jumpForce;
    public float jForceMultiplier;
    public float initJForceMultiplier;
    public Vector2 storedForce;
    public float maxStoredForce = 2000f;

    public float bodyTorque;
    public float initBodyTorqueMultiplier;
    public float backwardTorqueBoost;

    public float blurTime = 0f;
    public float maxBlurTime;
    public float blurTimeInc;

    public float footTorque;
 
    public float torqueLeft;
    public float torqueRight;

    public Vector3 leftScale;
    public Vector3 rightScale;

    public SpriteRenderer headInjury;

    SpringJoint2D spring;

    // Use this for initialization
    void Start()
    {
        LevelEndManager.currentLevel = LevelEndManager.nextLevel = SceneManager.GetActiveScene().buildIndex;

        blurEffect = Camera.main.GetComponent<BlurOptimized>();
        health = maxHealth;
        lastHealth = health;

        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        damageBar.minValue = 0;
        damageBar.maxValue = maxHealth;

        sound = GetComponentInChildren<AudioSource>();

        torqueLeft = bodyTorque;
        torqueRight = -bodyTorque;

        leftScale = transform.localScale;
        rightScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        jumpForce = Vector2.up * jForceMultiplier * initJForceMultiplier;
        storedForce = jumpForce;

        powerMeter.minValue = storedForce.y;
        powerMeter.maxValue = maxStoredForce;
    }

    // Update is called once per frame
    void Update()
    {
        damage = Mathf.Abs(myBody.velocity.x) + Mathf.Abs(myBody.velocity.y); //+ Mathf.Abs(myBody.angularVelocity);

        powerMeter.value = storedForce.y;
        healthBar.value = health;
        damageBar.value = health - damage;
        if (health <= 0)
        {
            if (lastHealth == maxHealth)
                health = 1;
            else
                Die();
        }
        else if (health > maxHealth)
            health = maxHealth;

        if (blurEffect.enabled)
        {
            if (blurTime >= maxBlurTime)
            {
                blurEffect.enabled = false;
                blurEffect.blurSize = 0f;
                blurTime = 0f;
            }
            else
            {
                blurEffect.blurSize = maxBlurTime - blurTime;
                blurTime += blurTimeInc * Time.deltaTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myBody.AddTorque(bodyTorque * initBodyTorqueMultiplier);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myBody.AddTorque(bodyTorque);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                myRightShoulder.useMotor = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            myBody.AddTorque(-bodyTorque + backwardTorqueBoost * initBodyTorqueMultiplier);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myBody.AddTorque(-bodyTorque + -backwardTorqueBoost);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                myLeftShoulder.useMotor = true;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow) && crossArmPose == false)
        {
            crossArmPose = true;
            JointMotor2D tempMotor = myLeftShoulder.motor;
            myLeftShoulder.motor = myRightShoulder.motor;
            myRightShoulder.motor = tempMotor;

            myLeftShoulder.useMotor = true;
            myRightShoulder.useMotor = true;
            if (secretStarting == false && secretsOn == false)
                Invoke("StartSecret", 5f);

        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && crossArmPose == true)
        {

            JointMotor2D tempMotor = myLeftShoulder.motor;
            myLeftShoulder.motor = myRightShoulder.motor;
            myRightShoulder.motor = tempMotor;

            myLeftShoulder.useMotor = false;
            myRightShoulder.useMotor = false;
            crossArmPose = false;
            if (secretStarting == true && secretsOn == false)
            {
                CancelInvoke("TheSecretsOn");
                sound.Stop();
                FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().Play();
                secretStarting = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            myLeftShoulder.useMotor = false;
            myRightShoulder.useMotor = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            myBody.angularVelocity = 0;// AddTorque(bodyTorque);
            myRightShoulder.useMotor = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            myBody.angularVelocity = 0;// AddTorque(-bodyTorque);
            myLeftShoulder.useMotor = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (storedForce.y <= maxStoredForce)
                storedForce += Vector2.up * jForceMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (onGround > 0)
            {
                myBody.AddRelativeForce(storedForce, ForceMode2D.Force);
                myCalf.AddTorque(footTorque);
                myHips.AddTorque(-footTorque);
                storedForce = jumpForce = Vector2.up * jForceMultiplier * initJForceMultiplier;
                sound.PlayOneShot(jumpSound, 1f);
            }
            else
            {
                storedForce = jumpForce = Vector2.up * jForceMultiplier * initJForceMultiplier;
            }
        }
    }

    void StartSecret()
    {
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().Stop();
        sound.PlayOneShot(jumpAround);
        secretStarting = true;
        Invoke("TheSecretsOn", 7);
    }
    void TheSecretsOn()
    {
        secretsOn = true;
        dealWithIt.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Deadly"))
        {
            Die();
        }
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("CheckPoint");
            checkpoint = col.gameObject.transform;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hitHead == true && onElevator == false)
        {
            if (damage > 9f)
            {
                if (!blurEffect.enabled)
                {
                    blurEffect.enabled = true;
                }
                Debug.Log("velocity: " + damage);
                sound.PlayOneShot(hitSound, hitVolume);
                headInjury.enabled = true;
                Invoke("FlashRed", 0.1f);
                lastHealth = health;
                health -= damage;
                if (health < 0)
                    health = 0;
                Debug.Log("Health:" + health);
                hitHead = false;
                if (IsInvoking("regenHealth"))
                    CancelInvoke("regenHealth");

                if (healthStep != null)
                {
                    StopCoroutine(healthStep);
                    healthStep = null;
                }

                Invoke("regenHealth", regenTime);
                Debug.Log("HitHead");
                Invoke("HitHead", hitHeadTime);
            }
        }

        if (col.gameObject.CompareTag("Deadly"))
        {
            Die();
        }
        
        if(onElevator == true)
        {
            if(col.gameObject.CompareTag("Elevator") == false)
            {
                Die();
            }
        }
    }

    void HitHead()
    {
        Debug.Log("HitHead");
        hitHead = true;
    }

    void regenHealth()
    {
        healthStep = StartCoroutine(HealthStep());
    }

    IEnumerator HealthStep()
    {
        Debug.Log("corutine started");
        while (health != maxHealth)
        {
            Debug.Log("corutine running");
            lastHealth = health;
            health = Mathf.MoveTowards(health, maxHealth, healthInc);
            yield return null;
        }
    }

    public void Die()
    {
        if(checkpoint != null)
        {
            sound.PlayOneShot(death);
            transform.position = checkpoint.position;
            health = maxHealth;
        }
        else
        {
            SceneManager.LoadScene("LevelEnd");
        }
    }

    void FlashRed()
    {
        headInjury.enabled = false;
    }
}
