using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public GameObject Projectile;

    public float speed;
    public float maxRotation;
    public float minRotation;

    CharacterController playerMovement;
    public Transform camera;

    Vector3 vel;

    public float sensitivity;

    float xRotation = 0;

    List<GameObject> projectilePool = new List<GameObject>();//creates a list for projectiles
    public int projectileNum;
    int projectileIndex = 0;

    AudioSource myAudioSource;
    public AudioClip shootSound;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();//gets audio
    
        playerMovement = GetComponent<CharacterController>();//gets character control
        CreateProjectilePool();//generates projectile list at start
    }

    
    void Update()
    {
        vel.z = Input.GetAxis("Vertical") * speed;//Tracks movemnet direction when moving on z axis and sets movement speed on the axis
        vel.x = Input.GetAxis("Horizontal") * speed;//Tracks movement direction when moving on x axis and sets movement speed on the axis

        vel = transform.TransformDirection(vel);
        playerMovement.SimpleMove(vel);//locks movement speed to time instead of framerate

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);//binds horizontal camera movement to mouse x
        xRotation -= Input.GetAxis("Mouse Y") * sensitivity;//binds vertical camera movement to mouse y
        xRotation = Mathf.Clamp(xRotation, -maxRotation, minRotation);//allows minimum and maximum vertical looking parameters
        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);

        Debug.Log(projectileIndex);
        if (Input.GetMouseButtonDown(0))//detects if left click is used then executes the following
        {
            myAudioSource.PlayOneShot(shootSound);//plays sound
            GameObject currentBullet = projectilePool[projectileIndex];//reviews the list to find the current projectile
            currentBullet.SetActive(true);//sets the current projectile as active
            currentBullet.transform.position = transform.position;//allows bullet to move
            currentBullet.GetComponent<Rigidbody>().velocity = 5 * transform.forward;//gives the projectile collisons while moving
            projectileIndex++;//sets the next projectile
            if(projectileIndex >= projectilePool.Count)//resets projectiles once list is exhausted
            {
                projectileIndex = 0;
            }
        }
    }

     void CreateProjectilePool()
     {
         for(int i = 0; i < projectileNum; i++)//creates a for loop to keep track of projectile pool
         {
            GameObject newBullet = Instantiate(Projectile, transform.position, Quaternion.identity);//summons a projectile with a specific position
            newBullet.SetActive(false);
            projectilePool.Add(newBullet);
         }
     }
}
