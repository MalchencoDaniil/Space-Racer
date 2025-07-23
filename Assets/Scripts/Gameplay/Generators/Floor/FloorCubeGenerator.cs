using System.Collections.Generic;
using UnityEngine;

public class FloorCubeGenerator : MonoBehaviour
{
    private List<GameObject> _cubes = new List<GameObject>();

    private MeshRenderer _planeRenderer;
    private MeshFilter _planeFilter;

    private int _maxCubeCount;
    private float _averageCubeWidth, _averageCubeDepth;
    private bool _recalculateMaxCount = true;

    private List<Rect> _occupiedRects = new List<Rect>();
    private int _seed;

    [Header("Floor Settings")]
    [SerializeField] private float _minCubeWidth = 1f;
    [SerializeField] private float _maxCubeWidth = 3f;

    [Space(8)]
    [SerializeField] private float _minCubeDepth = 1f;
    [SerializeField] private float _maxCubeDepth = 3f;

    [Space(8)]
    [SerializeField] private float _minCubeHeight = 0.5f;
    [SerializeField] private float _maxCubeHeight = 1.5f;

    [Space(8)]
    [SerializeField] private float _planeOffset = 0.01f;

    [Header("Random")]
    [SerializeField] private bool _useRandomSeed = false;
    [SerializeField] private int _currentSeeed = 0;

    [Header("Density Settings")]
    [SerializeField, Range(0.1f, 1f)] private float _cubeDensity = 0.5f;

    [Header("Material")]
    [SerializeField] private Material _cubeMaterial;

    private void Start()
    {
        GenerateFloor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateFloor();
        }

        if (_recalculateMaxCount)
        {
            RecalculateMaxCount();
        }
    }

    void GenerateFloor()
    {
        SeedInitialize();

        ClearFloor();
        _occupiedRects.Clear();

        _planeRenderer = GetComponent<MeshRenderer>();
        _planeFilter = GetComponent<MeshFilter>();

        Bounds _bounds = _planeRenderer.bounds;

        float _planeWidth = _bounds.size.x;
        float _planeDepth = _bounds.size.z;

        Vector3 _planeCenter = _bounds.center;

        _averageCubeWidth = (_minCubeWidth + _maxCubeWidth) / 2f;
        _averageCubeDepth = (_minCubeDepth + _maxCubeDepth) / 2f;

        RecalculateMaxCount();

        for (int i = 0; i < _maxCubeCount; i++)
        {
            float _cubeWidth = Random.Range(_minCubeWidth, _maxCubeWidth);
            float _cubeDepth = Random.Range(_minCubeDepth, _maxCubeDepth);
            float _cubeHeight = Random.Range(_minCubeHeight, _maxCubeHeight);

            int _attempts = 0;

            Vector2 _cubePosition2D;
            do
            {
                float _randomX = Random.Range(-_planeWidth / 2 + _cubeWidth / 2, _planeWidth / 2 - _cubeWidth / 2);
                float _randomZ = Random.Range(-_planeDepth / 2 + _cubeDepth / 2, _planeDepth / 2 - _cubeDepth / 2);

                _cubePosition2D = new Vector2(_randomX, _randomZ);
                _attempts++;

                if (_attempts > 100)
                {
                    Debug.Log("Too many attempts");
                    break;
                }

            } while (IsOverlapping(_cubePosition2D, _cubeWidth, _cubeDepth));

            if (_attempts > 100)
                continue;

            GameObject _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cube.transform.localScale = new Vector3(_cubeWidth, _cubeHeight, _cubeDepth);

            int _randomRotation = Random.Range(0, 4) * 90;
            _cube.transform.rotation = Quaternion.Euler(0, _randomRotation, 0);

            Vector3 _cubePosition = _planeCenter + new Vector3(_cubePosition2D.x, _cubeHeight / 2 + _planeOffset, _cubePosition2D.y);

            _cube.transform.position = _cubePosition;

            if (_cubeMaterial != null)
            {
                _cube.GetComponent<Renderer>().material = _cubeMaterial;
            }

            _cubes.Add(_cube);
            _cube.transform.SetParent(transform);

            _occupiedRects.Add(new Rect(_cubePosition2D.x - _cubeWidth / 2, _cubePosition2D.y - _cubeDepth / 2, _cubeWidth, _cubeDepth));
        }
    }

    private void SeedInitialize()
    {
        InitializeSeed();
        Random.InitState(_seed);
    }

    private void InitializeSeed()
    {
        if (_useRandomSeed)
        {
            _seed = System.Environment.TickCount;
        }
        else
        {
            _seed = _currentSeeed;
        }
    }

    bool IsOverlapping(Vector2 _position, float _width, float _depth)
    {
        Rect _newRect = new Rect(_position.x - _width / 2, _position.y - _depth / 2, _width, _depth);

        foreach (Rect _occupied in _occupiedRects)
        {
            if (_newRect.Overlaps(_occupied))
            {
                return true; 
            }
        }
        return false;
    }

    public void RecalculateMaxCount()
    {
        if (_planeRenderer == null) 
            return;

        Bounds _bounds = _planeRenderer.bounds;

        float _planeWidth = _bounds.size.x;
        float _planeDepth = _bounds.size.z;

        _maxCubeCount = Mathf.FloorToInt((_planeWidth * _planeDepth) / (_averageCubeWidth * _averageCubeDepth) * _cubeDensity);

        _recalculateMaxCount = false;
    }

    public void ClearFloor()
    {
        foreach (GameObject _cube in _cubes)
        {
            if (_cube != null)
            {
                DestroyImmediate(_cube);
            }
        }

        _cubes.Clear();
        _occupiedRects.Clear();
    }
}