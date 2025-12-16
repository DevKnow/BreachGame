using UnityEngine;
using System.Collections.Generic;

public class ModuleCardView : MonoBehaviour, IBindable<ModuleModel>
{
    [SerializeField] private CommandCardView _commandCard;
    [SerializeField] private Transform _patchParent;
    private List<PatchItemView> _patchList = new();

    public void Bind(ModuleModel model)
    {
        _commandCard.Bind(model.Command.Data);

        ClearPatchList();
        var patches = model.InstalledPatches;
        for (int i = 0, iMax = patches.Count; i < iMax; i++)
        {
            AddPatchItem(patches[i]);
        }
    }

    private void ClearPatchList()
    {
        for (int i = 0, iMax = _patchList.Count; i < iMax; i++)
        {
            PoolManager.Instance.ReleasePatchItem(_patchList[i]);
        }
        _patchList.Clear();
    }

    private void AddPatchItem(PatchModel patch)
    {
        var item = PoolManager.Instance.GetPatchItem();
        item.transform.SetParent(_patchParent, false);

        var effects = patch.Data.effects;
        for (int i = 0, iMax = effects.Length; i < iMax; i++)
        {
            item.SetDesc(effects[i]);
        }

        _patchList.Add(item);
    }
}
