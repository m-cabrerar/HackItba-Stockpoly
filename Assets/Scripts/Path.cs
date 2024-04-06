using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> nodeList = new List<Transform>();

    void getNodes()
    {
        nodeList.Clear();
        Transform[] childObjs = GetComponentsInChildren<Transform>();

        foreach (Transform node in childObjs)
        {
            if (node == this.transform) { continue; }
            nodeList.Add(node);
        }
    }

    void OnDrawGizmos()
    {
        getNodes();
        Gizmos.color = Color.green;
        for (int i  = 0; i < nodeList.Count; i++)
        {
            Vector3 a = nodeList[i].position;
            Vector3 b;
            if(i==0) { b = nodeList[nodeList.Count-1].position; }
            else { b = nodeList[i-1].position; }
            Gizmos.DrawLine(a, b);
        }
    }

}
