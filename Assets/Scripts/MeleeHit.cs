using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    [SerializeField]private Material melee;
    [SerializeField]private Material weapon;

    [SerializeField] private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
      mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.collider.CompareTag("Enemy")) StartCoroutine(SwapMat());
            
        
    }

    IEnumerator SwapMat()
    {
        mr.material = melee;
        yield return new WaitForSeconds(0.2f);
        mr.material = weapon;
    }
}
