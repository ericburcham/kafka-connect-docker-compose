// -----------------------------------------------------------------------
// <copyright file="Build.cs" company="Enterprise Products Partners L.P. (Enterprise)">
// © Copyright 2012 - 2022, Enterprise Products Partners L.P. (Enterprise), All Rights Reserved.
// Permission to use, copy, modify, or distribute this software source code, binaries or
// related documentation, is strictly prohibited, without written consent from Enterprise.
// For inquiries about the software, contact Enterprise: Enterprise Products Company Law
// Department, 1100 Louisiana, 10th Floor, Houston, Texas 77002, phone 713-381-6500.
// </copyright>
// -----------------------------------------------------------------------

using Nuke.Common;
using Nuke.Common.Tools.Docker;

using static Nuke.Common.Tools.Docker.DockerComposeTasks;

class Build : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    Target Clean =>
        _ => _
             .Before(Restore)
             .Executes(() =>
             {
             });

    Target Compile =>
        _ => _
             .DependsOn(Restore)
             .Executes(() =>
             {
             });

    Target ConfluentPlatformDown =>
        _ => _
            .Executes(() =>
            {
                DockerComposeDown(upSettings => upSettings.SetFile(@"./src/confluent-platform/docker-compose.yml"));
            });

    Target ConfluentPlatformUp =>
        _ => _
            .Executes(() =>
            {
                DockerComposeUp(upSettings => upSettings.SetFile(@"./src/confluent-platform/docker-compose.yml").SetDetach(true));
            });

    Target Restore =>
        _ => _
            .Executes(() =>
            {
            });

    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);
}
