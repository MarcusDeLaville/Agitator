using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class MaterialAgitation
{
    public PropagandaMaterial PropagandaType;
    public Rigidbody MaterialPrefab;
    public Text TextMaterialCount;
}

public class DropMaterials : MonoBehaviour
{
    [SerializeField] private List<MaterialAgitation> _materialAgitations;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private PropagandaMaterialsStorage _propagandaMaterialsStorage;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private int _currentMaterialIndex;
    [SerializeField] private MaterialAgitation _choosedMaterial;
    
    [SerializeField] private float _forse;

    [SerializeField]private MaterialsText _materialsText;
    
    private void Start()
    {
        _materialsText = FindObjectOfType<MaterialsText>();
        PickTexts();
        ChooseMaterial();
    }

    private void Update()
    {
        float mouseRotation = Input.GetAxis("Mouse ScrollWheel");
        
        if (mouseRotation != 0)
        {
            if (mouseRotation > 0.05f)
            {
                _currentMaterialIndex++;
            
                if (_currentMaterialIndex > _materialAgitations.Count - 1)
                {
                    _currentMaterialIndex = 0;
                }
            }
            if (mouseRotation < -0.05f)
            {
                _currentMaterialIndex--;
            
                if (_currentMaterialIndex < 0)
                {
                    _currentMaterialIndex = _materialAgitations.Count - 1;
                }
            }
            
            ChooseMaterial();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Drop();
        }
    }

    private void Drop()
    {
        if (_propagandaMaterialsStorage.IsEnoughMaterial(_choosedMaterial.PropagandaType))
        {
            _propagandaMaterialsStorage.DepriveMaterials(_choosedMaterial.PropagandaType);
            Rigidbody gameObject = Instantiate(_choosedMaterial.MaterialPrefab ,_spawnPosition.position, Quaternion.identity);
            gameObject.AddForce(transform.TransformDirection(Vector3.forward) * _forse, ForceMode.Impulse);
            _animator.SetTrigger("Drop");
        }
    }

    //todo: костыль, потом с ним что0то сделать
    private void PickTexts()
    {
        _choosedMaterial.TextMaterialCount = _materialsText.Texts.PaperSmall;
        _materialAgitations[0].TextMaterialCount = _materialsText.Texts.PaperSmall;
        _materialAgitations[1].TextMaterialCount = _materialsText.Texts.PaperBig;
        _materialAgitations[2].TextMaterialCount = _materialsText.Texts.LeafletsSmall;
        _materialAgitations[3].TextMaterialCount = _materialsText.Texts.LeafletsBig;
        _materialAgitations[4].TextMaterialCount = _materialsText.Texts.Poster;
    }
    private void ChooseMaterial()
    {
        if (_choosedMaterial.TextMaterialCount == null)
        {
            PickTexts();
        }
        
        _choosedMaterial.TextMaterialCount.color = Color.white;
        _choosedMaterial = _materialAgitations[_currentMaterialIndex];
        _choosedMaterial.TextMaterialCount.color = Color.red;
    }
}
