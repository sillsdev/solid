rem call "c:\Program Files (x86)\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat"

rem pushd c:\src\sil\solid\build
pushd C:\dev\solid\build

rem MSbuild /target:installer /property:teamcity_build_checkoutDir=c:\src\sil\solid  /property:teamcity_dotnet_nunitlauncher_msbuild_task="notthere" /property:BUILD_NUMBER="0.1.345.abcd" /property:Minor="1"
MSbuild /target:Upload /property:teamcity_build_checkoutDir=.. /property:teamcity_dotnet_nunitlauncher_msbuild_task="notthere" /property:BUILD_NUMBER="0.1.345.abcd" /property:Minor="1"

popd
PAUSE

rem /verbosity:detailed