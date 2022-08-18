# Check if the NUKE global tool is already installed.
Write-Output "Checking for NUKE global tool."
if ((dotnet tool list -g).Where({ $_ -like "*nuke.globaltool*"}, 'First').Count -gt 0)
{
    Write-Output "NUKE global tool found.  Skipping installation."
}
else
{
    # Install NUKE Build globally.
    Write-Output "NUKE global tool not found.  Installing."
    dotnet tool install Nuke.GlobalTool --global
}

# Configure git flow
iex "git flow init -d --feature feature/ --bugfix bugfix/ --release release/ --hotfix hotfix/ --support support/ -t 'v'";

# Checkout develop
iex "git checkout develop"

# Run the Confluent platform
iex "nuke ConfluentPlatformUp"