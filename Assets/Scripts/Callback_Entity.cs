using System.Xml.Serialization;
using UnityEngine;

public abstract class Callback_Entity : MonoBehaviour
{
    [SerializeField]
    private bool hasCallback;// Does the entity have a callback


    public void SaveCallback()
    {
        CaptureState();
        hasCallback = true;
    }

    public void RecallCallback()
    {
        if (!hasCallback) return; 
        RestoreState();
    }

    public virtual void ClearCallback()
    {
        hasCallback= false;
    }

    protected abstract void CaptureState();
   

    protected abstract void RestoreState();
    
}



