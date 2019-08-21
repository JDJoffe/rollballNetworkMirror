using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player : NetworkBehaviour
{
    public GameObject bombPrefab;

    public Transform playerVirtCam;
    public Camera playerCam;
    public float speed = 10f, jump = 10f;
    public LayerMask ignoreLayers;
    public float rayDistance = 10f;
    public bool isGrounded = false;
    private Rigidbody rigid;
    #region Unity Events
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
    private void Start()
    {
        playerVirtCam.transform.SetParent(null);
        playerCam.transform.SetParent(null);
        //or 
        //playerCam.enabled = isLocalPlayer;
        if (isLocalPlayer)
        {
            playerCam.enabled = true;
            //   playerCam.rect = new Rect(0f, 0f, 0.5f, 1f);
            playerVirtCam.gameObject.SetActive(true);
        }
        else
        {
            playerCam.enabled = false;
            //   playerCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            playerVirtCam.gameObject.SetActive(false);
        }
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(groundRay, rayDistance, ~ignoreLayers);
    }
    private void OnTriggerEnter(Collider col)
    {
        Item item = col.GetComponent<Item>();
        if (item)
        {
            item.Collect();
        }
    }
    private void OnDestroy()
    {
       
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdSpawnBomb(transform.position);
            }
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Move(inputH, inputV);
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }


        }

    }
    #endregion
    #region Commands
    [Command]
    private void CmdSpawnBomb(Vector3 pos)
    {
        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
        NetworkServer.Spawn(bomb);
    }
    #endregion
    #region Custom
    private void Jump()
    {
        if (isGrounded)
        {
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
  
    private void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0, inputV);

        // [Optional] Move with camera
        Vector3 euler = Camera.main.transform.eulerAngles;
        direction = Quaternion.Euler(0, euler.y, 0) * direction; // Convert direction to relative direction to camera only on Y

        rigid.AddForce(direction * speed);
    }
    #endregion
}

