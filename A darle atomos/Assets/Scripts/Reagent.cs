using UnityEngine;

public class Reagent
{
    public Reagent(string reagentName, float molarMass, Color flameColor, bool isSolid)
    {
        ReagentName = reagentName;
        MolarMass = molarMass;
        FlameColor = flameColor;
        IsSolid = isSolid;
    }

    public string ReagentName { get;}
    public float MolarMass { get;}
    public Color FlameColor { get;}
    public bool IsSolid { get;}
}
