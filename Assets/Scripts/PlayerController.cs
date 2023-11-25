using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;

    [SerializeField] private float speed;
    [SerializeField] private float TurnSpeed = 0.15f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletDirection;

    [SerializeField] private float timeBetweenShots = 0.5f;
    private bool canShoot = true;

    [SerializeField] private int playerHP = 10;
    [SerializeField] TextMeshProUGUI hpText;
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnMouselook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }


    // Start is called before the first frame update
    void Start()
    {
        hpText.text = playerHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

        if(Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }
        MoveplayerWithAim();

        hpText.text = playerHP.ToString();

        if(playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if(movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), TurnSpeed);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
    public void MoveplayerWithAim()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

        if(aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed);
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void PlayerShoot()
    {
        if (!canShoot) return;
        GameObject shoot = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        shoot.SetActive(true);
        StartCoroutine(CanShoot());
        Debug.Log("Shoot");
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.collider.CompareTag("Enemy"))
        {
            playerHP--;
        }
        
    }
}
