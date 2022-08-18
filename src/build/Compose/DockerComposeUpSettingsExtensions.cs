// -----------------------------------------------------------------------
// <copyright file="DockerComposeUpSettingsExtensions.cs" company="Enterprise Products Partners L.P. (Enterprise)">
// © Copyright 2012 - 2022, Enterprise Products Partners L.P. (Enterprise), All Rights Reserved.
// Permission to use, copy, modify, or distribute this software source code, binaries or
// related documentation, is strictly prohibited, without written consent from Enterprise.
// For inquiries about the software, contact Enterprise: Enterprise Products Company Law
// Department, 1100 Louisiana, 10th Floor, Houston, Texas 77002, phone 713-381-6500.
// </copyright>
// -----------------------------------------------------------------------

namespace Nuke.Common.Tools.Docker
{
    using JetBrains.Annotations;

    using Tooling;

    public static class DockerComposeUpSettingsExtensions
    {
        [Pure]
        public static T SetDetach<T>(this T settings, bool detach)
            where T : DockerComposeUpSettings
        {
            settings = settings.NewInstance();
            settings.Detach = detach;

            return settings;
        }
    }
}
