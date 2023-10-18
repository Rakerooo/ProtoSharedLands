using System;
using System.Collections;
using System.Collections.Generic;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Unit
{
    public abstract class NewProtoUnit<T> : MonoBehaviour where T : NewProtoPathPoint<T>
    {
        [SerializeField] private Transform rotator;
        [SerializeField] private float positionSpeed = 1f, rotationSpeed = 1f;
        [SerializeField] private T startPos;
        protected T currentPos;
        protected T targetPos;
        protected T finalTargetPos;
        protected List<T> path;

        private void Start()
        {
            currentPos = startPos;
        }

        private float rotationFactor, positionFactor;
        protected IEnumerator MoveToTargetPos()
        {
            rotationFactor = 0f;
            rotator.LookAt(targetPos.Node);
            
            while (!transform.rotation.Equals(rotator.rotation))
            {
                yield return new WaitForSeconds(Time.deltaTime);
                Vector3.Lerp(transform.eulerAngles, rotator.eulerAngles, rotationFactor);
                rotationFactor += Time.deltaTime * rotationSpeed;
            }
            
            positionFactor = 0f;
            while (!transform.position.Equals(targetPos.Node.position))
            {
                yield return new WaitForSeconds(Time.deltaTime);
                Vector3.Lerp(transform.position, targetPos.Node.position, positionFactor);
                positionFactor += Time.deltaTime * positionFactor;
            }
            
            TurnManager.instance.EndTitanTurn();
        }
    }
}
