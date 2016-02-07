using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkitectNexusCrew
{
    class ParkitectNexusSpawner : MonoBehaviour
    {
        private bool _luukPresent;
        private bool _timPresent;
        private bool _started;
        private bool _initialCheck;

        void Start()
        {
            EventManager.Instance.OnGuestAdded += GuestAdded;
            EventManager.Instance.OnGuestRemoved += GuestRemoved;
        }

        void Update()
        {
            if (!_started && !GameController.Instance.isLoadingGame)
            {
                _started = true;

                StartCoroutine(CheckIfPresent());
            }

            if (_initialCheck)
            {
                if (!_luukPresent)
                {
                    Spawn(209);
                    _luukPresent = true;
                }

                if (!_timPresent)
                {
                    Spawn(208);
                    _timPresent = true;
                }
            }
        }

        private void Spawn(int id)
        {
            Prefabs prefab = Prefabs.Guest;
            Guest guest = GameController.Instance.park.spawnUnInitializedPerson(prefab) as Guest;
            if (guest != null)
            {
                guest.uniqueID = id;
                guest.Initialize();
            }
        }

        private IEnumerator CheckIfPresent()
        {
            // just to be safe
            yield return new WaitForSeconds(2);

            foreach (Guest guest in FindObjectsOfType<Guest>())
            {
                if (guest.uniqueID == 208)
                {
                    _timPresent = true;
                }

                if (guest.uniqueID == 209)
                {
                    _luukPresent = true;
                }

                yield return null;
            }

            _initialCheck = true;
        }

        private void GuestAdded(Guest guest)
        {
            if (guest.uniqueID == 208)
            {
                _timPresent = true;
            }

            if (guest.uniqueID == 209)
            {
                _luukPresent = true;
            }
        }

        private void GuestRemoved(Guest guest)
        {
            if (guest.uniqueID == 208)
            {
                _timPresent = false;
            }

            if (guest.uniqueID == 209)
            {
                _luukPresent = false;
            }
        }
    }
}
