using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public new Camera camera;
    public GameObject bulletPrefab;
    
    private float shootSpeed = 0.2f;
    private bool _isShooting;
    
    void Start()
    {
        GetComponent<Rigidbody2D>();
        _isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
    
#if UNITY_STANDALONE || UNITY_WEBPLAYER
    //
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            StartCoroutine(Move());
            if (!_isShooting)
                StartCoroutine(Shoot());
        }
#endif
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        Destroy(bullet, 1f);
        yield return new WaitForSeconds(shootSpeed);
        _isShooting = false;
    }
    private IEnumerator Move()
    {
        Vector3 newPosition = camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        transform.position = new Vector3(newPosition.x, transform.position.y, 0f);
        yield return null;
    }

    private void OnMouseDrag()
    {        
        Vector3 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(newPosition.x, transform.position.y, 0f);
        if (!_isShooting)
            StartCoroutine(Shoot());
    }
}
