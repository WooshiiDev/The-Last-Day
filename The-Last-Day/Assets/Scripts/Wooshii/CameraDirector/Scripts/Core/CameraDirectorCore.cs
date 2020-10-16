using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WooshiiAttributes;

namespace Director
    {
    // K is spring constant
    // B is damping constant
    // m is point mass

    // B going to 0 => overshoot and oscillations (underdamping)
    // B going to 1 => slow convergence (overdamping)
    // B falling between has no oscillations (critical damping)

    //CRITICAL DAMPING OCCURS WHEN b^2 = 4(m * K)

    //Basic Equation for Spring
    //k * (desiredPosition - min) - damp
    //k * (desiredPosition - 'origin or target') - (dampValue * vectorLength)

    //CRITICAL DAMPING EQUATION
    //W = Sqrt(k/m) [Stiffness/Strength], can be used for damping
    //W^(2) * (desired - origin) - 2 * W;

    [DisallowMultipleComponent ()]
    public class DirectorCore : MonoBehaviour
        {
        
        }

    public static class DirectorSmoothing
        {
        [HideInInspector]
        private const float k = 1;

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 velocity, float dampTime, float deltaTime)
            {
            //Get smoothing for each component
            current.x = SmoothDamp (current.x, target.x, ref velocity.x, dampTime, deltaTime);
            current.y = SmoothDamp (current.y, target.y, ref velocity.y, dampTime, deltaTime);
            current.z = SmoothDamp (current.z, target.z, ref velocity.z, dampTime, deltaTime);

            return current;
            }

        public static float SmoothDamp(float current, float target, ref float velocity, float dampTime, float deltaTime)
            {
            //Clamp
            dampTime = Mathf.Max (dampTime, 0.0001f, dampTime);

            //Calculate smoothing and get per frame value
            float omega = 2.0f / dampTime;
            float x = omega * Time.deltaTime;
            float exp = 1.0f / (1.0f + x + 0.48f * x * x + 0.235f * x * x * x);

            //Get length
            float change = current - target;

            //Get frame velocity and calculate new velocity
            float temp = (velocity + omega * change) * deltaTime;
            velocity = (velocity - omega * temp) * exp;

            //Return new position target
            return target + (change + temp) * exp;
            }

        /// <summary>
        /// Dampen a springs force around a pivot
        /// </summary>
        /// <param name="position">Current position</param>
        /// <param name="pivot">The pivot the spring is held by</param>
        /// <param name="damp">Current dampened value</param>
        /// <param name="dampTime">Time in seconds to dampen</param>
        /// <param name="deltaTime">Update time to apply damp</param>
        /// <returns></returns>
        public static Vector3 SpringDamp(Vector3 position, Vector3 pivot, Vector3 damp, float dampTime, float deltaTime)
            {
            //Spring = -k * (range)
            //Damp = dampTime * Force;
            //Force = Spring force - Damp Force
            return Spring (position, pivot, deltaTime) - (dampTime * damp);
            }

        /// <summary>
        /// Spring a Vector based on the length between the position and pivot
        /// </summary>
        /// <param name="position">Current vector value</param>
        /// <param name="pivot">Target</param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public static Vector3 Spring(Vector3 position, Vector3 pivot, float deltaTime)
            {
            return -k * (position - pivot) * deltaTime;
            }

        /// <summary>
        /// Spring a value based on range 
        /// Uses constant <see cref="k"/> for inversion
        /// </summary>
        /// <param name="initial">Initial or current value</param>
        /// <param name="target">Target value to reach</param>
        /// <returns></returns>
        public static float Spring(float initial, float target, float deltaTime)
            {
            return -k * (initial - target) * deltaTime;
            }
        }
    }


