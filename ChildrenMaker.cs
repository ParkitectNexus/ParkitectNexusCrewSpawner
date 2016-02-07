using System.Collections;
using UnityEngine;

namespace Children
{
    class ChildrenMaker : MonoBehaviour
    {
        private bool _started;

        void Start()
        {
            EventManager.Instance.OnGuestAdded += GuestAdded;
        }

        void Update()
        {
            if (!_started && !GameController.Instance.isLoadingGame)
            {
                _started = true;

                StartCoroutine(MorphToChildren());
            }
        }

        private IEnumerator MorphToChildren()
        {
            // just to be safe
            yield return new WaitForSeconds(2);

            foreach (Guest guest in FindObjectsOfType<Guest>())
            {
                MakeChildren(guest);
            }
        }

        private void GuestAdded(Guest guest)
        {
            MakeChildren(guest);
        }

        private void MakeChildren(Guest guest)
        {
            float scale = Random.value * 0.5f + 0.6f;
            guest.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
