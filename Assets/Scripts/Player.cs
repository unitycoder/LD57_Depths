using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform leftHandPivot;
    public Transform rightHandPivot;
    public Transform spawnForceArea;

    public GameObject gameOverMenu;
    public GameObject gameFinishedMenu;
    //float minAngleLeft = -15f;
    //float maxAngleLeft = -150f;

    //float minAngleRight = 15f;
    //float maxAngleRight = 150f;

    int angleStepLeft = 0;
    int angleStepRight = 0;

    int angleSteps = 3;

    Quaternion origLeftRot;
    Quaternion origRightRot;

    Rigidbody2D rb;
    bool isInWater = false;

    float leftForce = 0;
    float rightForce = 0;

    float pushForce = 1.5f;

    bool gotTreasure = false;
    private bool isFinished;
    bool isDed = false;

    bool isStarted = false;

    AudioSource aus;

    float oxygen = 100f;

    void Start()
    {
        hpSlider.value = oxygen;
        aus = GetComponent<AudioSource>();

        gameOverMenu.SetActive(true);
        gameFinishedMenu.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        origLeftRot = leftHandPivot.localRotation;
        origRightRot = rightHandPivot.localRotation;

        //angleStepRight = (maxAngleRight - minAngleRight) / angleSteps;
        //angleStepLeft = (maxAngleLeft - minAngleLeft) / angleSteps;
        //Debug.Log("angleStep " +angleStepRight);

        //Debug.Log("leftHandPivot.localRotation.eulerAngles.z: " + leftHandPivot.localRotation.eulerAngles.z + " maxAngleLeft:" + maxAngleLeft);
    }


    void Update()
    {
        if (isStarted == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameOverMenu.SetActive(false);
                rb.AddForceAtPosition(-Vector2.one, ((Vector2)spawnForceArea.position + Random.insideUnitCircle * 0.26461f), ForceMode2D.Impulse);
                isStarted = true;
            }
            return;
        }


        if (isInWater == false) return;
        if (isFinished)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            return;
        }

        if (isDed)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftHandPivot.Rotate(0, 0, 40);
            angleStepLeft += 1;

            aus.pitch = Random.Range(0.9f, 1.1f);
            aus.Play();

            if (angleStepLeft > angleSteps)
            {
                angleStepLeft = 0;
                leftHandPivot.localRotation = origLeftRot;
                leftForce = 0;
                oxygen -= 0.5f;
            }
            else
            {
                leftForce = 1;
            }
        }

        leftForce -= Time.deltaTime;
        if (leftForce < 0) leftForce = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            rightHandPivot.Rotate(0, 0, -40);
            angleStepRight += 1;

            aus.pitch = Random.Range(0.9f, 1.1f);
            aus.Play();

            if (angleStepRight > angleSteps)
            {
                angleStepRight = 0;
                rightHandPivot.localRotation = origRightRot;
                rightForce = 0;
                oxygen -= 0.5f;
            }
            else
            {
                rightForce = 1;
            }
        }

        rightForce -= Time.deltaTime;
        if (rightForce < 0) rightForce = 0;


        oxygen -= Time.deltaTime * 0.35f;
        hpSlider.value = oxygen;
        if (oxygen <= 0)
        {
            isDed = true;
            gameOverMenu.SetActive(true);
            Debug.Log("You are dead!");
        }
    }

    public Slider hpSlider;

    private void FixedUpdate()
    {
        if (isInWater == false) return;

        rb.AddForceAtPosition(transform.up * leftForce * pushForce, leftHandPivot.position, ForceMode2D.Force);
        rb.AddForceAtPosition(transform.up * rightForce * pushForce, rightHandPivot.position, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInWater = true;
            collision.GetComponent<AudioSource>().Play();
            rb.linearDamping = 2.9f;
        }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("dead");

            collision.GetComponent<AudioSource>().Play();

            gameOverMenu.SetActive(true);
        }


        if (collision.CompareTag("Bubble"))
        {
            oxygen += 50f;
            collision.GetComponent<Renderer>().enabled = false;
            collision.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject, 1);
        }

        if (collision.CompareTag("Treasure"))
        {
            if (gotTreasure) return; // already got treasure

            collision.GetComponent<AudioSource>().Play();

            gotTreasure = true;
            // attach joint to treasure
            var targetRb = collision.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                var joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = targetRb;
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector2.zero;
                joint.connectedAnchor = Vector2.zero;
            }

            pushForce = 3f;
        }

        if (collision.CompareTag("Air"))
        {
            if (isInWater) oxygen = 100;
        }


        if (collision.CompareTag("Stairs"))
        {
            if (gotTreasure == false) return; // not got treasure

            if (isFinished) return; // already finished

            isFinished = true;
            Debug.Log("You win!");
            gameFinishedMenu.SetActive(true);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
