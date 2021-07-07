using System;
using System.Collections.Generic;
using UnityEngine;

public enum PropagandaMaterial
{
    LeafletSmall = 0,
    LeafletBig = 1,
    PaperSmall = 2,
    PaperBig = 3,
    Poster = 4
}
public class PropagandaMaterialsStorage : MonoBehaviour
{
    [SerializeField] private int _maxCapacity = 20;

    public int LeafletsSmall { get; private set; } = 0;
    public int LeafletsBig { get; private set; } = 0;
    public int PaperSmall { get; private set; } = 0;
    public int PaperBig { get; private set; } = 0;
    public int Poster { get; private set; } = 0;

    public Action ChangetMaterialsCount;
    private List<int> _materials = new List<int>();

    private void Awake()
    {
        LoadProgress();    
    }

    private void Start()
    {
        _materials.Add(LeafletsSmall);
        _materials.Add(LeafletsBig);
        _materials.Add(PaperSmall);
        _materials.Add(PaperBig);
        _materials.Add(Poster);

        ChangetMaterialsCount += SaveProgress;
    }

    public void AddMaterials(PropagandaMaterial propagandaMaterial)
    {
        switch (propagandaMaterial)
        {
            case PropagandaMaterial.Poster:
                Poster++;
                break;
            case PropagandaMaterial.LeafletBig: 
                LeafletsBig++;
                break;
            case PropagandaMaterial.LeafletSmall:
                LeafletsSmall++;
                break;
            case PropagandaMaterial.PaperBig:
                PaperBig++;
                break;
            case PropagandaMaterial.PaperSmall:
                PaperSmall++;
                break;
        }
        
        Recount();
        ChangetMaterialsCount?.Invoke();
    }

    public void DepriveMaterials(PropagandaMaterial propagandaMaterial)
    {
        switch (propagandaMaterial)
        {
            case PropagandaMaterial.Poster:
                Poster--;
                break;
            case PropagandaMaterial.LeafletBig: 
                LeafletsBig--;
                break;
            case PropagandaMaterial.LeafletSmall:
                LeafletsSmall--;
                break;
            case PropagandaMaterial.PaperBig:
                PaperBig--;
                break;
            case PropagandaMaterial.PaperSmall:
                PaperSmall--;
                break;
        }
        
        Recount();
        ChangetMaterialsCount?.Invoke();
    }

    //переписать дубляж под универсальную переменную с "параметром тип Материала" (ref out)
    
    public bool IsEnoughMaterial(PropagandaMaterial propagandaMaterial)
    {
        int templateMaterial ;
        
        switch (propagandaMaterial)
        {
            case PropagandaMaterial.Poster:
                templateMaterial = Poster;
                break;
            case PropagandaMaterial.LeafletBig: 
                templateMaterial = LeafletsBig;
                break;
            case PropagandaMaterial.LeafletSmall:
                templateMaterial = LeafletsSmall;
                break;
            case PropagandaMaterial.PaperBig:
                templateMaterial = PaperBig;
                break;
            case PropagandaMaterial.PaperSmall:
                templateMaterial = PaperSmall;
                break;
            default:
                templateMaterial = 0;
                break;
        }

        if (templateMaterial > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoadProgress()
    {
        PaperSmall = PlayerPrefs.GetInt("PaperSmall");
        PaperBig = PlayerPrefs.GetInt("PaperBig");
        LeafletsSmall = PlayerPrefs.GetInt("LeafletsSmall");
        LeafletsBig = PlayerPrefs.GetInt("LeafletsBig");
        Poster = PlayerPrefs.GetInt("Poster");
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("PaperSmall", PaperSmall);
        PlayerPrefs.SetInt("PaperBig", PaperBig);
        PlayerPrefs.SetInt("LeafletsSmall", LeafletsSmall);
        PlayerPrefs.SetInt("LeafletsBig", LeafletsBig);
        PlayerPrefs.SetInt("Poster", Poster);
        PlayerPrefs.Save();
    }

    public void ResettingAll()
    {
        PlayerPrefs.SetInt("PaperSmall", 0);
        PlayerPrefs.SetInt("PaperBig", 0);
        PlayerPrefs.SetInt("LeafletsSmall", 0);
        PlayerPrefs.SetInt("LeafletsBig", 0);
        PlayerPrefs.SetInt("Poster", 0);
        PlayerPrefs.Save();
        LoadProgress();
        ChangetMaterialsCount?.Invoke();
    }
        
    private void Recount()
    {
        for (int i = 0; i < _materials.Count; i++)
        {
            if (_materials[i] >_maxCapacity)
            {
                _materials[i] = 0;
            }
            else if (_materials[i] < 0)
            {
                _materials[i] = 0;
            }
        }
    }
}
