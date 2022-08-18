// -----------------------------------------------------------------------
// <copyright file="DockerComposeSettings.cs" company="Enterprise Products Partners L.P. (Enterprise)">
// © Copyright 2012 - 2022, Enterprise Products Partners L.P. (Enterprise), All Rights Reserved.
// Permission to use, copy, modify, or distribute this software source code, binaries or
// related documentation, is strictly prohibited, without written consent from Enterprise.
// For inquiries about the software, contact Enterprise: Enterprise Products Company Law
// Department, 1100 Louisiana, 10th Floor, Houston, Texas 77002, phone 713-381-6500.
// </copyright>
// -----------------------------------------------------------------------

namespace Nuke.Common.Tools.Docker
{
    using System;
    using System.Collections.Generic;

    using Tooling;

    [Serializable]
    public class DockerComposeSettings : ToolSettings
    {
        internal List<string> FileInternal;

        public IReadOnlyCollection<string> File => FileInternal.AsReadOnly();

        public override Action<OutputType, string> ProcessCustomLogger => DockerComposeTasks.CustomLogger;

        public override string ProcessToolPath => DockerComposeTasks.DockerPath;

        protected override Arguments ConfigureProcessArguments(Arguments arguments)
        {
            arguments.Add("--file {value}", File);

            return base.ConfigureProcessArguments(arguments);
        }
    }
}
