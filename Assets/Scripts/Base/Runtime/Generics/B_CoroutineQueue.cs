using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base {
    public class B_CoroutineQueue {
        private readonly Queue<IEnumerator> actions = new Queue<IEnumerator>();
        private Coroutine m_InternalCoroutine;
        private readonly MonoBehaviour m_Owner;

        public B_CoroutineQueue(MonoBehaviour aCoroutineOwner) {
            m_Owner = aCoroutineOwner;
        }

        public void StartLoop() {
            m_InternalCoroutine = m_Owner.StartCoroutine(Process());
        }

        public void StopLoop() {
            m_Owner.StopCoroutine(m_InternalCoroutine);
            m_InternalCoroutine = null;
        }

        public void EnqueueAction(IEnumerator aAction) {
            actions.Enqueue(aAction);
        }

        public void EnqueueWait(float aWaitTime) {
            actions.Enqueue(Wait(aWaitTime));
        }

        private IEnumerator Wait(float aWaitTime) {
            yield return new WaitForSeconds(aWaitTime);
        }

        private IEnumerator Process() {
            while (true)
                if (actions.Count > 0)
                    yield return m_Owner.StartCoroutine(actions.Dequeue());
                else
                    yield return null;
        }

        public Coroutine RunCoroutine(IEnumerator enumerator) {
            return m_Owner.StartCoroutine(enumerator);
        }
        
        public Coroutine RunCoroutine(IEnumerator enumerator, float delay) {
            return m_Owner.StartCoroutine(Ienum_DelayStartIenum(enumerator, delay));
        }

        public void RunCoroutine(IEnumerator enumerator, Coroutine coroutine) {
            if (coroutine == null) {
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
            else {
                m_Owner.StopCoroutine(coroutine);
                coroutine = null; 
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
        }

        public void RunCoroutine(IEnumerator enumator, Coroutine coroutine, float waitTime) {
            m_Owner.StartCoroutine(Ienum_DelayStartIenum(enumator, coroutine, waitTime));
        }

        private IEnumerator Ienum_DelayStartIenum( IEnumerator enumerator, Coroutine coroutine, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            if (coroutine == null) {
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
            else {
                m_Owner.StopCoroutine(coroutine);
                coroutine = null;
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
        }
        
        private IEnumerator Ienum_DelayStartIenum(IEnumerator enumerator, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            m_Owner.StartCoroutine(enumerator);
        }

        public Coroutine RunFunctionWithDelay(Action method, float waitTime) {
            return m_Owner.StartCoroutine(Ienum_DelayStartFunction(method, waitTime));
        }

        private IEnumerator Ienum_DelayStartFunction(Action method, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            method?.Invoke();
        }
    }
}