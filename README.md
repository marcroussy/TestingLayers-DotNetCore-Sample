# TestingLayers-DotNetCore-Sample

A sample repository for how I like to structure the different layers of tests for a project in .NET Core.

## Requirements To Run Locally

You'll need to have two environment values set to run the Acceptance and External Dependency Tests:
- TL_API_URL : The URL against which you want the acceptance tests to run

On a Mac, you can use these commands to set the environment variables (as of High Sierra 10.13.3):
- launchctl setenv TL_API_URL "localhost:50314/api"
