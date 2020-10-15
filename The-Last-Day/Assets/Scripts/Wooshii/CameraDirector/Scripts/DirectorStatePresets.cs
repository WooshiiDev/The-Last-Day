using System.Collections.Generic;
using UnityEngine;

namespace Director
    {
    public static class DirectorStatePresets
        {
        public static Dictionary<string, DirectorState> GlobalStates = new Dictionary<string, DirectorState> ()
            {
                {"ThirdPerson"  , ThirdPersonView   },
                {"Shoulder"     , ShoulderView      },
                {"Cinematic"    , CinematicDistant  },
            };

        /// <summary>
        /// Normal sort of 3rd Person View
        /// </summary>
        public static readonly DirectorState ThirdPersonView = new DirectorState ()
            {
            stateName = "main",

            rotation = DirectorState.RotationType.PITCH_YAW,
            canMove = true,

            originOffset = new Vector3 (0, 2, 0),
            radius = 3,

            targetFOV = 60,
            fovDamp = 0.5f
            };

        /// <summary>
        /// Close shouldered view of target 
        /// </summary>
        public static readonly DirectorState ShoulderView       = new DirectorState ()
            {
            stateName = "weapon",

            rotation = DirectorState.RotationType.PITCH_YAW,
            canMove = true,

            originOffset = new Vector3 (0.5f, 1f, 0),
            radius = 2,

            targetFOV = 50,
            fovDamp = 0.4f
            };

        /// <summary>
        /// Large far view focused on target
        /// </summary>
        public static readonly DirectorState CinematicDistant   = new DirectorState ()
            {
            stateName = "cinematic-tall",

            rotation = DirectorState.RotationType.PITCH_YAW,
            canMove = true,

            originOffset = new Vector3 (0, 0f, 0),
            radius = 15f,

            targetFOV = 70,
            fovDamp = 0.9f
            };


        }
    }
