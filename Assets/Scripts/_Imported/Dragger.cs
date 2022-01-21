using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pazaak.Unity {
	public class Dragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
		private static Transform prevParent;

		private static Vector3 startPosition;

		private static Vector3 offset;

		public GameObject View { get; private set; }

		private static GameObject placeholder;

		private LayoutElement layoutElement;

		private CanvasGroup canvasGroup;

		private IDragController dragController;

		static Dragger() {
			offset = new Vector3();
		}

		private void Start() {
			View = transform.Find("View").gameObject;
			layoutElement = View.GetComponent<LayoutElement>();
			canvasGroup = View.GetComponent<CanvasGroup>();
			dragController = GetComponent<IDragController>();
			if( placeholder is null ) {
				placeholder = new GameObject("Placeholder");
				placeholder.AddComponent<LayoutElement>();
				placeholder.transform.SetParent(transform.parent.parent);
			}

		}

		public void OnPointerDown(PointerEventData eventData) {
			Debug.Log(nameof(OnPointerDown) + eventData.pointerCurrentRaycast);

			offset.Set(
				eventData.position.x - transform.position.x,
				eventData.position.y - transform.position.y,
				transform.position.z);
		}

		public void OnBeginDrag(PointerEventData eventData) {
			Debug.Log(nameof(OnBeginDrag) + name);

			if( dragController?.IsAllowed(eventData) ?? false ) {
				LayoutElement copy = placeholder.GetComponent<LayoutElement>();
				layoutElement = View.GetComponent<LayoutElement>();

				startPosition = transform.position;
				prevParent = transform.parent;

				foreach( FieldInfo field in layoutElement.GetType().GetFields() )
					field.SetValue(copy, field.GetValue(layoutElement));

				placeholder.transform.SetParent(prevParent);

				foreach( FieldInfo field in transform.GetType().GetFields() )
					field.SetValue(placeholder.transform, field.GetValue(transform));

				placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

				transform.SetParent(transform.root);

				if( canvasGroup != null )
					canvasGroup.blocksRaycasts = false;
			}
			else
				eventData.pointerDrag = null;
		}

		public void OnDrag(PointerEventData eventData) {
			transform.position = new Vector3(
				eventData.position.x - offset.x,
				eventData.position.y - offset.y,
				transform.position.z);
		}

		public void OnEndDrag(PointerEventData eventData) {
			GameObject target = eventData.pointerCurrentRaycast.gameObject;
			DropZone dropZone = target != null ? target.GetComponentInParent<DropZone>() : null;

			if( dropZone != null && dropZone.TryDrop(eventData) ) {

				/*
				transform.position = new Vector3(
					eventData.pointerCurrentRaycast.gameObject.transform.position.x,
					eventData.pointerCurrentRaycast.gameObject.transform.position.y,
					startPosition.z);
					*/

				View = new GameObject("View");

				View.AddComponent<LayoutElement>();
				View.transform.SetParent(transform.parent.parent);

				GameObject empty = new GameObject("Empty");
				empty.AddComponent<LayoutElement>();
				empty.transform.SetParent(prevParent);
				empty.transform.SetPositionAndRotation(placeholder.transform.position, placeholder.transform.rotation);
				empty.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

				placeholder.transform.SetParent(transform.parent);
			}
			else {
				transform.SetParent(prevParent);
				transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
				transform.position = placeholder.transform.position;
				placeholder.transform.SetParent(transform.root);
			}
			if( canvasGroup != null )
				canvasGroup.blocksRaycasts = true;

		}
	}
}