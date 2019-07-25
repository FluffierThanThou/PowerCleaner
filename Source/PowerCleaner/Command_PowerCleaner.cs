// Command_PowerCleaner.cs
// Copyright Karel Kroeze, -2019

using RimWorld;
using Verse;

namespace PowerCleaner
{
    public class Command_PowerCleaner : Command_Action
    {
        public Command_PowerCleaner( PowerCleaner cleaner )
        {
            icon = Resources.PowerCleanerIcon;
            defaultLabel = "Fluffy.PowerCleaner.Clean".Translate();
            defaultDesc = "Fluffy.PowerCleaner.Desc".Translate();

            action = () => Find.CurrentMap.GetComponent<MapComponent_PowerCleaner>().ActivePowerCleaner = cleaner;
        }
    }
}