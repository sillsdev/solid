# SOLID
Solid is a Windows .Net Framework desktop application that can be used to check, clean up, and convert Standard Format (e.g. Toolbox) lexicon data.  It is most commonly used by technology support personnel when bringing linguistic data from SIL Toolbox into SIL Fieldworks.

Learn more at [software.sil.org](https://software.sil.org/solid/).

For bug reports, please [submit an issue in our issue tracker](https://github.com/sillsdev/solid/issues).

# Developers

### Pull request process

Generally, a PR is required for code changes.  At least one approving review and all unit tests passing are required for a PR to be merged.

### Releasing a new version

This project uses GitHub Actions CI to automate building, running unit tests, creating an installer, and creating a GitHub release.

1. Prepare for a release by changing the version numbers hard-coded in the installer files (search for current version)
1. Push a version tag in the format `v1.0` to the commit you want to release.  This will kick off GHA to build, test, and create the installer
1. Once finished, visit https://github.com/sillsdev/solid/releases and modify the draft release, automatically created, to your satisfaction.  Publish the release.
1. Publish the new installer file on software.sil.org

### Building locally (debug)

1. `dotnet restore src/solid.sln`
1. `dotnet build src/solid.sln` (debug)
or
1. `dotnet build -c Release src/solid.sln` (release)

Alternatively you can use Visual Studio and build / run tests there

### Running unit tests

`dotnet test output/net461/*Tests.dll`

### Create an installer locally
(normally GHA will do this for you - this is if you need to make an installer locally)
Make sure you [have InnoSetup installed locally](https://jrsoftware.org/isinfo.php) on your machine and that iscc.exe is available in your PATH
`iscc .\installer\setup.iss`