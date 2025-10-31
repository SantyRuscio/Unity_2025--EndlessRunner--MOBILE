using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Tienda/Item")]
public class TiendaItem : ScriptableObject
{
    public string itemNombre;
    public Sprite icono;
    public int precio;
    public bool equipable;
}
