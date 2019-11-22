﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// De path class beheerd een array van waypoints. En houd bij bij welk waypoint een object is.
    /// Deze vormen samen het pad. 
    /// Logica die op het path niveau plaatsvindt gebeurt in deze class.
    /// Een deel van de functies welke je nodig hebt zijn hier al beschreven.
    /// </summary>
    public class Path : MonoBehaviour
        {
        /// <summary>
        /// Deze functie returned het volgende waypoint waar naartoe kan worden bewogen.
        /// </summary>
        /// 

        public Waypoint[] waypoints;

        private void Start()
        {
            waypoints = this.gameObject.GetComponentsInChildren<Waypoint>();
        }

        public Waypoint GetNextWaypoint(int targetNumber, GameObject AI)
        {
            if (targetNumber >= waypoints.Length)
            {
            AI.GetComponent<PathFollower>().targetNumber = 0;
            return waypoints[0];
            }

            return waypoints[targetNumber];
        }
    }