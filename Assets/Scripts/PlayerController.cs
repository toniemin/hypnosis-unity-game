using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    private float walkSpeed = 10f; // Player's normal movement speed.
    private float runSpeed = 17f; // Player's running speed.
    private float rotationSpeed = 5f; // Player's rotation speed.

    public GameObject visionCCTV; //GameObject of CCTV's vision
    public GameObject gameOverPanel; //Panel for displaying the "You have been spotted!" text

    // Running mechanic variables.
    private float sprintDepletionRate = .3f;

    private float sprintRefillRate = .6f;
    private float sprintRefillDelay = .3f;

    private float sprintDisableRefillDelay = 1f;

    private IEnumerator sprintDepleter;
    private IEnumerator sprintDisabler;
    private IEnumerator sprintRefiller;

    //private bool sprintDepleting = false;
    private bool sprintDisabled = false;
    private bool sprintRefilling = false;
    
    public float SprintEnergy { get; private set; } = 1;

    Subject playerSubject = new Subject();
    Subject soundSubject = new Subject();

    void Awake()
    {
        gameOverPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();

        playerSubject.AddObserver(new PlayerNotifier());
        soundSubject.AddObserver(new SoundNotifier());

        sprintDepleter = depleteSprint(sprintDepletionRate);
        sprintDisabler = disableSprint(sprintDisableRefillDelay);
        sprintRefiller = refillSprint(sprintRefillDelay, sprintRefillRate);
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Sprint") && ! sprintDisabled)
        {
            if (sprintRefilling)
            {
                StopCoroutine(sprintRefiller);
            }

            StartCoroutine( sprintDepleter );
        }

        if (Input.GetButtonUp("Sprint") && ! sprintRefilling)
        {
            StopCoroutine(sprintDepleter);
            StartCoroutine(sprintRefiller);
        }

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

    // Deplete sprint meter and call disableSprint if meter gets to 0.
    IEnumerator depleteSprint(float rate)
    {
        while (Mathf.Approximately(SprintEnergy, 0))
        {
            SprintEnergy = Mathf.MoveTowards(SprintEnergy, 0, rate * Time.deltaTime);

            yield return null;
        }

        StartCoroutine( sprintDisabler );
    }

    IEnumerator disableSprint(float refillDelay)
    {
        sprintDisabled = true;

        yield return new WaitForSeconds(refillDelay);

        StartCoroutine( sprintRefiller );
    }

    // Wait for the delay and then start refilling sprint meter. 
    // If sprint was disabled, renable after meter full.
    IEnumerator refillSprint(float delay, float rate)
    {
        sprintRefilling = true;

        yield return new WaitForSeconds(delay);

        while (Mathf.Approximately(SprintEnergy, 1))
        {
            SprintEnergy = Mathf.MoveTowards(SprintEnergy, 1, rate * Time.deltaTime);

            yield return null;
        }

        sprintDisabled = false;

        sprintRefilling = false;
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


