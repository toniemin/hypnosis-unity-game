using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    private float walkSpeed = 10f; // Player's normal movement speed.
    private float runSpeed = 17f; // Player's running speed.

    public GameObject visionCCTV; //GameObject of CCTV's vision
    public GameObject gameOverPanel; //Panel for displaying the "You have been spotted!" text

    // UI indicator for player's run energy. UI for SprintEnergy variable.
    public Slider SprintEnergySlider;

    // Running mechanic variables.
    bool sprintDepleting = false;
    float sprintDepleteRate = .8f;
    bool sprintDisabled = false;
    float sprintDisabledTime = 2f;
    float sprintRefillRate = .05f;
    public float SprintEnergy { get; private set; } = 1;

    Subject playerSubject = new Subject();
    Subject soundSubject = new Subject();

    void Awake()
    {
        gameOverPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();

        playerSubject.AddObserver(new PlayerNotifier());
        soundSubject.AddObserver(new SoundNotifier());
    }

    private void Start()
    {
        // Start coroutines that control the player's sprint energy meter.
        StartCoroutine(UpdateSprintEnergySlider());
        StartCoroutine(DepleteSprint());
        StartCoroutine(RefillSprint());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit(); //quits the game if the player presses ESC
        }
    }

    void FixedUpdate()
    {
        // Set default movement speed. Change if needed.
        float movementSpeed = walkSpeed;

        // Check if player is running.
        if (Input.GetButton("Sprint") && ! sprintDisabled)
        {
            movementSpeed = runSpeed;
        }
        
        // Read WASD/arrow-key button presses.
        float moveH = Input.GetAxis("Horizontal"); // Horizontal movement.
        float moveV = Input.GetAxis("Vertical"); // Vertical movement.
        
        // Apply player movement to a vector and clamp diagonal movement.
        Vector3 movement = new Vector3( moveH, 0, moveV );
        Vector3.ClampMagnitude(movement, 1f);

        // Apply movement speed and make it fps-independent.
        movement = movement *movementSpeed * Time.fixedDeltaTime;

        // Move player to new positon.
        rb.MovePosition(transform.position + movement);
        
        // Turn player to face the moving direction.
        if (movement != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(rotation);
        }
    }

    // Deplete sprint energy meter whenever player holds sprint key.
    // If sprint meter reaches 0, disable spriting for some time.
    IEnumerator DepleteSprint()
    {
        while (true)
        {
            if (Input.GetButton("Sprint"))
            {
                sprintDepleting = true;
                if (! Mathf.Approximately(SprintEnergy, 0))
                {
                    SprintEnergy = Mathf.MoveTowards(SprintEnergy, 0, sprintDepleteRate * Time.deltaTime);
                    yield return null;
                }
                else
                {
                    sprintDisabled = true;
                    yield return new WaitForSeconds(sprintDisabledTime);
                    sprintDisabled = false;
                }
            }
            else
            {
                sprintDepleting = false;
            }

            yield return null;
        }
    }

    // Refill sprint energy meter whenever it is not depleting.
    IEnumerator RefillSprint()
    {
        while (true)
        {
            if (! sprintDepleting)
            {
                SprintEnergy = Mathf.MoveTowards(SprintEnergy, 1, sprintRefillRate * Time.deltaTime);
            }

            yield return null;
        }
    }

    IEnumerator UpdateSprintEnergySlider()
    {
        while (true)
        {
            SprintEnergySlider.value = SprintEnergy;

            yield return null;
        }
    }

    //Called when the playe collides with visionCCTV
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == visionCCTV)
        {
            gameOverPanel.SetActive(true);

            ObserverEvent collision = new ObserverEvent("collision");
            playerSubject.Notify(collision); //Observer pattern to notify of collision

            Destroy(gameObject); //destroys the player character
        }

    }

}


