using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public GameObject cube;
    public List<GameObject> players = new List<GameObject>();
    public LayerMask layer, layerMask;
    private Camera _cam;
    private GameObject _cubeSelection;
    private RaycastHit _hit;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && players.Count > 0)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
            {
                foreach (var el in players)
                {
                    if (el != null)
                    {
                        var agent = el.GetComponent<UnityEngine.AI.NavMeshAgent>();
                        if (agent != null)
                        {
                            agent.SetDestination(agentTarget.point);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var el in players)
            {
                if (el != null)
                {
                    var child = el.transform.GetChild(0);
                    if (child != null)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }

            players.Clear();

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        if (_cubeSelection)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (xScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (zScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }

                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }

        if (Input.GetMouseButtonUp(0) && _cubeSelection)
        {
            Vector3 halfExtents = _cubeSelection.transform.localScale / 2;
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                halfExtents,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            foreach (var el in hits)
            {
                if (el.collider.CompareTag("Enemy")) continue;
                if (el.transform != null && el.transform.gameObject != null)
                {
                    players.Add(el.transform.gameObject);
                    var child = el.transform.GetChild(0);
                    if (child != null)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            Destroy(_cubeSelection);
        }
    }
}
