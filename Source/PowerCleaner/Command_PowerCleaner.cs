// Command_PowerCleaner.cs
// Copyright Karel Kroeze, -2019

using RimWorld;
using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class Command_PowerCleaner : Command_Action
    {
        public Command_PowerCleaner( PowerCleaner cleaner )
        {
            defaultLabel = "Fluffy.PowerCleaner.Clean".Translate();
            defaultDesc = "Fluffy.PowerCleaner.Desc".Translate();
            icon = ThingDefOf.PowerCleaner.graphicData.Graphic.MatNorth.mainTexture as Texture2D;
            action = () => Find.CurrentMap.GetComponent<MapComponent_PowerCleaner>().ActivePowerCleaner = cleaner;
        }

        
    }
}