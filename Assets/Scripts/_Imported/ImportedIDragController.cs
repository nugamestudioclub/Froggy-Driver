using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	interface ImportedIDragController {
		bool IsAllowed(PointerEventData eventData);

		bool TryDrop(PointerEventData eventData);
	}
}
