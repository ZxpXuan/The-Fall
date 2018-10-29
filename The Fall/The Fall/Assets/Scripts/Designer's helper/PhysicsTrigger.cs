using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtility
{

	[System.Serializable] public class ColliderEvent : UnityEvent<Collider> { }
	[System.Serializable] public class Collider2DEvent : UnityEvent<Collider2D> { }
	[System.Serializable] public class CollisionEvent : UnityEvent<Collision> { }
	[System.Serializable] public class Collision2DEvent : UnityEvent<Collision2D> { }

	public class PhysicsTrigger : MonoBehaviour
	{
		[SerializeField]
		bool triggerOnlyOnce;

		[SerializeField]
		string[] triggerableTags;

		[SerializeField]
		UnityEvent onTrigged;
		bool triggeed;

		[SerializeField] ColliderEvent OnEnterTrigger;
		[SerializeField] ColliderEvent OnStayTrigger;
		[SerializeField] ColliderEvent OnExitTrigger;

		[SerializeField] CollisionEvent OnStartCollision;
		[SerializeField] CollisionEvent OnStayCollision;
		[SerializeField] CollisionEvent OnExitCollision;

		[SerializeField] Collider2DEvent OnEnterTrigger2D;
		[SerializeField] Collider2DEvent OnStayTrigger2D;
		[SerializeField] Collider2DEvent OnExitTrigger2D;

		[SerializeField] Collision2DEvent OnStartCollision2D;
		[SerializeField] Collision2DEvent OnStayCollision2D;
		[SerializeField] Collision2DEvent OnExitCollision2D;

		void OnTriggerEnter(Collider collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnEnterTrigger != null)
			{
				OnEnterTrigger.Invoke(collider);
			}
		}

		void OnTriggerStay(Collider collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnStayTrigger != null)
			{
				OnStayTrigger.Invoke(collider);
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnExitTrigger != null)
			{
				OnExitTrigger.Invoke(collider);
			}
		}

		void OnCollisionEnter(Collision collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnStartCollision != null)
			{
				OnStartCollision.Invoke(collision);
			}
		}

		void OnCollisionStay(Collision collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnStayCollision != null)
			{
				OnStayCollision.Invoke(collision);
			}
		}

		void OnCollisionExit(Collision collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnExitCollision != null)
			{
				OnExitCollision.Invoke(collision);
			}
		}


		void OnTriggerEnter2D(Collider2D collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnEnterTrigger2D != null)
			{
				OnEnterTrigger2D.Invoke(collider);
			}
		}

		void OnTriggerStay2D(Collider2D collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnStayTrigger2D != null)
			{
				OnStayTrigger2D.Invoke(collider);
			}
		}

		void OnTriggerExit2D(Collider2D collider)
		{
			if (!IsTriggerable(collider)) return;

			triggeed = true;
			if (OnExitTrigger2D != null)
			{
				OnExitTrigger2D.Invoke(collider);
			}
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnStartCollision2D != null)
			{
				OnStartCollision2D.Invoke(collision);
			}
		}

		void OnCollisionStay2D(Collision2D collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnStayCollision2D != null)
			{
				OnStayCollision2D.Invoke(collision);
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			if (!IsTriggerable(collision.collider)) return;

			triggeed = true;
			if (OnExitCollision2D != null)
			{
				OnExitCollision2D.Invoke(collision);
			}
		}

		bool IsTriggerable(Collider collider)
		{
			if (triggeed && triggerOnlyOnce) return false;

			if (triggerableTags.Length == 0) return true;

			foreach (string s in triggerableTags)
			{
				if (s == collider.tag) return true;
			}

			return false;
		}

		bool IsTriggerable(Collider2D collider)
		{
			if (triggeed && triggerOnlyOnce) return false;

			if (triggerableTags.Length == 0) return true;

			foreach (string s in triggerableTags)
			{
				if (s == collider.tag) return true;
			}

			return false;
		}
	}
}

