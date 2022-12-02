using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;
public class CameraBehaviour : MonoBehaviour
{
    
    [SerializeField] bool walkingCam;
    public GameObject target;
    private Vector3 toFollow;
    [SerializeField] float moveSpeed;
    [SerializeField] private float targety;
    [SerializeField] private float targetx;
    private float counter;

    [SerializeField] Transform left, right;

    [Header("Delay na camera")]
    [Tooltip("Maior valor menor delay")]
    public float delay;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(walkingCam)
        {
            WalkingToKill();
        }
        else
        {
            FollowCam();
        }

    }

    void FollowCam()
    {
        Vector3 pos = transform.position;

        if (!Player.instance.isRight && pos.x >= left.position.x)
        {
            toFollow = (new Vector3(target.transform.position.x + (-1 * targetx), target.transform.position.y + targety, -1));
            Vector3 newPosition = new Vector3(toFollow.x, toFollow.y, toFollow.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delay);

        }

        if (Player.instance.isRight && pos.x <= right.position.x)
        {
            toFollow = (new Vector3(target.transform.position.x + targetx, target.transform.position.y + targety, -1));
            Vector3 newPosition = new Vector3(toFollow.x, toFollow.y, toFollow.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delay);
        }
        // this.transform.position = new Vector3(target.transform.position.x + targetx, target.transform.position.y + targety, -10);
    }

    void WalkingToKill()
    {
        Vector3 pos = transform.position;

        if (pos.x >= left.position.x)
        {
            toFollow = (new Vector3(pos.x += moveSpeed *Time.deltaTime , target.transform.position.y + targety, -1));
            Vector3 newPosition = new Vector3(toFollow.x, toFollow.y, toFollow.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delay);

        }

        if (pos.x <= right.position.x)
        {
           // toFollow = (new Vector3(target.transform.position.x + targetx, target.transform.position.y + targety, -1));
            //Vector3 newPosition = new Vector3(toFollow.x, toFollow.y, toFollow.z);
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delay);
        }
    }
}