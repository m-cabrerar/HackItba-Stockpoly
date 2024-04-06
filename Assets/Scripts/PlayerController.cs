using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed = 2f;
    [SerializeField] float wait = 0.2f;
    public HashSet<Integer> properties = new HashSet<Integer>();
    [HideInInspector] public int posicionTablero = 0;
    [HideInInspector] public int steps;
    bool isMoving;
    
    public IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        for (;steps > 0; steps--)
        {
            Vector3 nextPos = path.nodeList[(posicionTablero + 1) % path.nodeList.Count].position;
            while (MoveToNode(nextPos)) {yield return null;}
            yield return new WaitForSeconds(wait);
            posicionTablero++;
        }

        isMoving = false;
    }

    bool MoveToNode(Vector3 node)
    {
        return node != (transform.position = Vector3.MoveTowards(transform.position, node, speed * Time.deltaTime));
    }

}
