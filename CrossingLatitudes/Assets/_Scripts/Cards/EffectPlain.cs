using System.Collections;

[System.Serializable]
public abstract class EffectPlain 
{
    public abstract string GetDescription();

    public abstract IEnumerator Perform();
}
