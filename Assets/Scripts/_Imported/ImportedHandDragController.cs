using UnityEngine;
using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	class ImportedHandDragController : MonoBehaviour, ImportedIDragController {
		public bool IsAllowed(PointerEventData eventData) {
			return true;
		}

		public bool TryDrop(PointerEventData eventData) {
			return true;
		}
	}
}
