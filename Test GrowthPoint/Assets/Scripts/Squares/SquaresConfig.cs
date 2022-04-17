using UnityEngine;

[CreateAssetMenu]
public class SquaresConfig : ScriptableObject
{
    [Header("Settings")] 
    [SerializeField] private string _id;
    [SerializeField] private Color _color;
    [SerializeField] private int _points;

    public string Id => _id;
    public Color Color => _color;
    public int Points => _points;
}
