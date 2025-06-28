using UnityEngine;

public class pickableObject : MonoBehaviour
{
    public GameObject confetti;
    Material myMat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().material;
        myMat.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            //Instantiate(confetti, collision.GetContact(0).point, Quaternion.identity);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        
    }
}
