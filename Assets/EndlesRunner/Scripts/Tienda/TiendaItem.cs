using UnityEngine;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Tienda/Item")]
public class TiendaItem : ScriptableObject
{
    public string itemNombre;
    public Sprite icono;
    public int precio;
    public bool equipable;

    [Header("Música")]
    public AudioClip musicaClip;
    public int clipIndex;        // Índice en AudioShop.musicClips
}
