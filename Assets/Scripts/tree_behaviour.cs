using UnityEngine;

public class tree_behaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject leafsContainer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Distopic Bullet")
        {
            Debug.Log("Tree Distopic");
            Rigidbody[] childrenRigidbodies = leafsContainer.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in childrenRigidbodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Destroy(collision.gameObject);
        }
    }
}
