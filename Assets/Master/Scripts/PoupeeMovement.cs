using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoupeeMovement : MonoBehaviour
{

    private GameObject father_toFollow;

    private List<Vector3> points_toFollow = new List<Vector3>();
    public float distance_toFollow;

    public int num_point_toFollow;

    public void SetFather(GameObject new_father)
    {
        father_toFollow = new_father;
    }

    public void Set_pointsToFollow()
    {
        if (father_toFollow != null)
        {
            for (int x = 0; x < num_point_toFollow; x++)
            {
                points_toFollow.Add(father_toFollow.transform.position);
                Debug.Log(points_toFollow.Count);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (points_toFollow.Count >= num_point_toFollow)
        {
            Update_points_toFollow();
        }

    }

    public void Update_points_toFollow()
    {
        if ((points_toFollow[points_toFollow.Count - 1] - father_toFollow.transform.position).magnitude > distance_toFollow)
        {
            points_toFollow.RemoveAt(0);
            points_toFollow.Add(father_toFollow.transform.position);
            Move();
        }
    }

    public void Move()
    {
        transform.position = points_toFollow[0];
    }
}
