using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Mouse : MonoBehaviour
{
    [SerializeField]
    [Range(2, 12)]          // Slider
    public float speed = 4; // Moving Speed

    // Clicked Position
    private Vector3 targetPosition;

    // Define if is moving
    private bool isMoving = false;

    // Clicked Animate Object
    private Transform clickVFX;

    //Walking simulation animation
    private Animator ani;

    // Get VFX 
    private void Awake()
    {
        clickVFX = GameObject.Find("Nav-VFX").GetComponent<Transform>();
        ani = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }

        if (isMoving)
        {
            Move();
        }
    }

    void SetTargetPosition()
    {
        // Get mouse target position
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;


        // Visual Effect for Click, pass in position
        StartCoroutine(PlayClickedAni(targetPosition));
        
        // Define is Moving
        isMoving = true;

        //Define animator bool
        ani.SetBool("isWalking", true);
    }

    void Move()
    {
        // Rotate to moving position
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            EndMoving();
        }
    }

    // Fire to stop moving
    void EndMoving()
    {
        isMoving = false;
        ani.SetBool("isWalking", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EndMoving();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EndMoving();
    }

    IEnumerator PlayClickedAni(Vector3 posi)
    {
        // Clone a Nav-VFX
        Transform tmp;
        tmp = Instantiate(clickVFX, posi, Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        
        // Destroy after 0.5s
        Destroy(tmp.gameObject);


    }
}
