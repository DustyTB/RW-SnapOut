﻿using System.Collections.Generic;
using RimWorld;
using Verse.AI;
using Verse;

namespace  Calm_Down
{
    class JobDriver_SnappingOut : JobDriver
    {


        //Toil Reservations
        #region toilreservations
        public override bool TryMakePreToilReservations()
        {
            return true;
        }
        #endregion


        //Toil stuffs
        #region toilstuffs
        protected override IEnumerable<Toil> MakeNewToils()
        {
            Pawn rpawn = this.pawn;
            this.TargetThingB = this.pawn;
            IntVec3 c = RCellFinder.RandomWanderDestFor(rpawn, rpawn.PositionHeld, 5f, null, Danger.None);
            yield return Toils_Goto.GotoCell(c, PathEndMode.OnCell);       
            Toil waitonspot = Toils_General.Wait(500);
            waitonspot.socialMode = RandomSocialMode.Off;
            yield return waitonspot;
            Toil snappingout = Toils_General.Do(delegate
            {
                rpawn.MentalState.RecoverFromState();  
                rpawn.jobs.EndCurrentJob(JobCondition.Succeeded);
            });
            snappingout.socialMode = RandomSocialMode.Off;
            yield return snappingout;
        }
        #endregion


    }
}