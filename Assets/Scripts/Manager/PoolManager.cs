using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [Header("Prefabs")]
    [SerializeField] private EffectItemView _effectItemPrefab;
    [SerializeField] private PatchItemView _patchItemPrefab;

    private ObjectPool<EffectItemView> _effectItemPool;
    private ObjectPool<PatchItemView> _patchItemPool;


    protected override void Awake()
    {
        base.Awake();

        InitEffectItemPool();
    }

    private void InitEffectItemPool()
    {
        _effectItemPool = new ObjectPool<EffectItemView>(
            createFunc: CreateEffectItem,
            actionOnGet: OnGetEffectItem,
            actionOnRelease: OnReleaseEffectItem,
            defaultCapacity: 10
        );
    }

    private EffectItemView CreateEffectItem()
    {
        var item = Instantiate(_effectItemPrefab);
        return item;
    }

    private void OnGetEffectItem(EffectItemView item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnReleaseEffectItem(EffectItemView item)
    {
        item.gameObject.SetActive(false);
    }

    public EffectItemView GetEffectItem()
    {
        return _effectItemPool.Get();
    }

    public void ReleaseEffectItem(EffectItemView item)
    {
        _effectItemPool.Release(item);
    }

    public PatchItemView GetPatchItem()
    {
        return _patchItemPool.Get();
    }

    public void ReleasePatchItem(PatchItemView item)
    {
        _patchItemPool.Release(item);
    }
}
