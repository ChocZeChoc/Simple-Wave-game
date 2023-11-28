using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float resTime = 0.1f;
    [SerializeField] private float resMulti = 8f;

    public bool playerRes = true;

    // Start is called before the first frame update
    void Start()
    {
        
        offset = new Vector3(0f, 0f, 0f);

    }

    

    // Update is called once per frame
    void Update()
    {
        if(target != null && !playerRes)
        {

            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        if(playerRes)
        {
            StartCoroutine(PlayerRespawn());
        }

    }

    IEnumerator PlayerRespawn()
    {
        playerRes = false;

        yield return new WaitForSeconds(resTime);

        for (float i = 0; i < resMulti; i++)
        {
            
            offset = target.position + offset + new Vector3(0f,i,-i);
            
            Vector3 resPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, resPosition, ref velocity, smoothTime);

            yield return new WaitForSeconds(resTime);
            
        }
        

        offset = new Vector3(0f,resMulti,-resMulti);

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }


}
