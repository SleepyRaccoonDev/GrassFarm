using UnityEngine;
using Zenject;

public class Dropper : MonoBehaviour
{
    [SerializeField] private DropItemConfig _dropConfig;

    private IDroppable _droppable;

    private Factory<Item> _factory;

    private void OnEnable()
    {
        SubscribeToEvent();
        _droppable.IsDropped += Drop;
    }

    private void OnDisable()
    {
        _droppable.IsDropped -= Drop;
    }

    [Inject]
    private void Construct(Factory<Item> factory)
    {
        _factory = factory;
    }

    public void SetConfig(Factory<Item> factory, DropItemConfig config)
    {
        _factory = factory;
        _dropConfig = config;
        //SubscribeToEvent();
    }

    public void Drop(Vector3 position)
    {
        float chance = _dropConfig.ChanseToDropItem;

        if (_dropConfig == null || Random.value < 1 - chance)
            return;

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);

        var item = _factory.Get(_dropConfig.ItemPrefab, position, rotation, null);

        item.Initialize(_dropConfig.Type, _dropConfig.Color, _dropConfig.Currency);

        if (_dropConfig.NextDrop != null)
        {
            var dropper = item.gameObject.AddComponent<Dropper>();
            dropper.SetConfig(_factory, _dropConfig.NextDrop);

        }
    }

    private void SubscribeToEvent()
    {
        _droppable = GetComponent<IDroppable>();
    }
}