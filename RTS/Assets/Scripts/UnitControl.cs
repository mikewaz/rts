using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class UnitControl : MonoBehaviour
{
    public List<GameObject> SelectedUnits { get; private set; }

    bool _isSelecting = false;
    Vector3 _mousePosition;

    void Start()
    {
        this.SelectedUnits = new List<GameObject>();
    }

    void Update()
    {
        #region Select units

        if (Input.GetMouseButtonDown(0))
        {
            this.SelectedUnits.Clear();
            foreach (var unit in GameObject.FindGameObjectsWithTag("Player Unit").ToList())
                unit.GetComponent<Unit>().TurnOffSelection();
            this._isSelecting = true;
            this._mousePosition = Input.mousePosition;
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
            if (target.transform != null && target.transform.name == "Terrain")
                MoveTo(target.point);
            else
                MoveTo(target.point);
            //if (target.transform != null && target.transform.name == "Terrain") MoveTo(target.transform.position);
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

        var units = GameObject.FindGameObjectsWithTag("Player Unit").ToList();
        foreach (var unit in units)
        {
            if (!InSelectedArea(unit, hit1.point, hit2.point)) continue;
            unit.transform.FindChild("Selected").gameObject.SetActive(true);
            this.SelectedUnits.Add(unit);
        }
    }

    void MoveTo(Vector3 position)
    {
        foreach (var selectedUnit in this.SelectedUnits)
            selectedUnit.GetComponent<Unit>().SetTarget(position);
    }

    RaycastHit ClickResult()
    {
        RaycastHit hit;
        Physics.Raycast(this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit);
        return hit;
    }

    static bool InSelectedArea(GameObject unit, Vector3 fPoint, Vector3 sPoint)
    {
        if (unit.transform.position.x >= Mathf.Min(fPoint.x, sPoint.x) &&
            unit.transform.position.x <= Mathf.Max(fPoint.x, sPoint.x) &&
            unit.transform.position.z >= Mathf.Min(fPoint.z, sPoint.z) &&
            unit.transform.position.z <= Mathf.Max(fPoint.z, sPoint.z))
            return true;
        else
            return false;
    }

    // ReSharper disable once InconsistentNaming
    void OnGUI()
    {
        if (this._isSelecting)
        {
            var rect = Utils.GetScreenRect(_mousePosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}