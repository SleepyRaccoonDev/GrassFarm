using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(OnTriggerEventer))]
public class СashDesk : MonoBehaviour
{
    private const float SpeedOfPassItem = 15f;
    private const float TimeToDestroy = 3f;

    private OnTriggerEventer _onTriggerEventer;

    private Backpack _backpack;
    private QueueConttoller _queueConttoller;
    
    private ISeller _seller;

    private bool _isOnСashDesk;

    [Inject]
    private void Construct(Backpack backpack, QueueConttoller queueConttoller)
    {
        _backpack = backpack;
        _queueConttoller = queueConttoller;
    }

    private void Awake()
    {
        _onTriggerEventer = GetComponent<OnTriggerEventer>();

        _onTriggerEventer.IsTriggered += CheckDeal;
        _onTriggerEventer.IsExit += CheckExit;
    }

    private void OnDisable()
    {
        _onTriggerEventer.IsTriggered -= CheckDeal;
        _onTriggerEventer.IsExit -= CheckExit;
    }

    private void CheckExit(Collider collider)
    {
        if (CustomTools.TryGetComponentInChildren<ISeller>(collider, out ISeller seller))
        {
            _isOnСashDesk = false;
        }
    }

    private void CheckDeal(Collider collider)
    {
        if(CustomTools.TryGetComponentInChildren<ISeller>(collider, out ISeller seller))
        {
            _isOnСashDesk = true;
            _seller = seller;
            MakeDeal();
        }
    }

    private void MakeDeal()
    {
        if (_isOnСashDesk == false)
            return;

        var item = _seller.Sell().Supply;

        if (item == null)
            return;

        StartCoroutine(SellItemProcess(item));
    }

    private IEnumerator SellItemProcess(Supply item)
    {   
        var coroutine = StartCoroutine(PassItemProcess(item));
        StartCoroutine(_queueConttoller.ScrollQueue(coroutine, MakeDeal));
        yield return null;
    }

    private IEnumerator PassItemProcess(Supply item)
    {
        item.transform.SetParent(null);
        Transform target = _queueConttoller.GetCurrentCaracter().transform;

        while (Vector3.Distance(item.transform.position, target.position) > 0.1f)
        {
            item.transform.position = Vector3.MoveTowards(
                item.transform.position,
                target.position,
                SpeedOfPassItem * Time.deltaTime
            );
            yield return null;
        }

        item.transform.SetParent(target);
        UnityEngine.Object.Destroy(item.gameObject, TimeToDestroy);
    }
}