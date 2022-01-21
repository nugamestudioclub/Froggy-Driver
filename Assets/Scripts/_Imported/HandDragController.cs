using UnityEngine;
using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	class HandDragController : MonoBehaviour, IDragController {
		public bool IsAllowed(PointerEventData eventData) {
			return true;
		}

		public bool TryDrop(PointerEventData eventData) {
			return true;
		}
	}
}
