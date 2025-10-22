using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public BouncyBall BouncyBall;
    public float speed = 5f;       
    public float maxX = 7.5f;
    private bool stopcalling = true;

    float movementHorizontal;       
    void Start()
    {

    }


    void Update()
    {
        movementHorizontal = Input.GetAxis("Horizontal");

        // Check if we're within bounds before moving
        if ((movementHorizontal > 0 && transform.position.x < maxX) ||
            (movementHorizontal < 0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * movementHorizontal * speed * Time.deltaTime;
        }

        if (stopcalling) 
        {
            if (transform.position.x >= 0.01 || transform.position.x <= -0.01)
            {
                BouncyBall.StartCoroutine(BouncyBall.startBall());
                stopcalling = false;
            }
        }
        
    }
}
