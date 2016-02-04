﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2015 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using OsmSharp.Math.Geo;
using OsmSharp.Routing.Network;
using OsmSharp.Routing.Profiles;
using OsmSharp.Units.Distance;
using System;
using System.Collections.Generic;

namespace OsmSharp.Routing
{
    /// <summary>
    /// Abstract representation of a router.
    /// </summary>
    public interface IRouter
    {
        /// <summary>
        /// Returns true if all given profiles are supported.
        /// </summary>
        bool SupportsAll(params Profile[] profiles);

        /// <summary>
        /// Searches for the closest point on the routing network that's routable for the given profiles.
        /// </summary>
        /// <returns></returns>
        Result<RouterPoint> TryResolve(Profile[] profiles, float latitude, float longitude,
            Func<RoutingEdge, bool> isBetter, float searchDistanceInMeter = Constants.SearchDistanceInMeter);

        /// <summary>
        /// Checks if the given point is connected to the rest of the network. Use this to detect points on routing islands.
        /// </summary>
        /// <returns></returns>
        Result<bool> TryCheckConnectivity(Profile profile, RouterPoint point, float radiusInMeters);

        /// <summary>
        /// Calculates a route between the two locations.
        /// </summary>
        /// <returns></returns>
        Result<Route> TryCalculate(Profile profile, RouterPoint source, RouterPoint target);

        /// <summary>
        /// Calculates the weight between the two locations.
        /// </summary>
        /// <returns></returns>
        /// <remarks>The weight is the distance * factor from the given profile.</remarks>
        Result<float> TryCalculateWeight(Profile profile, RouterPoint source, RouterPoint target);

        /// <summary>
        /// Calculates all routes between all sources and all targets.
        /// </summary>
        /// <returns></returns>
        Result<Route[][]> TryCalculate(Profile profile, RouterPoint[] sources, RouterPoint[] targets,
            ISet<int> invalidSources, ISet<int> invalidTargets);

        /// <summary>
        /// Calculates all routes between all sources and all targets.
        /// </summary>
        /// <returns></returns>
        Result<Route[][]> TryCalculate(Profile profile, RouterPoint[] sources, RouterPoint[] targets,
            HashSet<int> invalidSources, HashSet<int> invalidTargets);

        /// <summary>
        /// Calculates all weights between all sources and all targets.
        /// </summary>
        /// <returns></returns>
        Result<float[][]> TryCalculateWeight(Profile profile, RouterPoint[] sources, RouterPoint[] targets,
            ISet<int> invalidSources, ISet<int> invalidTargets);

        /// <summary>
        /// Calculates all weights between all sources and all targets.
        /// </summary>
        /// <returns></returns>
        Result<float[][]> TryCalculateWeight(Profile profile, RouterPoint[] sources, RouterPoint[] targets,
            HashSet<int> invalidSources, HashSet<int> invalidTargets);
    }
}