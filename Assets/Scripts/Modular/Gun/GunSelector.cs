using RepeatUtils;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class GunSelector : RepeatMonoBehaviour
    {
        public EventHandler<OnSwitchGunEventArgs> OnSwitchGun;

        public class OnSwitchGunEventArgs : EventArgs
        {
            public GunSO GunSO;
        }

        public class OnReloadEventArgs : EventArgs
        {
            public float ReloadtimerNormalize;
        }

        public class OnUpdatedBulletEventArgs : EventArgs
        {
            public float CurrentBullet;
            public float TotalBullet;
        }

        [SerializeField]
        private List<GunSO> gunSOList;

        [SerializeField]
        private Transform gunHoldTransform;

        [SerializeField]
        private int projectileLayerMarkIndex;

        [SerializeField]
        private bool holdGunOnStart;

        [SerializeField]
        private bool infiniteBullet;

        private List<GunController> gunControllerList;
        private int indexSelectGun;

        private void Start()
        {
            InitializeGunControllers();
            if (holdGunOnStart) ActiveGun(0);
        }

        private void InitializeGunControllers()
        {
            gunControllerList = new();
            foreach (var item in gunSOList)
            {
                GameObject gun = Instantiate(item.Prefab, gunHoldTransform.position, Quaternion.identity);
                gun.transform.parent = gunHoldTransform;
                GunController gunController = gun.GetComponent<GunController>();
                gunControllerList.Add(gunController);
            }
        }

        private void ActiveGun(int indexSelectGun)
        {
            if (indexSelectGun < 0 || indexSelectGun >= gunSOList.Count) return;

            gunControllerList.ForEach(gunController => gunController.gameObject.SetActive(false));
            gunControllerList[indexSelectGun].gameObject.SetActive(true);

            OnSwitchGun?.Invoke(this, new OnSwitchGunEventArgs
            {
                GunSO = gunSOList[indexSelectGun]
            });
        }

        public void SwitchGunNext()
        {
            if (gunSOList.Count <= 1) return;

            indexSelectGun = (indexSelectGun + 1) % gunSOList.Count;
            ActiveGun(indexSelectGun);
        }

        public void Shoot(Vector3 shootDirection, bool isDeltaTime)
        {
            CurrentGunController().SpawnProjectile(shootDirection);
        }


        public GunSO CurrentGunSO() => gunSOList[indexSelectGun];

        private GunController CurrentGunController() => gunControllerList[indexSelectGun];
    }
}
