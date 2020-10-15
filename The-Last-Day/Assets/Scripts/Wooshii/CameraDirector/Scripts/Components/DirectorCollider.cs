using UnityEngine;
using WooshiiAttributes;

namespace Director.Component
    {
    [RequireComponent(typeof(DirectorCamera))]
    public class DirectorCollider : DirectorComponent
        {
        public float wallPadding;
        [Range(0f, 1f)] public float radiusDamp;

        public LayerMask collisionMask;

        public float solveDelay;

        //For collision
        private RaycastHit[] hitArray;
        public Ray ray;

        [ReadOnly] public float hitRadius;
        [ReadOnly] public Vector3 hitVelocity;
        [ReadOnly] public Vector3 hitPoint;

        public override void Initialize()
            {

            }

        public override void Execute()
            {

            //TODO: [WOOSHII || CAMERA DIRECTIOR] - Apply correct smooth damn
            //directorCam.radius = DirectorSmoothing.SmoothDamp (directorCam.radius, hitRadius, ref hitVelocity, radiusDamp, Time.deltaTime);
            }

        public override void FixedExecute()
            {
            Vector3 origin = directorCam.follow.position;
            Vector3 targetPosition = origin + directorCam.TargetOffset;
            Vector3 vecLen = (targetPosition - origin);

            //Get hit wall or max distance
            ray = new Ray (origin, vecLen.normalized);

            float hitDst = Mathf.Max (0, SphereCast (ray, wallPadding, vecLen.magnitude, collisionMask));

            hitPoint = ray.GetPoint (hitDst);

            Vector3 vecOne = targetPosition - hitPoint;
            Vector3 vecTwo = hitPoint - origin;

            Vector3 vecTarget = vecLen - (vecTwo - vecOne);
            //directorCam.displacement -= (directorCam.CameraTransform.transform.position - hitPoint)*Time.deltaTime;

            directorCam.displacement = DirectorSmoothing.SmoothDamp (
                directorCam.displacement, (directorCam.CameraTransform.transform.position - hitPoint)*Time.deltaTime, ref hitVelocity, radiusDamp, Time.deltaTime);

            // posDiff = hitPoint - directorCam.transform.position;
            //directorCam.displacement -= posDiff * Time.deltaTime;
            //(hitPoint - directorCam.TargetPosition)*Time.deltaTime;



            //if (hitDst < Mathf.Epsilon)
            //    directorCam.displacement = Vector3.zero;
            }

        private float SphereCast(Ray ray, float radius, float maxDistance, LayerMask collision)
            {
            hitArray = Physics.SphereCastAll (ray, radius, maxDistance, collision, QueryTriggerInteraction.Ignore);
            float min = maxDistance;

            for (int i = 0; i < hitArray.Length; i++)
                {
                float distance = hitArray[i].distance;

                if (distance < min && hitArray[i].collider != null)
                    min = distance;
                }

            return min;
            }

        private void OnDrawGizmos()
            {
            Vector3 origin = directorCam.follow.position;
            Vector3 targetPosition = origin + directorCam.TargetOffset;

            Gizmos.DrawLine (origin, targetPosition);
            Gizmos.DrawLine (origin, targetPosition);
            Gizmos.DrawWireSphere (hitPoint, wallPadding);
            }
        }
    }

