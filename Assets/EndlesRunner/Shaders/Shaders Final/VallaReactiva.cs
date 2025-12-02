using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VallaReactiva : MonoBehaviour
{
    private Transform player;
    private List<Material> materialesHijos = new List<Material>();

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            materialesHijos.Add(rend.material);
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 posPlayer = player.position;

        foreach (Material mat in materialesHijos)
        {
            if (mat != null)
            {
                mat.SetVector("_PlayerPosition", posPlayer);
            }
        }
    }
}
