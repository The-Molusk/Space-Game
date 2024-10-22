using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float pitchSpeed, yawSpeed, rollSpeed, moveSpeed, acceleration;
    private float currentPitchSpeed, currentYawSpeed, currentRollSpeed, currentMoveSpeed;
    
    [SerializeField] float planeHeight;
    // Update is called once per frame
    void Update()
    {
        

        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, acceleration * Time.deltaTime);
        currentYawSpeed = Input.GetAxisRaw("Horizontal") * yawSpeed * Time.deltaTime;
        currentRollSpeed = Input.GetAxisRaw("Roll") * rollSpeed * Time.deltaTime;
        currentPitchSpeed = Input.GetAxisRaw("Pitch") * pitchSpeed * Time.deltaTime * -1;
        if(currentMoveSpeed > 0.005f && currentMoveSpeed < 0.01f)
        {
            transform.Rotate(0, currentYawSpeed / 2, 0);
        }
        if (currentMoveSpeed > 0.01f)
        {
            transform.Rotate(0, currentYawSpeed, 0);
        }
        if (currentMoveSpeed > 0.03f && currentMoveSpeed <= 0.04f)
        {
            transform.Rotate(currentPitchSpeed/5, 0, currentRollSpeed/5);
        }
        else if (currentMoveSpeed > 0.04f && currentMoveSpeed <= 0.05f)
        {
            transform.Rotate(currentPitchSpeed/4, 0, currentRollSpeed/4);
        }
        else if (currentMoveSpeed > 0.05f && currentMoveSpeed <= 0.06f)
        {
            transform.Rotate(currentPitchSpeed/3, 0, currentRollSpeed/3);
        }
        else if (currentMoveSpeed > 0.06f && currentMoveSpeed <= 0.07f)
        {
            transform.Rotate(currentPitchSpeed/3, 0, currentRollSpeed/2);
        }
        else if (currentMoveSpeed > 0.07f)
        {
            transform.Rotate(currentPitchSpeed, 0, currentRollSpeed);
        }


        transform.position = transform.position + (transform.forward * currentMoveSpeed * -1);

        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position); 
        if ((terrainHeight + planeHeight) > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, terrainHeight + planeHeight, transform.position.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 0))
        {
            Debug.Log("Down");
            //down
        }
        if (Mathf.Approximately(angle, 180))
        {
            Debug.Log("Up");
        }
        if (Mathf.Approximately(angle, 90))
        {
            // Sides
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y > 0)
            { // left side of the player
                Debug.Log("Left");
            }
            else
            { // right side of the player
                Debug.Log("Right");
            }
        }
        else
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.z > 0)
            {
                Debug.Log("Front");
            }
            else
            { // right side of the player
                Debug.Log("Back");
            }
        }
        //if (collision.gameObject.tag == "wall")
        //{
        //    transform.position = new Vector3(transform.position.z, transform.position.y, transform.position.z);
        //}
    }
    
    

}
