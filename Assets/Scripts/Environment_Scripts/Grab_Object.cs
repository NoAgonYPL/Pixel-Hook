using UnityEngine;

public class Grab_Object : MonoBehaviour
{

    [SerializeField] Grab_Rope grab_Rope_Ref;
    [SerializeField] BoxCollider2D thisBox2D;
    

    // Start is called before the first frame update
    void Start()
    {
        grab_Rope_Ref = FindObjectOfType<Grab_Rope>();
        thisBox2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (grab_Rope_Ref.targetIsGrabbed)
        {
            thisBox2D.enabled = false;
        }
        else
        {
            thisBox2D.enabled = true;
        }
    }
}
