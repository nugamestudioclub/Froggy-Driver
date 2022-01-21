using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	interface IDragController {
		bool IsAllowed(PointerEventData eventData);

		bool TryDrop(PointerEventData eventData);
	}
}
