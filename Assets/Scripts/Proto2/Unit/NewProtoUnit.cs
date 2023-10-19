using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Proto2.PathFinding;
using UnityEngine;

namespace Proto2.Unit
{
    public abstract class NewProtoUnit<T> : MonoBehaviour where T : NewProtoPathPoint<T>
    {
        [SerializeField] private Transform rotator, body;
        [SerializeField] private float positionSpeed = 1f, rotationSpeed = 1f;
        [SerializeField] private T startPos;
        [SerializeField] protected NewProtoPathRenderer pathRenderer;
        protected T currentPos;
        protected T targetPos;
        protected T finalTargetPos;
        protected int pathIndex = 0;
        protected readonly List<T> path = new();
        protected List<T> possiblePositions = new();

        protected void Start()
        {
            currentPos = startPos;
            transform.position = currentPos.Node.transform.position;
            possiblePositions = FindObjectsOfType<T>().ToList();
        }

        private float rotationFactor, positionFactor;
        protected IEnumerator MoveToTargetPos()
        {
            rotationFactor = 0f;
            rotator.LookAt(targetPos.Node);
            var baseRotation = body.rotation;
            while (rotationFactor <= 1f)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                body.rotation = Quaternion.Slerp(baseRotation, rotator.rotation, rotationFactor);
                rotationFactor += Time.deltaTime * rotationSpeed;
            }
            
            positionFactor = 0f;
            var basePosition = body.position;
            while (positionFactor <= 1f)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                body.position = Vector3.Lerp(basePosition, targetPos.Node.position, positionFactor);
                positionFactor += Time.deltaTime * positionSpeed;
            }
            rotator.position = body.position;

            currentPos = targetPos;
            MovementFinished();
        }

        protected virtual void MovementFinished()
        {
            throw new Exception();
        }
    }
}
