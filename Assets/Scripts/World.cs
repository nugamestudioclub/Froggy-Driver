using System;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    [SerializeField]
    private Hand hand;
    public Hand Hand => hand;

    [SerializeField]
    private Camera interiorCamera;
    public Camera InteriorCamera => interiorCamera;

    [SerializeField]
    private Camera topDownCamera;
    public Camera TopDownCamera => topDownCamera;

    [SerializeField]
    private Camera firstPersonCamera;
    public Camera FirstPersonCamera => firstPersonCamera;

    [SerializeField]
    private Camera leftMirrorCamera;
    public Camera LeftMirrorCamera => leftMirrorCamera;

    [SerializeField]
    private Camera rightMirrorCamera;
    public Camera RightMirrorCamera => rightMirrorCamera;

    [SerializeField]
    private CabinController cabin;

    public enum Layer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        TopDown = 6,
        Interior = 7,
        FirstPerson = 8,
    }

    public Camera Camera(Layer layer)
    {
        return layer switch
        {
            Layer.Interior => InteriorCamera,
            Layer.TopDown => TopDownCamera,
            Layer.FirstPerson => FirstPersonCamera,
            _ => null,
        };
    }

    public Camera Camera(int layerId)
    {
        return Enum.IsDefined(typeof(Layer), layerId)
            ? Camera((Layer)layerId)
            : throw new ArgumentException(nameof(layerId));
    }

    void Awake()
    {
        Instance = this;
    }

    public void Take(GameObject obj)
    {
        Destroy(obj);
    }

    public void Give(GameObject obj)
    {
        cabin.Take(obj);
    }
}