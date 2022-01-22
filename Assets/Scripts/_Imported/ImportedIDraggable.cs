using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	public interface ImportedIDraggable {
		bool IsAllowed(PointerEventData eventData);
	}
}