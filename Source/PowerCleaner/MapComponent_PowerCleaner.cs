using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class MapComponent_PowerCleaner : MapComponent
    {
        [TweakValue( "PowerCleaner", 1f )]
        private static float _ticksPerSpawn = 3f;
        public static int TicksPerSpawn => (int) _ticksPerSpawn;

        public MapComponent_PowerCleaner( Map map ) : base( map )
        {
        }

        public PowerCleaner ActivePowerCleaner { get; set; }

        public bool ValidPowerCleaner
        {
            get
            {
                return ActivePowerCleaner?.Wearer != null                    &&
                       Find.Selector.IsSelected( ActivePowerCleaner.Wearer ) &&
                       ActivePowerCleaner.Wearer.Drafted;
            }
        }

        public override void MapComponentOnGUI()
        {
            if ( ValidPowerCleaner )
            {

            }
            else
            {
                ActivePowerCleaner = null;
            }
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            // handle button down state
            if ( Input.GetMouseButton( 0 ) &&
                 ValidPowerCleaner         &&
                 ( Find.TickManager.TicksGame + GetHashCode() ) % TicksPerSpawn == 0 )
            {
                var water = new Water( ActivePowerCleaner.Wearer.DrawPos, UI.MouseMapPosition() );
                GenSpawn.Spawn( ThingDefOf.PowerCleaner_WaterDef, water.Position, map );
            }
        }
    }
}