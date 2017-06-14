using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class UnitControl : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> SelectedUnits { get; private set; }
    public GameObject PlayersUnitsRoot;

    bool _isSelecting = false;
    Vector3 _mousePosition;

    public GameObject enemy;

    void Start()
    {
        this.SelectedUnits = new List<GameObject>();
    }

    void Update()
    {
        #region Select units

        if (Input.GetMouseButtonDown(0))
        {
            UnselectUnits();

            this._isSelecting = true;
            this._mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            HighlightUnits();
        }

        if (Input.GetMouseButtonUp(0))
        {
            this._isSelecting = false;
            SelectUnits();
        }

        #endregion

        if (Input.GetMouseButtonDown(1))
        {
            var target = this.ClickResult();
            if (target.transform != null)
            {
                if (target.transform.name == "Terrain")
                    MoveTo(target.point);
                else
                    MoveTo(target.transform.gameObject);
            }
        }
    }

    void HighlightUnits()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        var firstPoint = this.GetComponent<Camera>().ScreenPointToRay(this._mousePosition);
        var secondPoint = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(firstPoint, out hit1) || !Physics.Raycast(secondPoint, out hit2))
            return;

        foreach (Transform unit in this.PlayersUnitsRoot.transform)
        {
            if (!InSelectedArea(unit.gameObject, hit1.point, hit2.point))
            {
                unit.FindChild("Selected").gameObject.SetActive(false);
            }
            else
            {
                unit.FindChild("Selected").gameObject.SetActive(true);
            }
        }
    }

    void UnselectUnits()
    {
        if (this.SelectedUnits.Count > 0)
        {
            //foreach(Transform unit in this.PlayersUnitsRoot.transform)
            //    unit.GetComponent<PlayerUnit>().TurnOffSelection();

            this.SelectedUnits.Clear();
        }
    }

    void SelectUnits()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        var firstPoint = this.GetComponent<Camera>().ScreenPointToRay(this._mousePosition);
        var secondPoint = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(firstPoint, out hit1) || !Physics.Raycast(secondPoint, out hit2))
            return;

        foreach (Transform unit in this.PlayersUnitsRoot.transform)
        {
            if (!InSelectedArea(unit.gameObject, hit1.point, hit2.point)) continue;
            unit.transform.FindChild("Selected").gameObject.SetActive(true);
            this.SelectedUnits.Add(unit.gameObject);
        }
    }

    void MoveTo(Vector3 position)
    {
        foreach (var selectedUnit in this.SelectedUnits)
            selectedUnit.GetComponent<Unit>().SetTarget(position);
    }

    void MoveTo(GameObject target)
    {
        foreach (var selectedUnit in this.SelectedUnits)
        {
            selectedUnit.GetComponent<Unit>().SetTarget(target);
        }
    }

    RaycastHit ClickResult()
    {
        RaycastHit hit;
        Physics.Raycast(this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit);
        return hit;
    }

    private void OnGUI()
    {
        if (this._isSelecting)
        {
            var rect = Utils.GetScreenRect(_mousePosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    static bool InSelectedArea(GameObject unit, Vector3 fPoint, Vector3 sPoint)
    {
        var radius = unit.GetComponent<CapsuleCollider>().radius - 0.3f;
        if (unit.transform.position.x + radius >= Mathf.Min(fPoint.x, sPoint.x) &&
            unit.transform.position.x - radius <= Mathf.Max(fPoint.x, sPoint.x) &&
            unit.transform.position.z + radius >= Mathf.Min(fPoint.z, sPoint.z) &&
            unit.transform.position.z - radius <= Mathf.Max(fPoint.z, sPoint.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}