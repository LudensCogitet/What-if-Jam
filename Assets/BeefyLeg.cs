using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BeefyLeg : MonoBehaviour {

    public Rigidbody2D myBody;
    public Rigidbody2D myHips;
    public HingeJoint2D myKnee;

    public HingeJoint2D myLeftShoulder;
    public HingeJoint2D myRightShoulder;

    public AudioSource sound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public float hitVolume = 1f;

    public Rigidbody2D myCalf;
    public HingeJoint2D myAnkle;

    public float health = 1000f;

    public int onGround;
    public bool hitHead = false;

    public Vector2 jumpForce;
    public float jForceMultiplier;
    public float initJForceMultiplier;
    public Vector2 storedForce;
    public float maxStoredForce = 2000f;

    public float bodyTorque;
    public float initBodyTorqueMultiplier;

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
        sound = GetComponentInChildren<AudioSource>();
        myBody = GetComponent<Rigidbody2D>();

        torqueLeft = bodyTorque;
        torqueRight = -bodyTorque;

        leftScale = transform.localScale;
        rightScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        jumpForce = Vector2.up * jForceMultiplier * initJForceMultiplier;
        storedForce = jumpForce;    
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
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
            myBody.AddTorque(-bodyTorque * initBodyTorqueMultiplier);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myBody.AddTorque(-bodyTorque);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                myLeftShoulder.useMotor = true;
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
        if(Input.GetKeyUp(KeyCode.Space) && onGround > 0)
        {
            myBody.AddRelativeForce(storedForce, ForceMode2D.Force);
            myCalf.AddTorque(footTorque);
            myHips.AddTorque(-footTorque);
            storedForce = jumpForce = Vector2.up * jForceMultiplier * initJForceMultiplier;
            sound.PlayOneShot(jumpSound,1f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hitHead == true)
        {
            float damage = Mathf.Abs(myBody.angularVelocity + myBody.velocity.x);

            sound.PlayOneShot(hitSound,hitVolume);
            headInjury.enabled = true;
            Invoke("FlashRed", 0.1f);
            health -= damage;
            Debug.Log(Mathf.Abs(myBody.angularVelocity + myBody.velocity.x));
            hitHead = false;

            if(health <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void FlashRed()
    {
        headInjury.enabled = false;
    }
}
