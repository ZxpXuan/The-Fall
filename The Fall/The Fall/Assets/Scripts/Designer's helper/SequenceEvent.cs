using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtility
{
	public class SequenceEvent : MonoBehaviour
	{
		[System.Serializable]
		public class SequenceEventData
		{
			public string Label;
			public float BeforeTriggerDelay;
			public float AfterTriggerDelay;
			public UnityEvent Event;
		}

		[SerializeField] bool startAtBeginning;
		[SerializeField] bool manuallyExecute;

		[SerializeField] SequenceEventData[] sequences;

		public bool Running { get; private set; }
		public int nextEventIndex { get; private set; }
		public bool ManuallyExecute
		{
			get { return manuallyExecute; }
			set { manuallyExecute = value; }
		}

		private void Start()
		{
			if (startAtBeginning)
				Execute();
		}

		public void Execute()
		{
			if (Running) return;

			if (manuallyExecute)
			{
				sequences[nextEventIndex].Event.Invoke();
				nextEventIndex ++;

				if (nextEventIndex >= sequences.Length)
					nextEventIndex = 0;
			}
			else
			{
				StartCoroutine(RunningEvents());
			}
		}

		public void AbortAndReset()
		{
			StopAllCoroutines();
			Running = false;
			nextEventIndex = 0;
		}

		IEnumerator RunningEvents()
		{
			Running = true;
			nextEventIndex = 0;
			foreach (var seq in sequences)
			{
				if (seq.BeforeTriggerDelay > 0)
					yield return new WaitForSeconds(seq.BeforeTriggerDelay);

				if (seq.Event != null)
					seq.Event.Invoke();

				if (seq.AfterTriggerDelay > 0)
					yield return new WaitForSeconds(seq.AfterTriggerDelay);
			}
			Running = false;
			nextEventIndex = -1;
		}
	}
}
