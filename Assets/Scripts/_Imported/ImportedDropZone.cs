using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pazaak.Unity {
	public class ImportedDropZone : MonoBehaviour, IDropHandler {
		private static string viewName = "View";

		private List<GameObject> views;

		public GameObject View => views[views.Count - 1];

		public ImportedDropZone() {
			views = new List<GameObject>();
		}

		private void Start() {
			views.Add(transform.Find(viewName).gameObject);
		}

		public void OnDrop(PointerEventData eventData) {
			//eventData.pointerDrag.GetComponent<Image>().enabled = false;
		}

		public bool TryDrop(PointerEventData eventData) {
			ImportedDragger dragger = eventData.pointerDrag.GetComponent<ImportedDragger>();
			bool destroy = false;

			if( dragger != null ) {
				GameObject oldView = View;

				views.Add(dragger.View);
				if( dragger.View != null ) {
					View.transform.SetParent(transform);
					View.transform.SetPositionAndRotation(transform.position, transform.rotation);
				}
				if( destroy )
					Destroy(oldView);
				else
					views.Add(View);
			}

			return true;
		}
	}
}