using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitBase : MonoBehaviour
{
    
    [HideInInspector] public List<Vector2> MoveVariants = new List<Vector2>();

    public int ColorIndex = 0;

    [SerializeField] GroundInteractor _groundInteractor;
    [HideInInspector] public List<Cell> Variants = new List<Cell>();

    [SerializeField] Cell _currentCell;
    [SerializeField] PressurePlate _pressurePlate;

    [SerializeField] AudioClip _moveSound;
    private AudioSource _audioSource;

    ColorManager _colorManager;

    LevelManager _levelManager;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        MoveVariants.Add(new Vector2(0f, 0f));
        MoveVariants.Add(new Vector2(1f, 0f));
        MoveVariants.Add(new Vector2(0f, 1f));
        MoveVariants.Add(new Vector2(-1f, 0f));
        MoveVariants.Add(new Vector2(0f, -1f));

        _levelManager = FindObjectOfType<LevelManager>();

        var colorManager = FindObjectOfType<ColorManager>();
        
        if (colorManager)
        {
            if (colorManager.Units == null)
                colorManager.Units = new List<UnitBase>();
            colorManager.Units.Add(this);
            _colorManager = colorManager;
            UpdateColor();
        }
    }

    public void UpdateColor()
    {
        var mesh = gameObject.GetComponent<MeshRenderer>();
        if (mesh)
            mesh.material.color = _colorManager.GetColor(ColorIndex);
    }
    private void OnMouseDown()
    {
        if (FindObjectOfType<PauseMenu>().GamePaused == false)
        {
            if (_groundInteractor)
            {
                if (_groundInteractor.GroundRepos.SelectedCell)
                {
                    _groundInteractor.DeselectCell(_groundInteractor.GroundRepos.SelectedCell);
                }

                _groundInteractor.SelectedCell(_currentCell);
            }
        }
    }
    public void MoveToCell(Cell cell)
    {
        if (cell.Unit == null)
        {
            Move(cell);
            _colorManager.ChangeColors(this);

            

            if (_levelManager)
            {
                var state = _colorManager.PlayerWin();
                Debug.Log("STATE -- " + state);
                _levelManager.MoveMade(state); 
            }
        }
    }
    private void Move(Cell cell)
    {
        var c = _groundInteractor.GroundRepos.SelectedCell;
        _groundInteractor.DeselectCell(c);
        c.Unit = null;
        

        transform.position = new Vector3(cell.transform.position.x,
            transform.position.y, cell.transform.position.z);

        cell.Unit = this;

        _currentCell = cell;
        _pressurePlate = cell.Plate;

        _audioSource.PlayOneShot(_moveSound);
    }

    [ContextMenu("Init cell")]
    public void InitCell()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.GetComponent<Cell>())
            {
                transform.position = new Vector3(hit.transform.position.x,
                    transform.position.y, hit.transform.position.z);

                var cell = hit.collider.GetComponent<Cell>();

                _groundInteractor = cell.GroundInteractor;
                _currentCell = cell;
                cell.Unit = this;
            }
        }
    }

    
    /*private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("trigger ENTER");
            _pressurePlate = other.GetComponent<PressurePlate>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _pressurePlate = null;
    }*/

    public bool IsWinable()
    {
        bool result = false;

        if (_pressurePlate)
        {
            if (_pressurePlate.GetColorNum() % _colorManager.GetColorAmount() ==
                ColorIndex % _colorManager.GetColorAmount())
                result = true;
        }

        return result;
    }

}
