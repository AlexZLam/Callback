using UnityEngine;

public class Switch_Callback : Callback_Entity
{
    public SwitchController switchController;
    public bool saveActivated;

    protected override void CaptureState()
    {
        saveActivated=  switchController.GetActivated();
        // store in saveActivated
    }

    protected override void RestoreState()
    {
        switchController.SetActivated(saveActivated);
    }

    private void Awake()
    {
        switchController = GetComponent<SwitchController>();
    }
}

