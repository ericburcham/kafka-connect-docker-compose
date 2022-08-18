// -----------------------------------------------------------------------
// <copyright file="DockerComposeTasks.cs" company="Enterprise Products Partners L.P. (Enterprise)">
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

    using JetBrains.Annotations;

    using Tooling;

    public static class DockerComposeTasks
    {
        internal static string DockerPath => ToolPathResolver.GetPathExecutable("docker-compose");

        public static IReadOnlyCollection<Output> DockerComposeDown(Configure<DockerComposeDownSettings> configure) => DockerComposeDown(configure(new DockerComposeDownSettings()));

        public static IReadOnlyCollection<Output> DockerComposeDown(DockerComposeDownSettings settings = null) => StartProcess(settings ?? new DockerComposeDownSettings());

        public static IReadOnlyCollection<Output> DockerComposeLogs(Configure<DockerComposeLogsSettings> configure) => DockerComposeLogs(configure(new DockerComposeLogsSettings()));

        public static IReadOnlyCollection<Output> DockerComposeLogs(DockerComposeLogsSettings settings = null) => StartProcess(settings ?? new DockerComposeLogsSettings());

        public static IReadOnlyCollection<Output> DockerComposeUp(Configure<DockerComposeUpSettings> configure) => DockerComposeUp(configure(new DockerComposeUpSettings()));

        public static IReadOnlyCollection<Output> DockerComposeUp(DockerComposeUpSettings settings = null) => StartProcess(settings ?? new DockerComposeUpSettings());

        internal static void CustomLogger(OutputType type, string output)
        {
            switch (type)
            {
                case OutputType.Std:
                    Logger.Normal(output);

                    break;
                case OutputType.Err:
                {
                    if (output.StartsWith("WARNING!"))
                    {
                        Logger.Warn(output);
                    }
                    else
                    {
                        Logger.Normal(output);
                    }

                    //TODO: logging real errors
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        static IReadOnlyCollection<Output> StartProcess([NotNull] ToolSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var process = ProcessTasks.StartProcess(settings);
            process.AssertWaitForExit();

            return process.Output;
        }
    }
}
