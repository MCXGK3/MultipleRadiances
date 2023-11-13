using ManyRadiances;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Modding;
using System;
using Satchel;
using System.Linq;

namespace MultipleRadiances
{

    internal class RadiancesControler:MonoBehaviour
    {
        int hit = 0;
        public bool flag1= false;
        public bool flag2= false;
        public bool flag3= false;
        public bool flag4= false;
        public bool flag5= false;
        public bool flag6= false;
        public bool flag7= false;
        public int hit1= 0;
        public int hit2= 0;
        public int hit3= 0;
        public int hit4= 0;
        public int hit5= 0;
        public bool ok = false;
        private void Awake()
        {
            int i = 0;
            for (int j = 0; j < MultipleRadiances.Instance.set.ori; j++) { i++;Log("ORI    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.any1; j++) { MultipleRadiances.Instance.rads[i].AddComponent<any1>(); i++; Log("any1   OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.any2; j++) { MultipleRadiances.Instance.rads[i].AddComponent<any2>(); i++; Log("any2    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.anyPrime; j++) { MultipleRadiances.Instance.rads[i].AddComponent<anyprime>(); i++; Log("prime    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.ultimatum; j++) { MultipleRadiances.Instance.rads[i].AddComponent<ultimatum>(); i++; Log("ultimatum    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.dumb; j++) { MultipleRadiances.Instance.rads[i].AddComponent<dumb>(); i++; Log("dumb    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.atomic; j++) { MultipleRadiances.Instance.rads[i].AddComponent<atomic>(); i++; Log("atomic    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.ironHead; j++) { MultipleRadiances.Instance.rads[i].AddComponent<ironhead>(); i++; Log("ironhead    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.superNova; j++) { MultipleRadiances.Instance.rads[i].AddComponent<supernova>(); i++; Log("supernova    OK"); }
            for (int j = 0; j < MultipleRadiances.Instance.set.forgottenLight; j++) { MultipleRadiances.Instance.rads[i].AddComponent<forgottenlight>(); i++; Log("forgottenlight    OK"); }
        }

        private void Start()
        {
            StartCoroutine(Init());
            On.HealthManager.TakeDamage += KnightHit;
        }

        private void KnightHit(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            hit += hitInstance.DamageDealt;
            orig(self, hitInstance);
        }

        private IEnumerator Init()
        {
            yield return new WaitForSeconds(2f);
            flag1 = false;
            flag2 = false;
            flag3= false;   
            flag4= false;
            flag5= false;
            flag6= false;
            flag7= false;
            hit = 0;
            hit1=0;
            hit2= 0;
            hit3= 0;
            hit4= 0;
            hit5= 0;
            foreach (var rad in MultipleRadiances.Instance.rads)
            {
                int hp = rad.GetComponent<HealthManager>().hp;
                int sw = rad.LocateMyFSM("Phase Control").FsmVariables.FindFsmInt("P2 Spike Waves").Value;
                int ar= rad.LocateMyFSM("Phase Control").FsmVariables.FindFsmInt("P3 A1 Rage").Value;
                int s1= rad.LocateMyFSM("Phase Control").FsmVariables.FindFsmInt("P4 Stun1").Value;
                int ascend= rad.LocateMyFSM("Phase Control").FsmVariables.FindFsmInt("P5 Acend").Value;
                int scream= rad.LocateMyFSM("Control").GetAction<SetHP>("Scream", 7).hp.Value;
                hit1 += (hp -sw );
                hit2 += (sw - ar);
                hit3 += (ar -s1);
                hit4 += (s1-ascend);
                hit5 += scream-720;
                    }
            hit2 += hit1;
            hit3 += hit2;
            hit4 += hit3;
            hit5 += hit4;

            foreach (var rad in MultipleRadiances.Instance.rads) { rad.GetComponent<HealthManager>().hp = 99999; rad.LocateMyFSM("Control").GetAction<SetHP>("Scream", 7).hp = 9999; }
            ok = true;
            yield break;
        }

        private void Update()
        {
            if (ok) { 
            if (hit >= hit1 && hit < hit2 && !flag1)
            {
                flag1 = true;
                MultipleRadiances.Instance.rads.ForEach(absRad => absRad.LocateMyFSM("Phase Control").SetState("Set Phase 2"));
            }
            if (hit >= hit2 && hit < hit3 && !flag2)
            {
                flag2 = true;
                MultipleRadiances.Instance.rads.ForEach(absRad => absRad.LocateMyFSM("Phase Control").SetState("Set Phase 3"));
            }
            if (hit >= hit3 && hit < hit4 && !flag3)
            {
                flag3 = true;
                MultipleRadiances.Instance.rads.ForEach(absRad => absRad.LocateMyFSM("Phase Control").SetState("Stun 1"));
            }
            if (hit >= hit4 && hit < hit5 && !flag4)
            {
                flag4 = true;
                MultipleRadiances.Instance.rads.ForEach(absRad => absRad.LocateMyFSM("Phase Control").SetState("Set Ascend"));
            }
            if (hit >= hit5 && !flag5)
            {
                flag5 = true;
                MultipleRadiances.Instance.rads.ForEach(absRad => absRad.LocateMyFSM("Control").SetState("Check Pos"));
            }

            if (MultipleRadiances.Instance.rads.Any(absRad => absRad.LocateMyFSM("Control").ActiveStateName == "Arena 2 Start"))
            {
                if (!flag6)
                {
                    MultipleRadiances.Instance.rads.ForEach(absRad =>
                    {
                        absRad.LocateMyFSM("Control").SetState("Arena 2 Start");
                        absRad.LocateMyFSM("Attack Choices").SetState("A1 End");
                    });
                    flag6 = true;
                }
            }



            }
        }


        private void OnDestroy()
        {
            On.HealthManager.TakeDamage -= KnightHit;
            foreach (var rad in MultipleRadiances.Instance.rads) { Destroy(rad); }
            MultipleRadiances.Instance.origin = false;
        }


        private void Log(object obj)
        {
            MultipleRadiances.Instance.Log(obj);
        }


    }
}