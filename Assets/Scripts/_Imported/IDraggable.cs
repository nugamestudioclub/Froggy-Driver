using UnityEngine.EventSystems;

namespace Pazaak.Unity {
	public interface IDraggable {
		bool IsAllowed(PointerEventData eventData);
	}
}