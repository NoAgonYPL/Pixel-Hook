using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete_Level : MonoBehaviour
{
    [SerializeField] List<GameObject> activeObjects = new List<GameObject>();
    [SerializeField] GameObject completeUILevel;

    // Start is called before the first frame update
    void Start()
    {
        completeUILevel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lever_Trigger"))
        {
            completeUILevel.SetActive(true);

            for (int i = 0; i < activeObjects.Count; i++)
            {
                activeObjects[i].SetActive(false);
            }
            Debug.Log("Door Open");
        }
    }
}
